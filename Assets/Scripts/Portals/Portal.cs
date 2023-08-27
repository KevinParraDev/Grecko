using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
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

          // Se requiere que objetos más rápidos como balas mantengan la velocidad
          // Discutir como queremos lo de la ejeccion en cierta direccion
          if (objTransform.CompareTag("Bullet")){
               objRigidBody.velocity = objRigidBody.velocity.magnitude * _destiny.transform.up;
          }

          objRigidBody.velocity = _destiny.transform.up * _ejectionSpeed;

     }

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if(Vector3.Distance(collision.transform.position, transform.position) > 0.5f)
          {
               Teleport(collision.transform, collision.attachedRigidbody);
          }
     }
}
