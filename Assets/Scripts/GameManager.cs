using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Start UI")]
    public GameObject startPanel;
    public Button startButton;

    [Header("Music")]
    public AudioSource musicSource;
    public AudioClip backgroundLoop;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public Text gameOverText;

    [Header("Enemy Goal")]
    public int maxEnemiesAllowed = 5;   // Kaç düşman geçerse kaybediyoruz
    private int enemiesThatReachedGoal = 0;
    public Text enemiesPassedText;      // UI’da gösterilecek sayaç

    public bool IsGameStarted { get; private set; } = false;

    void Start()
    {
        Time.timeScale = 0f;
        IsGameStarted = false;

        if (startPanel != null) startPanel.SetActive(true);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        if (startButton != null)
            startButton.onClick.AddListener(StartGame);

        if (musicSource != null)
        {
            musicSource.loop = true;
            if (backgroundLoop != null)
            {
                musicSource.clip = backgroundLoop;
            }
        }
        if (musicSource != null && musicSource.clip != null)
        {
            if (!musicSource.isPlaying) musicSource.Play();
        }

        UpdateEnemiesPassedUI();
    }

    public void StartGame()
    {
        if (startPanel != null) startPanel.SetActive(false);
        Time.timeScale = 1f;
        IsGameStarted = true;

        enemiesThatReachedGoal = 0;
        UpdateEnemiesPassedUI();

        if (musicSource != null && musicSource.clip != null)
        {
            if (!musicSource.isPlaying) musicSource.Play();
        }
    }

    public void EnemyReachedGoal()
    {
        enemiesThatReachedGoal++;
        Debug.Log("Enemy reached goal! Count = " + enemiesThatReachedGoal);

        UpdateEnemiesPassedUI();

        if (enemiesThatReachedGoal >= maxEnemiesAllowed)
        {
            TriggerGameOver("Too many enemies entered the longhouse!");
        }
    }

    void UpdateEnemiesPassedUI()
    {
        if (enemiesPassedText != null)
        {
            enemiesPassedText.text = $"Enemies Passed: {enemiesThatReachedGoal}/{maxEnemiesAllowed}";
        }
    }

    void TriggerGameOver(string message)
    {
        Time.timeScale = 0f;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameOverText != null)
            gameOverText.text = message;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}