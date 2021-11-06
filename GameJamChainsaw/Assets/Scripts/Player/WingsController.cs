using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsController : MonoBehaviour
{
    [SerializeField]
    private GameObject wind;                 // Reference sur le projectile qu'on va envoyer
    [SerializeField]
    private Transform playerTransform;       // Référence sur le transform du joueur pour tirer dans la bonne direction

    public float startReloadingTimeWings;        // Durée de rechargement du coup d'ailes
    private float reloadingTimeWings;            // Durée restante de rechargement du coup d'ailes

    // Start is called before the first frame update
    void Start()
    {
        reloadingTimeWings = startReloadingTimeWings;
    }

    // Update is called once per frame
    void Update()
    {
        if (reloadingTimeWings < startReloadingTimeWings)
        {
            reloadingTimeWings -= Time.deltaTime;
            if (reloadingTimeWings <= 0)
            {
                reloadingTimeWings = startReloadingTimeWings;
            }
        }

        if (Input.GetMouseButtonDown(1) && reloadingTimeWings == startReloadingTimeWings)
        {
            GameObject projectile = Instantiate(wind, playerTransform.position, playerTransform.rotation);
            reloadingTimeWings -= Time.deltaTime;
        }
    }
}
