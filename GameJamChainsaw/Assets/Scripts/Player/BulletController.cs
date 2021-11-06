using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;                                                 // Le RigidBody du projectile

    public float speed;                                                     // La vitesse du projectile
    public float lifetime = 3f;                                             // La duree de vie
    public int damage;                                                      // Les dégats du projectile

    private Colors color;

    private SpriteRenderer sprite;                                          // Référence sur le sprite

    private bool dead = false;

    private void Start()
    {
        rb = gameObject.GetComponentInChildren<Rigidbody2D>();
        rb.velocity = -transform.up * speed;

        sprite = GetComponentInChildren<SpriteRenderer>();
        switch (color)
        {
            case Colors.White:
                sprite.color = Color.white;
                break;
            case Colors.Red:
                sprite.color = Color.red;
                break;
            case Colors.Green:
                sprite.color = Color.green;
                break;
            case Colors.Blue:
                sprite.color = Color.blue;
                break;
            case Colors.Pink:
                sprite.color = Color.magenta;
                break;
        }
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Die();    
        }
    }

    public void Die()
    {
        dead = true;
        Destroy(gameObject);
    }

    public void SetColor(Colors color)
    {
        this.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(!dead)
            {
                collision.GetComponent<EnemyCollisionControlerGeneric>().GetHit(damage, color);
                Die();
            }
        }
        if (collision.CompareTag("Wall") || collision.CompareTag("Door"))
        {
            Die();
        }
    }
}
