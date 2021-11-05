using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    public int health = 3;
    private int currentHealth;
    public float invincibilityTime;
    private float invincibilityTimeLeft;

    void Start()
    {
        currentHealth = health;
        invincibilityTimeLeft = 0;
    }

    public void GetHit()
    {
        if(invincibilityTimeLeft <= 0)
        {
            currentHealth -= 1;

            if (currentHealth <= 0)
            {
                Die();
            }

            invincibilityTimeLeft = invincibilityTime;

            print("pas invincible");
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        invincibilityTimeLeft -= Time.deltaTime;
    }

    public float GetInvincibilityTimeLeft()
    {
        return invincibilityTimeLeft;
    }
}
