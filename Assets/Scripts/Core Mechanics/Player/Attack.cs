using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; // Prefab untuk object yang akan di-spawn
    [SerializeField] private Transform spawnPoint; // Titik spawn projectile
    [SerializeField] private float spawnDelay = 0.5f; // Delay antara tap

    private float lastSpawnTime;

    void Update()
    {
        // Jika tombol kiri mouse ditekan dan sudah melewati waktu delay
        if (Input.GetMouseButtonDown(0) && Time.time > lastSpawnTime + spawnDelay)
        {
            // Periksa apakah ada musuh
            if (FindClosestEnemyWithLowestHealth() != null)
            {
                SpawnProjectile();
                lastSpawnTime = Time.time;
            }
            else
            {
                Debug.Log("Tidak ada musuh, tidak bisa menembak!");
            }
        }
    }

    void SpawnProjectile()
    {
        if (projectilePrefab != null && spawnPoint != null)
        {
            // Spawn projectile di posisi spawnPoint
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

            // Cari musuh terdekat dengan darah terendah
            GameObject target = FindClosestEnemyWithLowestHealth();

            if (target != null)
            {
                // Arahkan projectile ke musuh tersebut
                Vector3 direction = (target.transform.position - spawnPoint.position).normalized;

                // Rotasi projectile agar ujung atas sprite mengarah ke musuh
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                projectile.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

                projectile.GetComponent<PlayerProjectile>().SetDirection(direction);
            }
        }
    }

    GameObject FindClosestEnemyWithLowestHealth()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float lowestHealth = float.MaxValue;
        float closestDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            EnemyStatus enemyComponent = enemy.GetComponent<EnemyStatus>();
            if (enemyComponent != null && enemyComponent.currentHealth < lowestHealth) // Gunakan currentHealth
            {
                float distance = Vector3.Distance(spawnPoint.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    lowestHealth = enemyComponent.currentHealth; // Gunakan currentHealth
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }
}
