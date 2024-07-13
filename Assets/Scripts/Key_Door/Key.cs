using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private Door door;

    // El isCollected puede usarse para la UI o no
    private bool _isCollected = false;
    private Vector3 _initialPosition;
    private Vector3 velocity = Vector3.right;
    [SerializeField] private float dampling;
    [SerializeField] private Transform targetPlayer;
    private Transform target;

    private void Start()
    {
        _initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        //Vector3 target = Player.Instance.transform.position;
        if(_isCollected)
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, dampling);

    }

    private void Collect()
    {
        if (door.keys.Count() > 0)
            target = door.keys.LastOrDefault().transform;
        else
            target = targetPlayer;

        _isCollected = true;
        door.CollectKey(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!_isCollected)
            {
                _isCollected = true;
                Collect();
            }
        }
    }

    public void Hide()
    {
        if (TryGetComponent(out Animator anim))
        {
            anim.SetTrigger("Hide");
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Drop()
    {
        transform.position = _initialPosition;
    }
}
