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

    public Colors color = Colors.White;    // La couleur de la balle

    public float startReloadingTime;        // Durée de rechargement
    private float reloadingTime;            // Durée restante de rechargement 
    

    // Start is called before the first frame update
    void Start()
    {
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
            GameObject projectile = Instantiate(shot, firingPoint.transform.position, playerTransform.rotation);
            projectile.GetComponent<BulletController>().SetColor(color);
            reloadingTime -= Time.deltaTime;
        }
    }
}
