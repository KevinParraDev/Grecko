using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateTypeRobert { Patrol, Turn, Search, Chase, Death, Damage }

public class EnemyRobert : DamageableEntiti
{
    public StateTypeRobert stateType;
    private Rigidbody2D rb;
    private Animator anim;

    private bool alive = true;
    private bool inDelayToSearch;
    private bool canBound = true;
    [SerializeField] private int direction = -1;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;

    // Colliders
    [SerializeField] private CollisionDetector alertZone;
    [SerializeField] private CollisionDetector unFollowZone;
    [SerializeField] private CollisionDetector backZone;
    [SerializeField] private CollisionDetector attackZone;
    [SerializeField] private CollisionDetector weakPoint;

    // Rycast
    private RaycastHit2D rycastPared, rycastSuelo;
    [SerializeField] private LayerMask capaSuelo;

    public override void TakeDamage(Vector3 damageDir)
    {
        Debug.Log("Damage");
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector3.Normalize(transform.position - damageDir) * 10, ForceMode2D.Impulse);
        StartCoroutine(DelayKnockback());
    }

    IEnumerator DelayKnockback()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        ChangeState(StateTypeRobert.Chase);
    }

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
            DetectEnvironment();

            if (weakPoint.isTouching == true & canBound)
            {
                canBound = false;
                Debug.Log("Pisoton");
                Player.Instance.Jump();
                Damage(2, transform.position);
            }

            if(canBound == false & weakPoint.isTouching == false)
                canBound = true;

            //ChangeState(StateTypeRobert.Death);

            if (stateType == StateTypeRobert.Patrol)
            {
                rb.velocity = new Vector2(direction * patrolSpeed, rb.velocity.y);

                if (alertZone.isTouching == true)
                    ChangeState(StateTypeRobert.Search);
            }
            else if (stateType == StateTypeRobert.Chase)
            {
                rb.velocity = new Vector2(direction * chaseSpeed, rb.velocity.y);

                if (unFollowZone.isTouching == false && !inDelayToSearch)
                    ChangeState(StateTypeRobert.Search);

                if (backZone.isTouching == true)
                    ChangeState(StateTypeRobert.Search);
            }
        }
    }

    private void DetectEnvironment()
    {
        
        if(direction == -1)
        {
            Debug.DrawRay(transform.position, Vector2.left * 1f, Color.red);
            rycastPared = Physics2D.Raycast(transform.position, Vector2.left, 1f, capaSuelo);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.right * 1f, Color.red);
            rycastPared = Physics2D.Raycast(transform.position, Vector2.right, 1f, capaSuelo);
        }

        Debug.DrawRay(transform.position + new Vector3(direction * 0.4f, 0, 0), Vector2.down * 1f, Color.red);
        rycastSuelo = Physics2D.Raycast(transform.position + new Vector3(direction * 0.4f, 0,0), Vector2.down, 1f, capaSuelo);

        if (!rycastSuelo | rycastPared)
        {
            Debug.Log("Rotar: " + rycastPared + " - " + rycastSuelo.ToString());
            if (stateType == StateTypeRobert.Patrol)
                ChangeState(StateTypeRobert.Turn);
            else if (stateType == StateTypeRobert.Chase)
                ChangeState(StateTypeRobert.Turn);
        }
    }

    // Estados
    private void Patrol()
    {
        anim.SetTrigger("Patrol");
    }

    private void Turn()
    {
        rb.velocity = Vector2.zero;
        direction *= -1;
        transform.localScale = new Vector2(-direction, 1);

        anim.SetTrigger("Turn");
    }

    private void Search()
    {
        anim.SetTrigger("Search");
        rb.velocity = Vector2.zero;
    }

    private void Chase()
    {
        anim.SetTrigger("Chase");
    }

    public override void Death()
    {
        alive = false;
        rb.velocity = Vector2.zero;
        rb.simulated = false;
        attackZone.gameObject.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("DeathEnemy");
        anim.SetTrigger("Death");
    }
    public void ChooseDirection()
    {
        if(unFollowZone.isTouching == true)
        {
            if ((Player.Instance.transform.position.x > transform.position.x & direction == -1) | (Player.Instance.transform.position.x < transform.position.x & direction == 1))
            {
                direction *= -1;
                transform.localScale = new Vector2(-direction, 1);
                ChangeState(StateTypeRobert.Chase);
            }
            else
            {
                ChangeState(StateTypeRobert.Chase);
            }
        }
        else
        {
            ChangeState(StateTypeRobert.Patrol);
        }
    }

    IEnumerator DelayToSearchAgain()
    {
        inDelayToSearch = true;
        yield return new WaitForSeconds(0.5f);
        if(alertZone.isTouching == false)
        {
            ChangeState(StateTypeRobert.Search);
        }
        inDelayToSearch = false;
    }
}
