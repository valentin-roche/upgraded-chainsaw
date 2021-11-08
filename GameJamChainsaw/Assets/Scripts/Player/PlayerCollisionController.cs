using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    public int health = 3;
    private bool dead = false;
    private int currentHealth;
    public float invincibilityTime;
    private float invincibilityTimeLeft;

    private Animator eyeAnimator;               // Référence sur l'animator des yeux
    private Animator bodyAnimator;              // Référence sur l'animator du body

    public Canvas gameOverCanvas;

    private GameObject ass;
    private GameObject wings;
    private PlayerMovementController playerMovementController;
    private PlayerRotationController playerRotationController;

    private List<ProjectorController> listProjector = new List<ProjectorController>();

    [SerializeField]
    private WaveNumberUpdater gameOverWaveUpdater;

    void Start()
    {
        ass = GameObject.FindGameObjectWithTag("Ass");
        wings = GameObject.FindGameObjectWithTag("Wings");
        playerMovementController = GetComponent<PlayerMovementController>();
        playerRotationController = GetComponent<PlayerRotationController>();
        eyeAnimator = GameObject.FindGameObjectWithTag("Eye").GetComponent<Animator>();
        bodyAnimator = GameObject.FindGameObjectWithTag("Body").GetComponent<Animator>();
        currentHealth = health;
        invincibilityTimeLeft = 0;
    }

    public void GetHit()
    {
        if(invincibilityTimeLeft <= 0)
        {
            currentHealth -= 1;

            if (currentHealth <= 0 && !dead)
            {
                Die();
            }

            invincibilityTimeLeft = invincibilityTime;
        }
    }

    private void Die()
    {
        dead = true;
        eyeAnimator.SetBool("deadEye", true);
        bodyAnimator.SetBool("isDead", true);
        ass.SetActive(false);
        wings.SetActive(false);
        playerMovementController.enabled = false;
        playerRotationController.enabled = false;
        GameOver();
    }
    void GameOver()
    {
        gameOverCanvas.gameObject.SetActive(true);
        gameOverWaveUpdater.SetWaveNumber(GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerController>().GetCurrentWaveIndex() + 1);
    }
    void Update()
    {
        invincibilityTimeLeft -= Time.deltaTime;
    }

    public float GetInvincibilityTimeLeft()
    {
        return invincibilityTimeLeft;
    }

    public void ChangeColor(SpriteColor color)
    {
        if(!dead)
            GetComponentInChildren<ShootingAssController>().ChangeColor(color);
    }

    public void RemoveProjector(ProjectorController projo)
    {
        listProjector.Remove(projo);
        CalculateColor();
    }

    public void AddProjector(ProjectorController projo)
    {
        listProjector.Add(projo);
        CalculateColor();
    }

    public void CalculateColor()
    {
        SpriteColor colorToChangeTo = new SpriteColor();
        if(listProjector.Count == 0)
        {
            colorToChangeTo.color = Colors.White;
            CodeRGB rgb;
            rgb.r = 255;
            rgb.g = 255;
            rgb.b = 255;
            colorToChangeTo.rgbCode = rgb;
        }
        else
        {
            colorToChangeTo = listProjector[0].GetActualColor();
        }

        ChangeColor(colorToChangeTo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GetHit();
        }
    }
}
