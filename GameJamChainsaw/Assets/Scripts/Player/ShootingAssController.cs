using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAssController : MonoBehaviour
{
    [SerializeField]
    private GameObject shot;                 // Reference sur l'objet projectile qu'on va envoyer
    [SerializeField]
    private GameObject firingPoint;          // La position de depart du projectile (le bout du pistolet)
    [SerializeField]
    private Transform playerTransform;       // Référence sur le transform du joueur pour tirer dans la bonne direction

    private Animator assAnimator;           // Référence sur l'animator du cul
    private Animator eyeAnimator;           // Référence sur l'animator des yeux

    public SpriteColor color;     // La couleur de la balle

    public float startReloadingTime;        // Durée de rechargement
    private float reloadingTime;            // Durée restante de rechargement 
    
    // Start is called before the first frame update
    void Start()
    {
        assAnimator = GetComponent<Animator>();
        eyeAnimator = GameObject.FindGameObjectWithTag("Eye").GetComponent<Animator>();

        // Initialisation du timer
        reloadingTime = startReloadingTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Si on est en cooldown, on décrémente la durée
        if (reloadingTime < startReloadingTime)
        {
            reloadingTime -= Time.deltaTime;
            if (reloadingTime <= 0)
            {
                reloadingTime = startReloadingTime;
            }
        }

        // Si on tire et qu'on est pas en train de recharger, alors on tire vraiment
        if (Input.GetMouseButtonDown(0) && reloadingTime == startReloadingTime)
        {
            Shoot();
            assAnimator.SetBool("shouldShoot", true);
            eyeAnimator.SetBool("angryEye", true);
        }
    }

    public void SetShouldShootToFalse()
    {
        assAnimator.SetBool("shouldShoot", false);
        eyeAnimator.SetBool("angryEye", false);
    }

    public void Shoot()
    {
        playerTransform.GetComponent<PlayerCollisionController>().CalculateColor();
        GameObject projectile = Instantiate(shot, firingPoint.transform.position, playerTransform.rotation);
        projectile.GetComponentInChildren<BulletCollisionController>().SetColor(color);
        reloadingTime -= Time.deltaTime;
	}

    public void ChangeColor(SpriteColor colorProjector)
    {
        color = colorProjector;
    }
}
