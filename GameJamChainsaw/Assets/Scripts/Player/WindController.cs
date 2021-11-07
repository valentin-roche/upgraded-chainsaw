using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;                                                     // La vitesse du projectile
    public float lifetime = 3f;                                             // La duree de vie
    public float windEffectTime;
    public float pushFactor;
    public float timeWhenHittingWall;                                       // Le temps avant que le projectile se détruise quand il rencontre un mur

    [SerializeField]
    private Collider2D colliderToIgnore;

    void Start()
    {
        GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in ennemies)
        {
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), colliderToIgnore);
        }

        rb = gameObject.GetComponentInChildren<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBoxEnemy"))
        {
            collision.GetComponent<EnemyCallGetHit>().GetPushed(windEffectTime, pushFactor);
        }
        if (collision.CompareTag("Wall") || collision.CompareTag("Door"))
        {
            lifetime = timeWhenHittingWall;

            /*Vector3 direction = collision.transform.position - transform.position;
            float dot = Vector2.Dot(rb.velocity.normalized, (Vector2)direction.normalized);
            print(dot);

            // If on va dans la direction du mur
            rb.velocity = Vector2.zero;*/
        }
    }
}
