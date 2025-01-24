using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float health;
    public float currentHealth;

    private void Start()
    {
        currentHealth = health;
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
    }
}
