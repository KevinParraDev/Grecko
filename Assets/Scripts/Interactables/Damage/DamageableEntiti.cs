using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageableEntiti : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float TotalLifePoits = 5;
    private float ActualLifePoits;
    public GameObject playerGO;

    private void Awake()
    {
        ActualLifePoits = TotalLifePoits;
    }

    public abstract void TakeDamage();
    public abstract void Death();

    public void Damage(float damageForce)
    {
        if(ActualLifePoits > 0)
        {
            TakeDamage();
            if (ActualLifePoits - damageForce >= 0)
                ActualLifePoits -= damageForce;
            else
            {
                ActualLifePoits = 0;
                Death();
                Debug.Log("Entidad Muerta");
            }
            Debug.Log("Vida restante: " + ActualLifePoits);
        }
    }
}
