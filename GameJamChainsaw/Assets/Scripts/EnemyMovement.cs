using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform playerTransform;  //transform du joueur
    float moveSpeed = 2f; //vitesse de déplacement
    Rigidbody2D rb; //rigidbody de l'ennemy (gameObject actuel)
    private Vector2 movement; //pour faire bouger l'ennemi
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>(); // cherche le rigidbody2D du gameObject actuel
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //va chercher le transform du player
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = playerTransform.position - transform.position; //distance entre le player et l'ennemi
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //calcule l'angle dans lequel il doit se tourner
        rb.rotation = angle; //change l'angle du rigidbody par rapport au calcul du dessus
        direction.Normalize(); 
        movement = direction;
        //transform.position = playerTransform.position;
    }
    private void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + (movement * moveSpeed * Time.deltaTime)); //bouge l'ennemi
    }
}
