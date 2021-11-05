using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionControler : MonoBehaviour
{
    public Enemy enemyScriptable;
    private int currentHealth;

    void Start()
    {
        currentHealth = enemyScriptable.health;
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
    }
}
