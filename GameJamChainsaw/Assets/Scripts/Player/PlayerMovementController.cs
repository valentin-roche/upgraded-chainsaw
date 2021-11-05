using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // La fonction de déplacement et la manière de récupérer l'input et d'utiliser la vitesse va dépendre des animations qu'on utilise pour montrer le déplacement du joueur.

    [SerializeField]
    private float speed;                       // Vitesse de déplacement du joueur

    private Rigidbody2D rb;                    // Une référence sur le rigidbody2D du personnage pour pouvoir le déplacer

    private float mx;                          // L'input en x du joueur
    private float my;                          // L'input en y du joueur
    private Vector2 moveVelocity;              // Le Vector2 de déplacement correspondant à l'input de déplacement du joueur
    private Vector2 moveInput;                 // Le Vector2 correspondant à l'input de déplacement du joueur

    void Start()
    {
        // Récupération des références
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Récupération des inputs et calcul du déplacement approprié
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(mx, my);
        moveVelocity = moveInput.normalized * speed;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
