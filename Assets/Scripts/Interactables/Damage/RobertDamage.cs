using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobertDamage : DamageableEntiti
{
    public override void TakeDamage()
    {
        Debug.Log("Daño");

    }

    public override void Death()
    {
        Debug.Log("Morir");

    }
}
