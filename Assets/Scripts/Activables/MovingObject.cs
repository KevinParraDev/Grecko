
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

     [Header("Sprites")]
     [SerializeField]
     private Sprite _activeSpriteObject;
     [SerializeField]
     private Sprite _inactiveSpriteObject;
     [SerializeField]
     private Sprite _activeSpritePoint;
     [SerializeField]
     private Sprite _inactiveSpritePoint;

     private SpriteRenderer _platformSpriteRenderer;

     private void Start()
     {
          _platformSpriteRenderer = _platform.GetComponent<SpriteRenderer>();

          if (inMovement)
          {

               ChangeSprite(_platformSpriteRenderer, _activeSpriteObject);

               foreach (Transform t in _wayPoints)
               {
                    if(t.TryGetComponent<SpriteRenderer>(out SpriteRenderer _rendererPoint))
                         ChangeSprite(_rendererPoint, _activeSpritePoint);
               }
          }
               
     }

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
          ChangeSprite(_platformSpriteRenderer, _activeSpriteObject);

          foreach (Transform t in _wayPoints)
          {
               if (t.TryGetComponent<SpriteRenderer>(out SpriteRenderer _rendererPoint))
                    ChangeSprite(_rendererPoint, _activeSpritePoint);
          }
     }

     public virtual void Deactivate()
     {
          inMovement = false;
          ChangeSprite(_platformSpriteRenderer, _inactiveSpriteObject);

          foreach (Transform t in _wayPoints)
          {
               if (t.TryGetComponent<SpriteRenderer>(out SpriteRenderer _rendererPoint))
                    ChangeSprite(_rendererPoint, _inactiveSpritePoint);
          }
     }

     private void ChangeSprite(SpriteRenderer _objToChange, Sprite newSprite)
     {
          if (_objToChange != null)
               _objToChange.sprite = newSprite;
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
