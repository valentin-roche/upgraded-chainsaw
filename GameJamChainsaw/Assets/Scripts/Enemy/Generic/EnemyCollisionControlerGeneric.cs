using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionControlerGeneric : MonoBehaviour
{
    public Enemy enemyScriptable;
    private int currentHealth;
    private SpawnerController spawnerController;                            // Référence sur le script du spawner.
    private PlayerCollisionController playerCollisionController = null;
    private SpriteRenderer sprite;                                          // Référence sur le sprite

    void Start()
    {
        currentHealth = enemyScriptable.health;
        spawnerController = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerController>();
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        foreach (GameObject door in doors)
        {
            Physics2D.IgnoreCollision(door.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
        sprite = GetComponentInChildren<SpriteRenderer>();
        switch(enemyScriptable.color)
        {
            case Colors.White:
                sprite.color = Color.white;
                break;
            case Colors.Red:
                sprite.color = Color.red;
                break;
            case Colors.Green:
                sprite.color = Color.green;
                break;
            case Colors.Blue:
                sprite.color = Color.blue;
                break;
            case Colors.Pink:
                sprite.color = Color.magenta;
                break;
        }
    }

    public void GetHit(int damage, Colors projectileColor)
    {
        print("enemy : " + enemyScriptable.color);
        print("projectileColor : " + projectileColor);
        if (enemyScriptable.color == projectileColor || enemyScriptable.color == Colors.White)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
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
