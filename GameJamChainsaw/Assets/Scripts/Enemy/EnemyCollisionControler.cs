using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionControler : MonoBehaviour
{
    public Enemy enemyScriptable;
    private int currentHealth;
    private SpawnerController spawnerController;                        // Référence sur le script du spawner.

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
}
