using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // La fonction de d�placement et la mani�re de r�cup�rer l'input et d'utiliser la vitesse va d�pendre des animations qu'on utilise pour montrer le d�placement du joueur.

    [SerializeField]
    private float speed;                       // Vitesse de d�placement du joueur

    private Rigidbody2D rb;                    // Une r�f�rence sur le rigidbody2D du personnage pour pouvoir le d�placer

    private float mx;                          // L'input en x du joueur
    private float my;                          // L'input en y du joueur
    private Vector2 moveVelocity;              // Le Vector2 de d�placement correspondant � l'input de d�placement du joueur
    private Vector2 moveInput;                 // Le Vector2 correspondant � l'input de d�placement du joueur

    void Start()
    {
        // R�cup�ration des r�f�rences
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // R�cup�ration des inputs et calcul du d�placement appropri�
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
