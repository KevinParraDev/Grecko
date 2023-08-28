using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
     [SerializeField]
     private GameObject _bulletPrefab;

     [SerializeField]
     private int _poolSize;

     [SerializeField]
     private List<GameObject> _bulletList;

     private void Start()
     {
          AddBulletsToPool(_poolSize);
     }

     private void AddBulletsToPool(int amount)
     {
          for (int i = 0; i < amount; i++)
          {
               GameObject bullet = Instantiate(_bulletPrefab);
               bullet.SetActive(false);
               _bulletList.Add(bullet);
               bullet.transform.parent = transform;
          }
     }

     public GameObject RequestBullet()
     {
          foreach (GameObject bullet in _bulletList)
          {
               // Si la bala no está activa se puede disparar
               if(!bullet.activeSelf)
               {
                    bullet.SetActive(true);
                    return bullet;
               }
          }
          AddBulletsToPool(1);
          _bulletList[_bulletList.Count - 1].SetActive(true);
          return _bulletList[_bulletList.Count - 1];
     }
}
