using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float speed; // Kecepatan projectile
    [SerializeField] private float timeToDestroy; // Waktu sebelum projectile dihancurkan
    public float damage; // Damage yang diberikan projectile

    private Vector3 direction;
    public Animator anim;
    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    void Update()
    {
        damage = SaveManager.instance.attackDamage; // Ambil nilai damage dari SaveManager

        if (direction != Vector3.zero)
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        StartCoroutine(Destroy());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile"))
        {
            anim.SetTrigger("hit");
            
        }

        if (collision.CompareTag("Enemy"))
        {
            
            anim.SetTrigger("hit");
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
    
    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
