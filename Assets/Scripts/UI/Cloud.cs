using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "CloudLimit")
            ChangeDirection();
    }

    private void ChangeDirection()
    {
        speed *= -1;
        rb.velocity = new Vector2(speed * 0.08f, 0);
    }
}
