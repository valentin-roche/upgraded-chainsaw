using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementControllerGeneric : MonoBehaviour
{
    Transform playerTransform;              //transform du joueur
    Rigidbody2D rb;                         //rigidbody de l'ennemy (gameObject actuel)
    private Vector2 movement;               //pour faire bouger l'ennemi
    public Enemy enemyScriptable;

    void Start()
    {
        // Récupération des références
        rb = gameObject.GetComponent<Rigidbody2D>(); 
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
    }

    void Update()
    {
        if(playerTransform != null)
        {
            Vector3 direction = playerTransform.position - transform.position; 
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
            rb.rotation = angle; 
            direction.Normalize();
            movement = direction;
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + (movement * enemyScriptable.speed * Time.deltaTime)); 
    }
}
