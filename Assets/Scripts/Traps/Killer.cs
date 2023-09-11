using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
     protected void OnCollisionEnter2D(Collision2D collision)
     {
          Debug.Log("Mataaaaaar");
          if (collision.transform.CompareTag("Player"))
          {
               Debug.Log("Mataaaaaar 2");
               if (collision.transform.TryGetComponent<Player>(out Player player))
               {
                    Debug.Log("Mataaaaaar 2.5");
                    KillPlayer(collision.gameObject.GetComponent<Player>());
               }
          }
     }

     protected void KillPlayer(Player player)
     {
          Debug.Log("Mataaaaaar 3");
          player.Kill();
     }
}
