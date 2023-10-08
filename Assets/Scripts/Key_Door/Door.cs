using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IActivable
{

     public void Open()
     {
          if (TryGetComponent(out Animator anim))
          {
               anim.SetTrigger("Open");
          }

          if (TryGetComponent(out BoxCollider2D collider2D)) {
               collider2D.enabled = false;
          }

     }

     public void Switch()
     {
          // En caso de que quiera cerrarse y abrirse
     }

     public void Activate()
     {
          Open();
     }

     public void Deactivate()
     {
          // En caso de que quiera cerrarse
     }
}
