using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10f;
    public float rotationSpeed = 360f; // axe spin speed (degrees per second)

    private Transform target;
    private int damage;

    [Header("Effects")]
    public GameObject hitEffectPrefab;

    public void SetTarget(Transform enemy, int dmg)
    {
        target = enemy;
        damage = dmg;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // yön ve hareket
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        // kendi etrafında dönme (axe spin)
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // hedefe ulaştı mı?
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            Enemy e = target.GetComponent<Enemy>();
            if (e != null)
                e.TakeDamage(damage);

            if (hitEffectPrefab != null)
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}