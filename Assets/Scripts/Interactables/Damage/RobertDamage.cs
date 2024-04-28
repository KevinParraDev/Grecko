using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobertDamage : DamageableEntiti
{
    public override void TakeDamage(Vector3 damageDi)
    {
        Debug.Log("Daño");

    }

    public override void Death()
    {
        Debug.Log("Morir");

    }
}
