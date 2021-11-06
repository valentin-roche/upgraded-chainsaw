using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionController : MonoBehaviour
{
    private BulletController bulletController;              // Référence sur le bulletController
    private Colors color;

    private SpriteRenderer sprite;                                          // Référence sur le sprite

    private void Start()
    {
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
        if (collision.CompareTag("Enemy"))
        {
            if (!bulletController.GetIsDead())
            {
                collision.GetComponent<EnemyCollisionControlerGeneric>().GetHit(bulletController.damage, color);
                bulletController.Die();
            }
        }
        if (collision.CompareTag("Wall") || collision.CompareTag("Door"))
        {
            bulletController.Die();
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
}
