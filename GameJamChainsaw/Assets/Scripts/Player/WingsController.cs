using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WingsController : MonoBehaviour
{
    [SerializeField]
    private GameObject wind;                 // Reference sur le projectile qu'on va envoyer
    [SerializeField]
    private Transform playerTransform;       // R�f�rence sur le transform du joueur pour tirer dans la bonne direction
    [SerializeField]
    private Transform startingPoint;        // Position de d�part du vent

    public float startReloadingTimeWings;        // Dur�e de rechargement du coup d'ailes
    private float reloadingTimeWings;            // Dur�e restante de rechargement du coup d'ailes

    public TextMeshProUGUI cooldownTextMesh;
    public Image cooldownImage;

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
            cooldownImage.fillAmount = reloadingTimeWings / startReloadingTimeWings;
            if (reloadingTimeWings <= 0)
            {
                cooldownImage.fillAmount = 1;
                reloadingTimeWings = startReloadingTimeWings;
            }
        }

        if (Input.GetMouseButtonDown(1) && reloadingTimeWings == startReloadingTimeWings)
        {
            GameObject projectile = Instantiate(wind, startingPoint.position, playerTransform.rotation);
            reloadingTimeWings -= Time.deltaTime;
        }
        HandleTextChange();
    }

    public void ToggleMoveAnimation(bool toggle)
    {
        foreach (Animator animator in wingsAnimator)
        {
            animator.SetBool("isMoving", toggle);
        }
    }
    public void HandleTextChange()
    {
        cooldownTextMesh.gameObject.SetActive(true);
        cooldownTextMesh.text = reloadingTimeWings.ToString().Substring(0,1);
        if (reloadingTimeWings == startReloadingTimeWings)
        {
            cooldownTextMesh.gameObject.SetActive(false);
        }
    }
}
