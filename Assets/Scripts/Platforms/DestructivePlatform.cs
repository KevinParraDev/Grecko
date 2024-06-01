using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructivePlatform : MonoBehaviour
{

     [SerializeField]
     private float _reaparitionTime = 3f;

     private Animator _animator;

     private Renderer _spriteRenderer;

     private BoxCollider2D _boxCollider;

     private ParticleSystem _particleSystem;

     private bool _platformEnable = true;

     private Vector3 _downPosition;
     private Vector3 _initialPosition;

     private void Awake()
     {
          _animator = GetComponent<Animator>();
          _spriteRenderer = GetComponent<Renderer>();
          _boxCollider = GetComponent<BoxCollider2D>();
          _particleSystem = GetComponentInChildren<ParticleSystem>();

          _downPosition = new Vector3(transform.position.x, transform.position.y - 0.15f, transform.position.z);
          _initialPosition = transform.position;

     }

     public void OnCollisionEnter2D(Collision2D collision)
     {
          if (collision.gameObject.CompareTag("Player") && collision.GetContact(0).normal.y < 0.5f)
          {

               //TODO: Colocar el trigger de la animación para dar tiempo al player de moverse
               if (_animator && _platformEnable)
               {
                    _animator.SetTrigger("inDestruction");
                    StartCoroutine(DownPlatform());
                    _platformEnable = false;
               }
          }
     }

     private IEnumerator ResetPlatform()
     {
         

          yield return new WaitForSeconds(_reaparitionTime);
          transform.position = _initialPosition;
          if (_spriteRenderer) _spriteRenderer.enabled = true;
          if(_boxCollider) _boxCollider.enabled = true;
          _platformEnable = true;

     }

     private IEnumerator DownPlatform()
     {
          float timeElapsed = 0f;
          
          while(timeElapsed < 1.5f)
          {
               transform.position = Vector3.Lerp(_initialPosition, _downPosition, timeElapsed / 1.5f);
               timeElapsed += Time.deltaTime;
               yield return null;
          }
     }


     // ToDo: Crear la animacion con el evento para que desaparezca
     public void DisapearPlatform()
     {
          
          if (_spriteRenderer) _spriteRenderer.enabled = false;
          if (_particleSystem) _particleSystem.Play();
          if (_boxCollider) _boxCollider.enabled = false;
          if (_animator)
          {
               _animator.SetTrigger("inReaparition");
          }
          StartCoroutine(ResetPlatform());
     }
}
