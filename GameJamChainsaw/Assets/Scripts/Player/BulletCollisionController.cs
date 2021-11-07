using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionController : MonoBehaviour
{
    private BulletController bulletController;              // Référence sur le bulletController
    private Colors color;

    private SpriteRenderer sprite;                                          // Référence sur le sprite
    private Animator animator;                                              // Référence sur l'animator

    private bool dead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        bulletController = GetComponentInParent<BulletController>();

        sprite = GetComponentInChildren<SpriteRenderer>();
        switch (color)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBoxEnemy"))
        {
            if (!dead)
            {
                collision.GetComponent<EnemyCallGetHit>().GetHit(bulletController.damage, color);
                Hit();
            }
        }
        if (collision.CompareTag("Wall") || collision.CompareTag("Door"))
        {
            Hit();
        }
    }

    public void SetColor(Colors color)
    {
        this.color = color;
    }

    public Colors GetColor()
    {
        return color;
    }

    public void Hit()
    {
        dead = true;
        animator.SetBool("gotHit", true);
        bulletController.SetSpeedToZero();
        GetComponent<Collider2D>().enabled = false;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public bool GetIsDead()
    {
        return dead;
    }
}
