using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private float detectionRange = 5f; // Jarak deteksi
    [SerializeField] private float attackRange = 1f;    // Jarak serang
    [SerializeField] private float moveSpeed = 2f;      // Kecepatan gerak
    [SerializeField] private float attackDamage = 10f;  // Damage serangan
    [SerializeField] private float attackCooldown = 1f; // Waktu jeda antar serangan

    private Transform player;
    private float lastAttackTime;
    private bool isAttackingWall;
    private bool isWallInFront;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Cari Player berdasarkan tag
        rb = GetComponent<Rigidbody2D>();  // Mengambil Rigidbody2D
    }

    void Update()
    {
        if (player == null) return;

        // Cek apakah ada wall di depan menggunakan raycast
        isWallInFront = CheckForWall();

        if (isWallInFront)
        {
            // Jika ada wall, serang wall
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, detectionRange);
            if (hit.collider != null && hit.collider.CompareTag("Wall"))
            {
                AttackWall(hit.collider.gameObject);
            }
        }
        else
        {
            // Jika tidak ada wall, kejar atau serang player
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

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

    private bool CheckForWall()
    {
        // Raycast untuk mengecek keberadaan wall di depan
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, detectionRange);
        return hit.collider != null && hit.collider.CompareTag("Wall");
    }

    private void MoveTowards(Vector2 targetPosition)
    {
        // Jangan bergerak jika sedang menyerang wall
        if (isAttackingWall) return;

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
            PlayerStatus playerHealth = player.GetComponent<PlayerStatus>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Enemy is attacking the player!");
            }
        }
    }

    private void AttackWall(GameObject wall)
    {
        // Serang wall jika cooldown sudah selesai
        isAttackingWall = true;

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            WallHealth wallHealth = wall.GetComponent<WallHealth>();
            if (wallHealth != null)
            {
                wallHealth.TakeDamage(attackDamage);
                Debug.Log("Enemy is attacking the wall!");
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Visualisasi raycast untuk debugging
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * detectionRange);
    }
}
