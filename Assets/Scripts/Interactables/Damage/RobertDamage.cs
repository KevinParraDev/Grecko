using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobertDamage : DamageableEntiti
{
    public override void TakeDamage(Vector3 damageDi)
    {
        Debug.Log("Da�o");

    }

    public override void Death()
    {
        Debug.Log("Morir");

    }
}
