using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
     [SerializeField]
     private Terminal _terminalToActive;

     private bool _isCollected = false;
     private Vector3 _initialPosition;

     private void Start()
     {
          _initialPosition = transform.position;
     }
     private void Collect()
     {
          _isCollected = true;
          _terminalToActive?.KeyUnlock();
     }

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.CompareTag("Player"))
          {
               transform.parent = collision.transform;
               transform.position = transform.parent.position + new Vector3(-0.5f, 0.5f, 0);

               // TODO: Solo puede coleccionarse si sale de la zona, si no se vuelve a soltar
               Collect();
          }
     }

     public void Drop()
     {
          transform.position = _initialPosition;
     }
}
