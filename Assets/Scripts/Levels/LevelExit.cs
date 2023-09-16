using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
     [SerializeField]
     private Player _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
          if (collision.CompareTag("Player"))
          {
               StartCoroutine(EndLevel());
               if(_player != null)
                    _player.gameObject.SetActive(false);
          } 
    }

     IEnumerator EndLevel()
     {
          if (transform.TryGetComponent(out Animator anim))
          {
               anim.SetTrigger("PortalIn");

               // Ejecutar mientras la animación está en proceso
               yield return new WaitForSeconds(1.3f);
          }

          LevelManager.Instance.LoadNextLevel();
     }
}
