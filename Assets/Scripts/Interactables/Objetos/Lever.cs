using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : InteractableObject
{
     private bool _active = false;

     [SerializeField]
     private List<GameObject> _objectsToActive;

     // Para la esperar la animacion
     private bool _isInAnimation = false;

     public override void Interact()
    {
          if (_objectsToActive.Count > 0 && !_isInAnimation)
          {
               StartAnimation();

               // Si no esta activo se activa con los elemento y viceversa

               if (!_active)
               {
                    foreach (GameObject obj in _objectsToActive)
                    {
                         ActivateObject(obj);
                    }

                    _active = true;
               }
               else
               {
                    foreach (GameObject obj in _objectsToActive)
                    {
                         DeactiveObject(obj);
                    }

                    _active = false;
               }
                      
          }
     }

     private void ActivateObject(GameObject obj)
     {
          
          if (obj.TryGetComponent(out IActivable _elementToActive))
               _elementToActive.Activate();
           
          
     }

     private void DeactiveObject(GameObject obj)
     {
          if (obj.TryGetComponent(out IActivable _elementToActive))
               _elementToActive.Deactivate();
     }

    public void FinishAnimation()
    {
          _isInAnimation = false;
    }

     private void StartAnimation()
     {
          if(TryGetComponent<Animator>(out Animator anim))
          {
               anim.SetTrigger("Interact");
               _isInAnimation |= true;
          }   
     }
}
