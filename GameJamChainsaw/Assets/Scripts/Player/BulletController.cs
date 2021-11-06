using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;                                                 // Le RigidBody du projectile

    public float speed;                                                     // La vitesse du projectile
    //public float lifetime = 3f;                                             // La duree de vie
    public int damage;                                                      // Les dégats du projectile

    private void Start()
    {
        rb = gameObject.GetComponentInChildren<Rigidbody2D>();
        rb.velocity = -transform.up * speed;
    }

    public void SetSpeedToZero()
    {
        rb.velocity = Vector2.zero;
    }

    /*private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Die();    
        }
    }*/

}
