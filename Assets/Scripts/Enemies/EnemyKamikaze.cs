using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StateTypeKamikaze { Idle, Alert, Chase, Explode }

public class EnemyKamikaze : MonoBehaviour
{
    public StateTypeKamikaze stateType;
    private Rigidbody2D rb;
    private Animator anim;

    private bool alive = true;
    private bool inDelayToSearch;
    [SerializeField] private int direction = -1;
    [SerializeField] private float countDown;
    [SerializeField] private float chaseSpeed;

    // Colliders
    [SerializeField] private CollisionDetector alertZone;
    [SerializeField] private CollisionDetector weakPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        SetInitialValues();
    }

    private void SetInitialValues()
    {
        alive = true;
        ChangeState(StateTypeKamikaze.Idle);
    }

    private void ChangeState(StateTypeKamikaze newState)
    {
        stateType = newState;

        switch (stateType)
        {
            case StateTypeKamikaze.Idle:
                Idle();
                break;
            case StateTypeKamikaze.Alert:
                Alert();
                break;
            case StateTypeKamikaze.Chase:
                Chase();
                break;
            case StateTypeKamikaze.Explode:
                Explode();
                break;
        }
    }

    private void Update()
    {
        if (alive)
        {
            if (stateType == StateTypeKamikaze.Idle && alertZone.isTouching == true)
            {
                ChangeState(StateTypeKamikaze.Alert);
            }
        }
    }

    private void Idle()
    {
        Debug.Log("Idle");
    }

    private void Alert()
    {
        Debug.Log("Alert");
        anim.SetTrigger("Alert");
    }

    private void Chase()
    {
        Debug.Log("Chase");
        anim.SetTrigger("Chase");
        rb.velocity = new Vector2(chaseSpeed * direction, 0);
        StartCoroutine(CountDownToExplode());
    }

    private void Explode()
    {
        Debug.Log("Explode");
        rb.velocity = new Vector2(0, 0);
        anim.SetTrigger("Explode");
    }

    private IEnumerator CountDownToExplode()
    {
        yield return new WaitForSeconds(countDown);
        ChangeState(StateTypeKamikaze.Explode);
    }

    public void Death()
    {
        alive = false;
        gameObject.layer = LayerMask.NameToLayer("DeathEnemy");
        gameObject.SetActive(false);
    }

}
