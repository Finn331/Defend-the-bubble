using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float health; // Health maksimum player
    public float currentHealth; // Health saat ini
    public float regenerationRate; // Jumlah regenerasi health per detik
    public float regenerationDelay; // Waktu jeda sebelum regenerasi mulai
    private float lastRegenerationTime; // Waktu terakhir regenerasi

    private Attack attack; // Referensi ke komponen Attack

    private void Awake()
    {
        // Ambil komponen Attack dari GameObject ini
        attack = GetComponent<Attack>();
    }

    private void Start()
    {
        // Set currentHealth ke nilai awal
        currentHealth = health;

        if (currentHealth == 100)
        {
            // Pastikan komponen Attack sudah diambil
            attack.enabled = true;
        }

        lastRegenerationTime = Time.time; // Set waktu pertama kali
    }

    private void Update()
    {
        health = SaveManager.instance.health; // Ambil nilai health dari SaveManager


        // Mengecek apakah regenerasi health dapat dimulai
        if (Time.time - lastRegenerationTime >= regenerationDelay)
        {
            RegenerateHealth();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Player menerima damage: " + damage);

        if (currentHealth <= 0)
        {
            Debug.Log("Player mati!");
            // Tambahkan logika jika Player mati
        }

        // Reset waktu regenerasi setelah menerima damage
        lastRegenerationTime = Time.time;
    }

    private void RegenerateHealth()
    {
        regenerationRate = SaveManager.instance.regenRate;
        // Regenerasi health hanya jika currentHealth < health maksimum
        if (currentHealth < health)
        {
            currentHealth += regenerationRate * Time.deltaTime;

            // Pastikan health tidak lebih dari maksimum
            if (currentHealth > health)
            {
                currentHealth = health;
            }

            Debug.Log("Regenerasi Health: " + currentHealth);
        }
    }
}
