using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool open = false;

    [SerializeField] private int collectKeys = 0;
    public List<Key> keys;

    public void InsertKey()
    {
        if(!open)
        {
            Debug.Log("key: " + collectKeys);
            if (TryGetComponent(out Animator anim))
            {
                //AudioManager.Instance.PlaySound2D("Door_Open");

                anim.SetInteger("step", collectKeys);
                foreach (var k in keys)
                {
                    k.Hide();
                }
                keys.Clear();

            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("In Door");

        if(collision.tag == "Player")
        {
            InsertKey();
        }
    }

    public void CollectKey(Key k)
    {
        keys.Add(k);
        collectKeys += 1;
    }

    public void Open()
    {
        open = true;

        if (TryGetComponent(out BoxCollider2D collider2D))
        {
            collider2D.enabled = false;
        }
    }
}
