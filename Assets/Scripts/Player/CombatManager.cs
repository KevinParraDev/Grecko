using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private float hitRange = 1;
    [SerializeField] private Transform attackZone;

    public void Hit(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackZone.position, hitRange);
            
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.GetComponent<DamageableEntiti>() != null)
                {
                    enemy.GetComponent<DamageableEntiti>().Damage(2, transform.position);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackZone.position, hitRange);
    }
}
