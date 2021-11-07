using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCallGetHit : MonoBehaviour
{
    public void GetHit(int damage, Colors projectileColor)
    {
        GetComponentInParent<EnemyCollisionControlerGeneric>().GetHit(damage, projectileColor);
    }

    public void GetPushed(float windEffectTime, float pushFactor)
    {
        GetComponentInParent<EnemyCollisionControlerGeneric>().GetPushed(windEffectTime, pushFactor);
    }

    public void SetShouldGetHitToFalse()
    {
        GetComponent<Animator>().SetBool("shouldGetHit", false);
    }
}
