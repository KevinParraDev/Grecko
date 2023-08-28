using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Killer
{
     
     public float speed { get; set; }

     [SerializeField]
     private LayerMask _collisionMask;

     private Animator _anim;

     private void Start()
     {
          _anim = GetComponent<Animator>();
     }

     private void FixedUpdate()
     {
          Vector3 dir = transform.up;
          Vector3 movement = dir * speed;
          transform.position += movement;

          CheckCollision(movement);
     }

     private void CheckCollision(Vector3 movement)
     {
          RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, movement.magnitude, _collisionMask);

          if (hit.collider != null)
          {
               if (hit.transform.CompareTag("Player"))
               {
                    KillPlayer();
               }

               // TODO: Activación de la animación de impacto de la bala

               DestroyBullet();
          }
     }

     private void DestroyBullet()
     {
          if (_anim != null)
          {
               _anim.SetTrigger("Destroy");
          }
          // queda disponible en el Pool
          gameObject.SetActive(false);
     }
}
