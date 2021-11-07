using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMovementAI;

public class EnemyCollisionControlerGeneric : MonoBehaviour
{
    public Enemy enemyScriptable;
    private int currentHealth;
    private SpawnerController spawnerController;                            // Référence sur le script du spawner.
    private PlayerCollisionController playerCollisionController = null;
    private SpriteRenderer[] sprites;                                          // Référence sur les sprites
    private SteeringBasics enemyMovementController = null;                  // Référence sur le script de mouvement

    [SerializeField]
    private Animator armAnimator;                                           // Référence sur l'animator des bras
    [SerializeField]
    private Animator hitAnimator;                                           // Référence sur l'animator du hit

    public float startTimeBetweenAttack = 1f;
    private float timeBetweenAttack;

    void Start()
    {
        timeBetweenAttack = startTimeBetweenAttack;
        currentHealth = enemyScriptable.health;
        spawnerController = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerController>();
        enemyMovementController = GetComponent<SteeringBasics>();
        sprites = GetComponentsInChildren<SpriteRenderer>();

        foreach(SpriteRenderer sprite in sprites)
        {
            if(!sprite.CompareTag("Ouin"))
            {
                sprite.color = new Color(enemyScriptable.color.rgbCode.r/255f, enemyScriptable.color.rgbCode.g/255f, enemyScriptable.color.rgbCode.b/255f);
            }
        }
    }

    public void GetHit(int damage, Colors projectileColor)
    {

        if (enemyScriptable.color.color == projectileColor || enemyScriptable.color.color == Colors.White)
        {
            currentHealth -= damage;

            hitAnimator.SetBool("shouldGetHit", true);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        spawnerController.DeathOfEnnemy(enemyScriptable.id);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCollisionController = collision.GetComponent<PlayerCollisionController>();
            enemyMovementController.SetHasPlayerInRange(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCollisionController = null;
            enemyMovementController.SetHasPlayerInRange(false);
        }
    }

    void Update()
    {
        if(playerCollisionController != null)
        {
            if(timeBetweenAttack == startTimeBetweenAttack)
            {
                armAnimator.SetBool("shouldAttack", true);
                timeBetweenAttack -= Time.deltaTime;
            }
        }

        if(timeBetweenAttack < startTimeBetweenAttack)
        {
            timeBetweenAttack -= Time.deltaTime;
            if(timeBetweenAttack <= 0)
            {
                timeBetweenAttack = startTimeBetweenAttack;
            }
        }
    }

    public void GetPushed(float windEffectTime, float pushFactor)
    {
        GetComponent<SteeringBasics>().ApplyPushEffect(windEffectTime, pushFactor);
    }
}
