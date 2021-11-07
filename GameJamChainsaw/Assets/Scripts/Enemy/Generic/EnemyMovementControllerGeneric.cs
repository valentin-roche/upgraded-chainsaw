using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementControllerGeneric : MonoBehaviour
{
    private Transform playerTransform;              
    private Rigidbody2D rb;                         
    private Vector2 movement;
    private bool hasPlayerInRange = false;

    [SerializeField]
    private Animator feetAnimator = null;                  // Référence sur l'animateur des pieds slurps

    public Enemy enemyScriptable;

    private float windDurationLeft;
    private float pushFactor;

    void Start()
    {
        // Récupération des références
        rb = gameObject.GetComponent<Rigidbody2D>(); 
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
    }

    void Update()
    {
        if(playerTransform != null)
        {
            Vector3 direction = playerTransform.position - transform.position; 
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
            rb.rotation = angle; 
            direction.Normalize();

            if (windDurationLeft <= 0)
            {
                if (!hasPlayerInRange)
                {
                    movement = direction;
                }
                else
                {
                    movement = Vector2.zero;
                }

                if (feetAnimator != null)
                    feetAnimator.SetFloat("magnitude", movement.magnitude);
                else
                    print("Il faut set l'animator des pieds dans EnemyMovementController");
            }
            else
            {
                windDurationLeft -= Time.deltaTime;
                movement = - direction * pushFactor;
            }
        }
    }
    private void FixedUpdate()
    {
        if (windDurationLeft <= 0)
        {
            rb.MovePosition((Vector2)transform.position + (movement * enemyScriptable.speed * Time.deltaTime));
        }
        else
        {
            rb.MovePosition((Vector2)transform.position + (movement * Time.deltaTime));
        }
    }

    public void ApplyPushEffect(float windEffectTime, float pushingFactor)
    {
        windDurationLeft += windEffectTime;
        pushFactor = pushingFactor;
    }

    public void SetHasPlayerInRange(bool inRange)
    {
        hasPlayerInRange = inRange;
    }
}
