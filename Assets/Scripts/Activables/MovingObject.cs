
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour, IActivable
{
     [Header("Movement")]
     [SerializeField]
     private Transform[] _wayPoints;
     [SerializeField]
     [Range(0f, 0.1f)]
     private float _speed;
     [SerializeField]
     private bool inMovement;

     [Space(10)]
     [SerializeField]
     private GameObject _platform;

     // flag de direccionamiento del movimiento
     private int _indexWayPoint;

     


     private void FixedUpdate()
     {
          if(inMovement)
          {
               Move();
          }
     }

     public virtual void Activate()
     {
          inMovement = true;
     }

     public virtual void Deactivate()
     {
          inMovement = false;
     }

     private void Move()
     {
          // Debe hacerse asi ya que al ser activable y desactivable usando lerp y Math.pingpong generaba comportamientos no deseados
          // adicionalmente esto permite tener mas de dos puntos de desplazamiento

          // Comprobamos si la plataforma ya llego al punto
          if (Vector3.Distance(_platform.transform.position, _wayPoints[_indexWayPoint].position) <= 0.1f)
          {
               _indexWayPoint++;

               if(_indexWayPoint >= _wayPoints.Length)
               {
                    _indexWayPoint = 0;
               }
          }

          // Esto es lo que me desplaza la plataforma
          _platform.transform.position = Vector3.MoveTowards(_platform.transform.position, _wayPoints[_indexWayPoint].position, _speed);         
     }

     // TODO: En caso de que queramos que la plataforma vuelva al medio creamos el metodo Stop
}
