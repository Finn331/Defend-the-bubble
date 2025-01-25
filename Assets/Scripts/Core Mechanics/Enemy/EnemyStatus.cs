using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float health; // Darah musuh
    public float currentHealth; // Darah musuh saat ini
    [SerializeField] int droppedMoney; // Uang yang dijatuhkan musuh
    [SerializeField] AudioClip dieSFX; // SFX ketika musuh mati
    private PlayerProjectile playerProjectile;
    void Start()
    {
        currentHealth = health;
    }

    void Update()
    {
        EnemyHealth();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerProjectile")
        {
            playerProjectile = collision.GetComponent<PlayerProjectile>();
            TakeDamage(playerProjectile.damage);
            
            Debug.Log("Enemy Health: ");
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void EnemyHealth()
    {
        if (currentHealth <= 1)
        {
            Die();
        }
    }

    public void Die()
    {
        AudioManager.instance.PlaySound(dieSFX);
        SaveManager.instance.money += droppedMoney;
        SaveManager.instance.Save();
        Destroy(gameObject);
    }
}