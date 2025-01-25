using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI moneyText; // UI untuk menampilkan uang
    [SerializeField] private TextMeshProUGUI phaseText; // UI untuk menampilkan phase
    [SerializeField] private TextMeshProUGUI waveText; // UI untuk menampilkan wave

    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject[] enemyPrefabs; // Array prefab musuh biasa
    [SerializeField] private GameObject[] miniBossPrefabs; // Array prefab mini boss
    [SerializeField] private GameObject[] bossPrefabs; // Array prefab boss

    [Header("Spawn Settings")]
    [SerializeField] private float spawnDelayPhase1to3; // Delay antar musuh untuk phase 1-3
    [SerializeField] private float spawnDelayPhase4to6; // Delay antar musuh untuk phase 4-6
    [SerializeField] private float spawnDelayPhase6Plus; // Delay antar musuh untuk phase 6 ke atas

    [Header("Game Settings")]
    public int currentPhase = 1;
    private int currentWave = 1;

    private int enemiesPerWave;
    private int miniBossSpawnInterval = 1; // Mini boss muncul setiap wave Note: Mid Game
    private int bossSpawnInterval = 2; // Boss muncul setiap 2 wave (default) Note: Late Game

    private bool isSpawning = false;

    private Camera mainCamera;


    void Start()
    {
        mainCamera = Camera.main;
        UpdateUI();
        StartCoroutine(HandlePhases());
        StartLevel1Check();
    }

    void Update()
    {
        UpdateMoneyText();
    }

    void StartLevel1Check()
    {
        if (currentPhase == 1 && currentWave == 1)
        {
            // Start level 1
            SaveManager.instance.attackSpeedClick = 3f;
            SaveManager.instance.health = 100f;
            SaveManager.instance.attackDamage = 10f;
            SaveManager.instance.regenRate = 0.1f;
            SaveManager.instance.Save();
        }
    }

    void UpdateMoneyText()
    {
        moneyText.text = "Money: " + SaveManager.instance.money;
    }

    void UpdateUI()
    {
        phaseText.text = "Phase: " + currentPhase;
        waveText.text = "Wave: " + currentWave;
    }

    IEnumerator HandlePhases()
    {
        while (true)
        {
            isSpawning = true;

            // Atur jumlah musuh berdasarkan fase
            if (currentPhase <= 3)
            {
                enemiesPerWave = 10; // Phase 1-3: 10 musuh biasa per wave
            }
            else if (currentPhase <= 6)
            {
                enemiesPerWave = 15; // Phase 4-6: 15 musuh biasa per wave
            }
            else
            {
                enemiesPerWave = 23; // Phase 6 ke atas: 23 musuh biasa per wave
            }

            // Pilih spawn delay berdasarkan fase
            float currentSpawnDelay = GetSpawnDelay();

            // Spawn musuh biasa
            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy(enemyPrefabs);
                yield return new WaitForSeconds(currentSpawnDelay); // Delay antar spawn
            }

            // Spawn mini boss jika sesuai aturan
            if (currentPhase >= 4 && (currentWave % miniBossSpawnInterval == 0))
            {
                SpawnEnemy(miniBossPrefabs);
            }

            // Spawn boss jika sesuai aturan
            if (currentPhase >= 6)
            {
                if (currentPhase >= 9 || (currentWave % bossSpawnInterval == 0))
                {
                    SpawnEnemy(bossPrefabs);
                }
            }

            // Tunggu hingga semua musuh pada wave ini dihancurkan
            while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            {
                yield return null;
            }

            // Tunggu 30 detik untuk istirahat sebelum memulai fase berikutnya
            yield return new WaitForSeconds(10f);

            // Selesai wave
            currentWave++;

            // Periksa jika fase perlu diubah
            if (currentWave > 5) // Maksimal 5 wave per fase
            {
                currentPhase++;
                currentWave = 1;

                // Atur interval spawn boss berdasarkan fase
                if (currentPhase >= 9)
                {
                    bossSpawnInterval = 1; // Boss setiap wave pada phase 9 ke atas
                }
            }

            UpdateUI();
        }
    }

    float GetSpawnDelay()
    {
        if (currentPhase <= 3)
        {
            return spawnDelayPhase1to3; // Delay untuk phase 1-3
        }
        else if (currentPhase <= 6)
        {
            return spawnDelayPhase4to6; // Delay untuk phase 4-6
        }
        else
        {
            return spawnDelayPhase6Plus; // Delay untuk phase 6 ke atas
        }
    }

    void SpawnEnemy(GameObject[] prefabArray)
    {
        // Pilih prefab secara random
        GameObject prefab = prefabArray[Random.Range(0, prefabArray.Length)];

        // Tentukan posisi spawn secara random di sisi kamera
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Spawn musuh
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Dapatkan ukuran kamera
        float screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float screenTop = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        float screenBottom = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        // Tentukan sisi random untuk spawn (kiri, kanan, atas, bawah)
        int side = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;

        switch (side)
        {
            case 0: // Kiri
                spawnPosition = new Vector3(screenLeft, Random.Range(screenBottom, screenTop), 0);
                break;
            case 1: // Kanan
                spawnPosition = new Vector3(screenRight, Random.Range(screenBottom, screenTop), 0);
                break;
            case 2: // Atas
                spawnPosition = new Vector3(Random.Range(screenLeft, screenRight), screenTop, 0);
                break;
            case 3: // Bawah
                spawnPosition = new Vector3(Random.Range(screenLeft, screenRight), screenBottom, 0);
                break;
        }

        return spawnPosition;
    }
}
