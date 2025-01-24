using UnityEngine;

public class WallHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Wall menerima damage: " + damage);

        if (currentHealth <= 0)
        {
            Debug.Log("Wall hancur!");
            Destroy(gameObject); // Hancurkan Wall
        }
    }
}
