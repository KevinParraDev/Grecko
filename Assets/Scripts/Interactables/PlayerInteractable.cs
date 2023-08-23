using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

public class PlayerInteractable : MonoBehaviour
{
     [HideInInspector]
     public PlayerInteractable instance;

     private List<InteractableObject> _interactablesInRange = new();

     public bool HasInteractablesInRange => _interactablesInRange.Count > 0;

     private void Awake()
     {
          if (instance != null && instance != this)
          {
               Destroy(this);
          }
          else
          {
               instance = this;
          }
     }

     public void ActiveInteraction(InputAction.CallbackContext callbackContext)
     {
          if (callbackContext.performed)
          {
               if (HasInteractablesInRange)
               {
                    // De momento interactua con el primero detectado, luego hacer por rango?
                    _interactablesInRange[0].Interact();
               }
          }
     }

     private void OnTriggerEnter2D(Collider2D other)
     {
          if(other.transform.TryGetComponent(out InteractableObject obj)){
               _interactablesInRange.Add(obj);
          }
     }

     private void OnTriggerExit2D(Collider2D other)
     {
          if(other.transform.TryGetComponent(out InteractableObject obj))
          {
               _interactablesInRange.Remove(obj);
          }
     }

     // TODO: Revisar si son necesarios metodos de limpieza del array de interaccion al teletransportar el player o similar
}
