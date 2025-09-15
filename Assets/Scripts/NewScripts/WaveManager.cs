using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Wave
{
    public int enemyCount = 5;       // bu dalgadaki düşman sayısı
    public bool isBossWave = false;  // boss dalgası mı?
    public int enemyHealth = 5;      // düşmanların canı
    public float enemySpeed = 2f;    // düşmanların hızı
    public float spawnInterval = 0.8f; // spawn aralığı
}

public class WaveManager : MonoBehaviour
{
    [Header("References")]
    public GameManager gameManager;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public Transform[] waypoints;

    [Header("UI")]
    public Text waveText;
    public Button earlyWaveButton;
    public GameObject victoryPanel;

    [Header("Flow")]
    public Wave[] waves;              // inspector’dan ayarlayacağın dalgalar
    public float timeBetweenWaves = 6f;

    private int currentWaveIndex = 0;
    private bool isSpawning = false;
    private bool interWaveWaiting = false;
    private Coroutine interWaveRoutine;

    void Start()
    {
        if (earlyWaveButton != null)
            earlyWaveButton.onClick.AddListener(CallNextWaveNow);

        UpdateWaveText();
        if (victoryPanel != null)
            victoryPanel.SetActive(false);
    }

    void Update()
    {
        if (gameManager != null && !gameManager.IsGameStarted)
            return;

        if (currentWaveIndex >= waves.Length && !isSpawning && !interWaveWaiting)
        {
            ShowVictory();
            return;
        }

        if (!isSpawning && !interWaveWaiting && currentWaveIndex < waves.Length)
        {
            StartCoroutine(SpawnWave(currentWaveIndex));
        }
    }

    IEnumerator SpawnWave(int waveIdx)
    {
        isSpawning = true;

        int waveNum = waveIdx + 1;
        UpdateWaveText();

        Wave wave = waves[waveIdx];

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnOne(wave);
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        currentWaveIndex++;
        isSpawning = false;

        if (currentWaveIndex < waves.Length)
        {
            interWaveRoutine = StartCoroutine(InterWaveWait());
        }
    }

    IEnumerator InterWaveWait()
    {
        interWaveWaiting = true;
        float t = 0f;

        while (t < timeBetweenWaves && interWaveWaiting)
        {
            t += Time.deltaTime;
            yield return null;
        }

        interWaveWaiting = false;
        interWaveRoutine = null;
    }

    void SpawnOne(Wave wave)
    {
        GameObject prefab = wave.isBossWave ? bossPrefab : enemyPrefab;
        if (prefab == null) return;

        GameObject enemyObj = Instantiate(prefab, waypoints[0].position, Quaternion.identity);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        enemy.waypoints = waypoints;

        // değerleri inspector’dan gelen dalgaya göre uygula
        enemy.maxHealth = wave.enemyHealth;
        enemy.speed = wave.enemySpeed;
    }

    void UpdateWaveText()
    {
        if (waveText != null)
        {
            int shown = Mathf.Min(currentWaveIndex + 1, waves.Length);
            waveText.text = "Wave: " + shown.ToString();
        }
    }

    public void CallNextWaveNow()
    {
        if (gameManager != null && !gameManager.IsGameStarted)
            return;

        if (isSpawning) return;

        if (interWaveWaiting)
        {
            interWaveWaiting = false;
            if (interWaveRoutine != null) StopCoroutine(interWaveRoutine);
            interWaveRoutine = null;
        }
    }

    void ShowVictory()
    {
        if (victoryPanel != null && !victoryPanel.activeSelf)
        {
            Time.timeScale = 0f;
            victoryPanel.SetActive(true);
        }
    }
}