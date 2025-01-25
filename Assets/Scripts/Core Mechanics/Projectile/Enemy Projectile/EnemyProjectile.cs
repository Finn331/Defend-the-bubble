using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyProjectile : MonoBehaviour
{
    public float damage; // Damage yang diberikan projectile
    [SerializeField] private float speed; // Kecepatan projectile
    [SerializeField] float timeToDestroy; // Waktu sebelum projectile dihancurkan
    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (direction != Vector3.zero)
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        StartCoroutine(Destroy());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStatus>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.CompareTag("PlayerProjectile"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
}
