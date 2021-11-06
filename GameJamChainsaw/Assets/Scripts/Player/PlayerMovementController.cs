using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // La fonction de d�placement et la mani�re de r�cup�rer l'input et d'utiliser la vitesse va d�pendre des animations qu'on utilise pour montrer le d�placement du joueur.

    [SerializeField]
    private float speed;                       // Vitesse de d�placement du joueur

    [SerializeField]
    private float dashSpeed;

    [SerializeField]
    private float dashTime;

    private Rigidbody2D rb;                    // Une r�f�rence sur le rigidbody2D du personnage pour pouvoir le d�placer

    private float mx;                          // L'input en x du joueur
    private float my;                          // L'input en y du joueur
    private Vector2 moveVelocity;              // Le Vector2 de d�placement correspondant � l'input de d�placement du joueur
    private Vector2 moveInput;                 // Le Vector2 correspondant � l'input de d�placement du joueur

    private bool dashing;
    public float dashReloadTime;
    private float reloadingTime;
    private Vector2 dashVelocity;
    private float dashTimeLeft;


    void Start()
    {
        // R�cup�ration des r�f�rences
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(dashTimeLeft <= 0)
        {
            dashing = false; 

            // R�cup�ration des inputs et calcul du d�placement appropri�
            mx = Input.GetAxisRaw("Horizontal");
            my = Input.GetAxisRaw("Vertical");
            moveInput = new Vector2(mx, my);
            moveVelocity = moveInput.normalized * speed;

            if (reloadingTime < dashReloadTime)
            {
                reloadingTime -= Time.deltaTime;
                if (reloadingTime <= 0)
                {
                    reloadingTime = dashReloadTime;
                }
            }

            if (Input.GetMouseButtonDown(2) && reloadingTime == dashReloadTime)
            {
                reloadingTime -= Time.deltaTime;
                dashing = true;
                dashVelocity = moveVelocity * dashSpeed;
                dashTimeLeft = dashTime;
            }
        }
        else
        {
            dashTimeLeft -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (!dashing)
        {
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + dashVelocity * Time.fixedDeltaTime);
        }
    }
}
