using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionControler : MonoBehaviour
{
    public Enemy enemyScriptable;
    private int currentHealth;
    private SpawnerController spawnerController;                        // Référence sur le script du spawner.
    private PlayerCollisionController playerCollisionController = null;

    void Start()
    {
        currentHealth = enemyScriptable.health;
        spawnerController = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerController>();
    }

    public void GetHit(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        spawnerController.DeathOfEnnemy(enemyScriptable.id);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCollisionController = collision.GetComponent<PlayerCollisionController>();
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCollisionController = null;
        }
    }

    void Update()
    {
        if(playerCollisionController != null)
        {
            if(playerCollisionController.GetInvincibilityTimeLeft() <= 0)
            {
                playerCollisionController.GetHit();
            }
        }
    }
}
