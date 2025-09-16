using UnityEngine;
using System.Linq;

public class PlayerAttack : MonoBehaviour
{
    public enum AttackType { Melee, Ranged }
    public AttackType attackType = AttackType.Melee;

    private float cooldownTimer = 0f;

    [Header("Attack Settings")]
    public float meleeRange = 2f;
    public float rangedRange = 10f;
    public float meleeCooldown = 1f;
    public float rangedCooldown = 2f;
    public int attackDamage = 2;

    [Header("Melee")]
    public AudioClip meleeSound;
    [Header("Effects")]
    public GameObject meleeBloodEffect;

    [Header("Ranged")]
    public GameObject projectilePrefab;
    public Transform firePoint;  // projectile çıkış noktası
    public AudioClip rangedSound;

    private Animator anim;
    private AudioSource audioSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            Enemy nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                Attack(nearestEnemy);

                if (attackType == AttackType.Melee)
                {
                    cooldownTimer = meleeCooldown;
                }
                else if (attackType == AttackType.Ranged)
                {
                    cooldownTimer = rangedCooldown;
                }
            }
        }
    }

    Enemy FindNearestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        if (enemies.Length == 0) return null;

        Enemy nearest = enemies
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
            .FirstOrDefault();

        float dist = Vector3.Distance(transform.position, nearest.transform.position);

        if (attackType == AttackType.Melee && dist <= meleeRange)
            return nearest;
        if (attackType == AttackType.Ranged && dist <= rangedRange)
            return nearest;

        return null;
    }

    void Attack(Enemy enemy)
    {
        if (attackType == AttackType.Melee)
        {
            if (anim != null)
                anim.SetTrigger("meleeAttack");

            enemy.TakeDamage(attackDamage);
            PlaySound(meleeSound);

            if (meleeBloodEffect != null)
                Instantiate(meleeBloodEffect, enemy.transform.position, Quaternion.identity);
        }
        else if (attackType == AttackType.Ranged)
        {
            if (anim != null)
                anim.SetTrigger("rangedAttack");

            if (projectilePrefab != null && firePoint != null)
            {
                GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                Projectile p = proj.GetComponent<Projectile>();
                p.SetTarget(enemy.transform, attackDamage);
            }

            PlaySound(rangedSound);
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }

    public void SetAttackType(AttackType newType)
    {
        attackType = newType;
    }
}