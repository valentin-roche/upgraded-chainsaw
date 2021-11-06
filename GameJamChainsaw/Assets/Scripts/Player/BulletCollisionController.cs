using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionController : MonoBehaviour
{
    private BulletController bulletController;              // Référence sur le bulletController

    private void Start()
    {
        bulletController = GetComponentInParent<BulletController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!bulletController.GetIsDead())
            {
                collision.GetComponent<EnemyCollisionControlerGeneric>().GetHit(bulletController.damage, bulletController.GetColor());
                bulletController.Die();
            }
        }
        if (collision.CompareTag("Wall") || collision.CompareTag("Door"))
        {
            bulletController.Die();
        }
    }
}
