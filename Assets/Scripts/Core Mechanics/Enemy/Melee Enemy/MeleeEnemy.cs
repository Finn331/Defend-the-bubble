using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float detectionRange = 5f; // Jarak deteksi zombie
    [SerializeField] private float attackRange = 1f; // Jarak serang zombie
    [SerializeField] private float moveSpeed = 2f; // Kecepatan gerak zombie
    [SerializeField] private float attackDamage; // Damage serangan zombie
    [SerializeField] private float attackCooldown = 1f; // Waktu jeda antar serangan

    private float lastAttackTime;
    private Transform player;
    private bool isAttackingWall;
    private bool isWallInFront;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Cari player berdasarkan tag
    }

    void Update()
    {
        if (player == null) return;

        // Cek apakah ada Wall di depan menggunakan Raycast
        isWallInFront = CheckForWall();

        if (isWallInFront)
        {
            // Jika ada Wall di depan, serang Wall
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, detectionRange);
            if (hit.collider != null && hit.collider.CompareTag("Wall"))
            {
                AttackWall(hit.collider.gameObject);
            }
        }
        else
        {
            // Jika tidak ada Wall di depan, kejar atau serang Player
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, detectionRange);
        return hit.collider != null && hit.collider.CompareTag("Wall");
    }

    private void MoveTowards(Vector2 targetPosition)
    {
        if (isAttackingWall) return; // Jangan bergerak jika sedang menyerang Wall

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Rotasi zombie agar selalu menghadap ke arah gerakan
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            PlayerStatus playerHealth = player.GetComponent<PlayerStatus>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Zombie menyerang Player!");
            }
        }
    }

    private void AttackWall(GameObject wall)
    {
        isAttackingWall = true;

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            WallHealth wallHealth = wall.GetComponent<WallHealth>();
            if (wallHealth != null)
            {
                wallHealth.TakeDamage(attackDamage);
                Debug.Log("Zombie menyerang Wall!");
            }
        }
    }

    private void OnDestroy()
    {
        isAttackingWall = false; // Pastikan status reset ketika zombie mati
    }

    private void OnDrawGizmos()
    {
        // Visualisasi Raycast untuk debugging
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * detectionRange);
    }
}
