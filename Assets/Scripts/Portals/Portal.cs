using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
     [SerializeField]
     private Portal _destiny;

     [SerializeField]
     private int _ejectionSpeed;

     public void Teleport(Transform objTransform, Rigidbody2D objRigidBody)
     {
          objTransform.position = _destiny.transform.position + _destiny.transform.up;

          // Se requiere que objetos m�s r�pidos como balas mantengan la velocidad
          // Discutir como queremos lo de la ejeccion en cierta direccion
          if (objTransform.CompareTag("Bullet"))
          {
               objRigidBody.velocity = objRigidBody.velocity.magnitude * _destiny.transform.up;
               objTransform.SetPositionAndRotation(_destiny.transform.position, _destiny.transform.rotation);
          }
          else
          {
               objRigidBody.velocity = _destiny.transform.up * _ejectionSpeed;
          }



     }

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (Vector3.Distance(collision.transform.position, transform.position) > 0.5f)
          {
               Teleport(collision.transform, collision.attachedRigidbody);
          }
     }

}
