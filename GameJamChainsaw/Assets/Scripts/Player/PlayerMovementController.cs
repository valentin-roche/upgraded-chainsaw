using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private Animator bodyAnimator;             // Une r�f�rence sur l'animator du body.
    private WingsController wingsController;   // Une r�f�rence sur l'animator de l'aile gauche

    private float mx;                          // L'input en x du joueur
    private float my;                          // L'input en y du joueur
    private Vector2 moveVelocity;              // Le Vector2 de d�placement correspondant � l'input de d�placement du joueur
    private Vector2 moveInput;                 // Le Vector2 correspondant � l'input de d�placement du joueur

    private bool dashing;
    public float dashReloadTime;
    private float reloadingTime;
    private Vector2 dashVelocity;
    private float dashTimeLeft;

    public TextMeshProUGUI cooldownTextMesh;

    void Start()
    {
        // R�cup�ration des r�f�rences
        bodyAnimator = GameObject.FindGameObjectWithTag("Body").GetComponent<Animator>();
        wingsController = GetComponentInChildren<WingsController>();
        rb = GetComponent<Rigidbody2D>();
        reloadingTime = dashReloadTime;
        dashTimeLeft = dashTime;
    }

    void Update()
    {
        dashing = false; 

        // R�cup�ration des inputs et calcul du d�placement appropri�
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");

        if(mx == 0 && my == 0)
        {
            bodyAnimator.SetBool("isMoving", false);
            wingsController.ToggleMoveAnimation(false);
        }
        else
        {
            bodyAnimator.SetBool("isMoving", true);
            wingsController.ToggleMoveAnimation(true);
        }

        moveInput = new Vector2(mx, my);
        moveVelocity = moveInput.normalized * speed;

        if (dashTimeLeft < dashTime)
        {
            dashing = true;
            dashTimeLeft -= Time.deltaTime;
            if (dashTimeLeft <= 0)
            {
                dashTimeLeft = dashTime;
            }
        }
        else
            dashing = false;

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
            dashTimeLeft -= Time.deltaTime;
        }
        HandleTextChange();
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
    public void HandleTextChange()
    {
        cooldownTextMesh.gameObject.SetActive(true);
        cooldownTextMesh.text = reloadingTime.ToString().Substring(0, 1);
        if (reloadingTime == dashReloadTime)
        {
            cooldownTextMesh.gameObject.SetActive(false);
        }
    }
}
