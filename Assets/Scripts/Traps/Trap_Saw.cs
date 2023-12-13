using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Saw : MovingObject
{
     [SerializeField]
     private CircleCollider2D _sawCollider;

     // Update is called once per frame
     public override void Activate()
     {
          base.Activate();

          if (_sawCollider) _sawCollider.enabled = true;
     }

     public override void Deactivate()
     {
          base.Deactivate();

          if (_sawCollider) _sawCollider.enabled = false;
     }
}
