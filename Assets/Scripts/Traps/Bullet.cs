using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{

     public float speed { get; set; }

     [SerializeField]
     private LayerMask _collisionMask;

     private Animator _anim;

     private void Start()
     {
          _anim = GetComponentInChildren<Animator>();
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
          RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, movement.magnitude * 2, _collisionMask);

          if (hit.collider != null)
          {
               DestroyBullet();
          }
     }

     private void OnTriggerEnter2D(Collider2D collision)
     {
          // Es mejor usar un trigger collider para matar al player
          if (collision.transform.CompareTag("Player"))
          {
               //KillPlayer();
               DestroyBullet();
          }
     }

     private void DestroyBullet()
     {
          speed = 0f;
          // queda disponible en el Pool
          StartCoroutine(DestroyOnDelay());
     }

     IEnumerator DestroyOnDelay()
     {
          if (_anim != null)
          {
               _anim.SetTrigger("Explode");
          }
          float duracionAnimacion = _anim.GetCurrentAnimatorStateInfo(0).length - 0.5f;
          yield return new WaitForSeconds(duracionAnimacion);

          gameObject.SetActive(false);
     }
}
