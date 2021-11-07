using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPivotController : MonoBehaviour
{
    public void SetShouldAttackToFalse()
    {
        GetComponent<Animator>().SetBool("shouldAttack", false);
    }
}
