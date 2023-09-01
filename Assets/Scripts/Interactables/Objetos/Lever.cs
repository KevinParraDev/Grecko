using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : InteractableObject
{

     private bool _active = false;

     [SerializeField]
     private Transform _leverStick;

     [SerializeField]
     private List<GameObject> _objectsToActive;

     // Para la animacion
     private bool _isInAnimation = false;
     Quaternion startRotation = Quaternion.Euler(0f, 0f, 0f);
     Quaternion targetRotation = Quaternion.Euler(0f, 0f, 45f); // Rotar el objeto en z

     public override void Interact()
    {
          if (_objectsToActive.Count > 0 && !_isInAnimation)
          {
               // Si no esta activo se activa con los elemento y viceversa

               if (!_active)
               {
                    foreach (GameObject obj in _objectsToActive)
                    {
                         ActivateObject(obj);
                    }

                    _active = true;
                    StartCoroutine(RotateLever());
               }
               else
               {
                    foreach (GameObject obj in _objectsToActive)
                    {
                         DeactiveObject(obj);
                    }

                    _active = false;
                    StartCoroutine(RotateLever());
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

    // Corrutina para animar la rotacion de la palanca
    IEnumerator RotateLever()
     {
          _isInAnimation = true;

          float elapsedTime = 0f;
          float rotationTime = 0.3f; // Duracion de la rotacion

          while (elapsedTime < rotationTime)
          {
               elapsedTime += Time.deltaTime;
               float t = elapsedTime / rotationTime;
               _leverStick.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

               yield return null;
          }

          // Invierto los puntos de inicio y fin de la animacion para una desactivacion
          Quaternion temp = targetRotation;
          targetRotation = startRotation;
          startRotation = temp;

          _isInAnimation = false;
     }
}
