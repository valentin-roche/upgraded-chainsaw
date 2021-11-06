using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsController : MonoBehaviour
{
    [SerializeField]
    private GameObject wind;                 // Reference sur le projectile qu'on va envoyer
    [SerializeField]
    private Transform playerTransform;       // R�f�rence sur le transform du joueur pour tirer dans la bonne direction

    public float startReloadingTimeWings;        // Dur�e de rechargement du coup d'ailes
    private float reloadingTimeWings;            // Dur�e restante de rechargement du coup d'ailes

    private Animator[] wingsAnimator;            // R�f�rences sur les animators des ailes

    void Start()
    {
        wingsAnimator = GetComponentsInChildren<Animator>();
        reloadingTimeWings = startReloadingTimeWings;
    }

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

    public void ToggleMoveAnimation(bool toggle)
    {
        foreach (Animator animator in wingsAnimator)
        {
            animator.SetBool("isMoving", toggle);
        }
    }
}
