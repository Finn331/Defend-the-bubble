using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float health; // Darah musuh
    public float currentHealth; // Darah musuh saat ini
    [SerializeField] int droppedMoney; // Uang yang dijatuhkan musuh

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
            Destroy(collision.gameObject);
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
        SaveManager.instance.money += droppedMoney;
        SaveManager.instance.Save();
        Destroy(gameObject);
    }
}