using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Killer : MonoBehaviour
{
     private void OnCollisionEnter2D(Collision2D collision)
     {
          if (collision.transform.CompareTag("Player"))
          {
               KillPlayer();
          }
     }

     protected void KillPlayer()
     {
          // Player.Kill();
     }
}
