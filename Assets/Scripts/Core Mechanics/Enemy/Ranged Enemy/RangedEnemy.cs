using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private float detectionRange = 5f; // Jarak deteksi
    [SerializeField] private float attackRange = 10f;   // Jarak serang
    [SerializeField] private float moveSpeed = 2f;      // Kecepatan gerak
    [SerializeField] private float attackCooldown = 1f; // Waktu jeda antar serangan
    [SerializeField] private GameObject projectilePrefab; // Prefab proyektil
    [SerializeField] private Transform firePoint;        // Titik tembak proyektil

    private Transform player;
    private float lastAttackTime;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Cari Player berdasarkan tag
        rb = GetComponent<Rigidbody2D>();  // Mengambil Rigidbody2D
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Cek apakah ada wall di depan menggunakan raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, detectionRange);
        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            // Jika ada wall, serang wall
            if (distanceToPlayer <= attackRange)
            {
                AttackWall(hit.collider.gameObject);
            }
        }
        else
        {
            // Jika tidak ada wall, kejar atau serang player
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
            else
            {
                MoveTowards(player.position);
            }
        }
    }

    private void MoveTowards(Vector2 targetPosition)
    {
        // Hitung arah ke target dan gerakkan enemy
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Gunakan MovePosition untuk kinematic Rigidbody2D agar menghindari menembus objek lain
        rb.MovePosition(newPosition);

        // Rotasi agar enemy selalu menghadap ke arah gerakan
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void AttackPlayer()
    {
        // Serang player jika cooldown sudah selesai
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            FireProjectile(player.position);
            Debug.Log("Enemy is attacking the player!");


        }
    }

    private void AttackWall(GameObject wall)
    {
        // Serang wall jika cooldown sudah selesai
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            FireProjectile(wall.transform.position);
            Debug.Log("Enemy is attacking the wall!");
        }
    }

    private void FireProjectile(Vector2 targetPosition)
    {
        // Instansiasi proyektil dan arahkan ke target
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Vector2 direction = (targetPosition - (Vector2)firePoint.position).normalized;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * 10f; // Kecepatan proyektil

        // Rotasi proyektil agar mengarah ke target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnDrawGizmos()
    {
        // Visualisasi jarak deteksi untuk debugging
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Visualisasi raycast untuk dinding
        Gizmos.DrawLine(transform.position, transform.position + transform.right * detectionRange);
    }
}
