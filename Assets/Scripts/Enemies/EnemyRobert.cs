using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateTypeRobert { Patrol, Turn, Search, Chase, Death }

public class EnemyRobert : MonoBehaviour
{
    public StateTypeRobert stateType;
    private Rigidbody2D rb;
    private Animator anim;

    private bool alive = true;
    private bool inDelayToSearch;
    [SerializeField] private int direction = -1;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;

    // Colliders
    [SerializeField] private CollisionDetector alertZone;
    [SerializeField] private CollisionDetector backZone;
    [SerializeField] private CollisionDetector attackZone;
    [SerializeField] private CollisionDetector weakPoint;

    // Rycast
    private RaycastHit2D rI1, rI2, rD1, rD2;
    [SerializeField] private LayerMask capaSuelo;

    private void ChangeState(StateTypeRobert newState)
    {
        stateType = newState;

        switch (stateType)
        {
            case StateTypeRobert.Patrol:
                Patrol();
                break;
            case StateTypeRobert.Turn:
                Turn();
                break;
            case StateTypeRobert.Search:
                Search();
                break;
            case StateTypeRobert.Chase:
                Chase();
                break;
            case StateTypeRobert.Death:
                Death();
                break;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        SetInitialValues();
    }

    private void SetInitialValues()
    {
        alive = true;
        ChangeState(StateTypeRobert.Patrol);
    }

    private void Update()
    {
        if(alive)
        {
            if (weakPoint.isTouching == true)
                ChangeState(StateTypeRobert.Death);

            if (stateType == StateTypeRobert.Patrol)
            {
                rb.velocity = new Vector2(direction * patrolSpeed, 0);

                if (alertZone.isTouching == true)
                    ChangeState(StateTypeRobert.Search);

                DetectEnvironment();
            }
            else if (stateType == StateTypeRobert.Chase)
            {
                rb.velocity = new Vector2(direction * chaseSpeed, 0);

                if (alertZone.isTouching == false && !inDelayToSearch)
                    StartCoroutine(DelayToSearchAgain());
            }
        }
    }

    private void DetectEnvironment()
    {
        Debug.DrawRay(transform.position, Vector2.left * 1f, Color.red);
        Debug.DrawRay(transform.position + new Vector3(direction * 0.4f, 0, 0), Vector2.down * 1f, Color.red);
        rI1 = Physics2D.Raycast(transform.position, Vector2.left, 1f, capaSuelo);
        rI2 = Physics2D.Raycast(transform.position + new Vector3(direction * 0.4f, 0,0), Vector2.down, 1f, capaSuelo);

        if (!rI2)
            ChangeState(StateTypeRobert.Turn);
    }

    // Estados
    private void Patrol()
    {
        Debug.Log("Patrullar");
        anim.SetTrigger("Patrol");
    }

    private void Turn()
    {
        Debug.Log("Girar");
        rb.velocity = Vector2.zero;
        direction *= -1;
        transform.localScale = new Vector2(-direction, 1);

        anim.SetTrigger("Turn");
    }

    private void Search()
    {
        Debug.Log("Buscar");

        anim.SetTrigger("Search");
        rb.velocity = Vector2.zero;
    }

    private void Chase()
    {
        Debug.Log("Perseguir");
        anim.SetTrigger("Chase");
    }

    private void Death()
    {
        alive = false;
        rb.velocity = Vector2.zero;
        attackZone.gameObject.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("DeathEnemy");
        anim.SetTrigger("Death");
    }
    private void ChooseDirection()
    {
        if (alertZone.isTouching == true)
            ChangeState(StateTypeRobert.Chase);
        else if (backZone.isTouching == true)
        {
            direction *= -1;
            transform.localScale = new Vector2(-direction, 1);
            ChangeState(StateTypeRobert.Chase);
        }
        else
            ChangeState(StateTypeRobert.Patrol);
    }

    IEnumerator DelayToSearchAgain()
    {
        inDelayToSearch = true;
        yield return new WaitForSeconds(0.5f);
        if(alertZone.isTouching == false)
        {
            Debug.Log("Repeticiones");
            ChangeState(StateTypeRobert.Search);
        }
        inDelayToSearch = false;
    }
}
