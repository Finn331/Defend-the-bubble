using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab; // Prefab untuk object yang akan di-spawn
    [SerializeField] private Transform spawnPoint; // Titik spawn projectile

    [Header("Manual Shoot Settings")]
    private float manualSpawnDelay; // Delay antara manual tap
    private float manualEffectiveDelay; // Delay efektif manual shooting
    private float lastManualSpawnTime; // Waktu terakhir manual shoot

    [Header("Auto Shoot Settings")]
    [SerializeField] private bool isAutoShootEnabled = false; // Toggle untuk auto shoot
    [SerializeField] private float autoFireRate = 2f; // Fire rate untuk auto shoot (tembakan per detik)
    private float autoEffectiveDelay; // Delay antara tembakan otomatis
    private float lastAutoSpawnTime; // Waktu terakhir auto shoot

    private void Awake()
    {
        UpgradeableCheck();
        UpdateEffectiveDelays();
    }

    void Update()
    {
        // Update upgradeable settings dan delay
        UpgradeableCheck();
        UpdateEffectiveDelays();

        // Manual shooting (klik kiri)
        if (Input.GetMouseButtonDown(0) && Time.time > lastManualSpawnTime + manualEffectiveDelay)
        {
            AttemptManualShoot();
        }

        // Auto shooting jika toggle diaktifkan
        if (isAutoShootEnabled && Time.time > lastAutoSpawnTime + autoEffectiveDelay)
        {
            AttemptAutoShoot();
        }

        // Toggle auto shoot menggunakan tombol "T" (contoh toggle)
        if (Input.GetKeyDown(KeyCode.T))
        {
            isAutoShootEnabled = !isAutoShootEnabled;
            Debug.Log($"Auto Shoot Toggled: {isAutoShootEnabled}");
        }
    }

    void UpgradeableCheck()
    {
        manualSpawnDelay = SaveManager.instance.attackSpeedClick;
        // Auto fire rate bisa di-upgrade jika Anda ingin menambahkannya ke sistem upgrade
        autoFireRate = SaveManager.instance.autoFireRate; // Ambil nilai fire rate auto dari SaveManager
    }

    // Perbarui delay efektif berdasarkan spawnDelay untuk manual dan auto shooting
    void UpdateEffectiveDelays()
    {
        if (manualSpawnDelay > 0)
        {
            manualEffectiveDelay = 1 / manualSpawnDelay; // Semakin besar spawnDelay, semakin kecil delay manual
        }
        else
        {
            manualEffectiveDelay = float.MaxValue; // Hindari pembagian dengan nol
        }

        if (autoFireRate > 0)
        {
            autoEffectiveDelay = 1 / autoFireRate; // Semakin besar fire rate, semakin kecil delay otomatis
        }
        else
        {
            autoEffectiveDelay = float.MaxValue; // Hindari pembagian dengan nol
        }
    }

    void AttemptManualShoot()
    {
        // Periksa apakah ada musuh
        if (FindClosestEnemyWithLowestHealth() != null)
        {
            SpawnProjectile();
            lastManualSpawnTime = Time.time;
        }
        else
        {
            Debug.Log("Tidak ada musuh, tidak bisa menembak (manual)!");
        }
    }

    void AttemptAutoShoot()
    {
        // Periksa apakah ada musuh
        if (FindClosestEnemyWithLowestHealth() != null)
        {
            SpawnProjectile();
            lastAutoSpawnTime = Time.time;
        }
        else
        {
            Debug.Log("Tidak ada musuh, tidak bisa menembak (auto)!");
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
