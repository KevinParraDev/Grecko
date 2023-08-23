using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
     [SerializeField] 
     private GameObject _icon;

     /// <summary>
     /// Metodo para implementar la interaccion
     /// </summary>
     public abstract void Interact();

     public void ShowIcon()
     {
          if(_icon!= null)
          {
               _icon.SetActive(true);
          }
     }

     public void HideIcon()
     {
          if (_icon != null)
          {
               _icon.SetActive(false);
          }
     }

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.CompareTag("Player")){
               ShowIcon();
          }
     }

     private void OnTriggerExit2D(Collider2D collision)
     {
          if (collision.CompareTag("Player"))
          {
               HideIcon();
          }
     }
}
