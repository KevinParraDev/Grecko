using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private void EndLevel()
    {
          LevelManager.Instance.LoadNextLevel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
          if (collision.CompareTag("Player"))
          {
               EndLevel();
          } 
    }
}
