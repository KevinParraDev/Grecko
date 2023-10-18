using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Saw : MovingObject
{
     private float _speed = 1f;

     // Update is called once per frame
     void Update()
    {
        if (this._active)
          {
               this._platform.transform.Rotate(0, 0, 360 * _speed * Time.deltaTime);
          }
    }
}
