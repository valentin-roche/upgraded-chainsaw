using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionController : MonoBehaviour
{
    private BulletController bulletController;              // Référence sur le bulletController
    private SpriteColor color;

    private SpriteRenderer sprite;                                          // Référence sur le sprite
    private Animator animator;                                              // Référence sur l'animator

    private bool dead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        bulletController = GetComponentInParent<BulletController>();

        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.color = new Color(color.rgbCode.r / 255f, color.rgbCode.g / 255f, color.rgbCode.b / 255f, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBoxEnemy"))
        {
            if (!dead)
            {
                collision.GetComponent<EnemyCallGetHit>().GetHit(bulletController.damage, color.color);
                Hit();
            }
        }
        if (collision.CompareTag("Wall") || collision.CompareTag("Door"))
        {
            Hit();
        }
    }

    public void SetColor(SpriteColor color)
    {
        this.color = color;
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.color = new Color(color.rgbCode.r / 255f, color.rgbCode.g / 255f, color.rgbCode.b / 255f, 1f);
    }

    public SpriteColor GetColor()
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
        bulletController.Die();
        Destroy(gameObject);
    }

    public bool GetIsDead()
    {
        return dead;
    }
}
