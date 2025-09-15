using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public float invulnerabilityTime = 1f;
    private bool isInvulnerable = false;

    private SpriteRenderer spriteRenderer;
    private Animator anim;

    [Header("UI")]
    public Text healthText;       // yeni: can göstergesi
    public GameObject gameOverPanel;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;

        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityFrames());
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Hero's Health: " + currentHealth + "/" + maxHealth;
        }
    }

    IEnumerator InvulnerabilityFrames()
    {
        isInvulnerable = true;
        float elapsed = 0f;

        while (elapsed < invulnerabilityTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        spriteRenderer.enabled = true;
        isInvulnerable = false;
    }

    void Die()
    {
        if (anim != null) anim.SetTrigger("die");
        Time.timeScale = 0f;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }
}