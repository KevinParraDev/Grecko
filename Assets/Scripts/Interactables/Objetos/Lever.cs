using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : InteractableObject
{
 
     [SerializeField]
     private List<GameObject> _objectsToActive;

     // Para la esperar la animacion
     private bool _isInAnimation = false;

     public override void Interact()
    {
          if (_objectsToActive.Count > 0 && !_isInAnimation)
          {
               AudioManager.Instance.PlaySound2D("Lever_Interact");
               StartAnimation();

               foreach (GameObject obj in _objectsToActive)
               {
                    ActivateObject(obj);
               }          
          }
     }

     private void ActivateObject(GameObject obj)
     {
          if (obj.TryGetComponent(out IActivable _elementToActive))
               _elementToActive.Switch();
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
