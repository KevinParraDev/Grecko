using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{

     // Los colliders de ciertos killer seran trigger, otros no, por eso ambos metodos

     protected void OnCollisionEnter2D(Collision2D collision)
     {
          if (collision.transform.CompareTag("Player"))
          {
               if (collision.transform.TryGetComponent<Player>(out Player player))
                    KillPlayer(collision.gameObject.GetComponent<Player>());
          }
     }

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.transform.CompareTag("Player"))
          {
               if (collision.transform.TryGetComponent<Player>(out Player player))
                    KillPlayer(collision.gameObject.GetComponent<Player>());
          }
     }

     protected void KillPlayer(Player player)
     {
          player.Death();
     }
}
