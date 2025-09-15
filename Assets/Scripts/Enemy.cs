using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int maxHealth = 5;
    private int currentHealth;

    public Transform[] waypoints;
    private int waypointIndex = 0;

    private Animator anim;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;

        if (anim != null) anim.SetBool("isWalking", true);
    }

    void Update()
    {
        if (waypointIndex < waypoints.Length)
        {
            Transform target = waypoints[waypointIndex];
            Vector3 dir = (target.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                waypointIndex++;
            }
        }
        else
        {
            ReachGoal();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        if (anim != null) anim.SetBool("isWalking", false);
        Destroy(gameObject, 0.5f);
    }

    public void SetColor(Color color)
    {
        if (spriteRenderer != null)
            spriteRenderer.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(1);
                if (anim != null) anim.SetTrigger("attack");
            }
        }
    }

    void ReachGoal()
    {
        // GameManager'a haber ver
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
            gm.EnemyReachedGoal();

        Destroy(gameObject);
    }
}