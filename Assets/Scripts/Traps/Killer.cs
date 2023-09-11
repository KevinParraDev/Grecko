using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
     protected void OnCollisionEnter2D(Collision2D collision)
     {
          if (collision.transform.CompareTag("Player"))
          {
               if (collision.transform.TryGetComponent<Player>(out Player player))
                    KillPlayer(collision.gameObject.GetComponent<Player>());
          }
     }

     protected void KillPlayer(Player player)
     {
          player.Kill();
     }
}
