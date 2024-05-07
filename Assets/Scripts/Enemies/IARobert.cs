using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateRobert { Patrol = 0, Chase = 1, Atack = 2, Death = 3, Search =4}

public class IARobert : DamageableEntiti
{
    public StateRobert stateType;
    private Rigidbody2D rb;
    private Animator anim;

    private float distance;
    private bool alive;
    private float speed;
    private int direction;
    public bool endClip;

    [SerializeField] private float knockbackForce;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;

    [SerializeField] private float atackRange;
    [SerializeField] private float alertRange;
    [SerializeField] private float unFollowRange;

    // Rycast
    private RaycastHit2D rycastPared, rycastSuelo;
    [SerializeField] private LayerMask capaSuelo;

    private void Start()
    {
        alive = true;
        direction = -1;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(StateRobert.Patrol);
        ChangeVelocity();
    }

    private void Update()
    {
        if(alive)
        {
            DetectEnvironment();

            distance = (Player.Instance.transform.position - transform.position).magnitude;

            if(distance < atackRange && stateType != StateRobert.Atack)
            {
                ChangeState(StateRobert.Atack);
            }
            else if(distance < alertRange && stateType != StateRobert.Chase && stateType != StateRobert.Search)
            {
                ChangeState(StateRobert.Search);
                ChangeVelocity();
            }
            else if(distance > unFollowRange && stateType != StateRobert.Patrol && stateType != StateRobert.Search)
            {
                ChangeState(StateRobert.Search);
                ChangeVelocity();
            }
        }
    }

    private void DetectEnvironment()
    {
        if (direction == -1)
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
        rycastSuelo = Physics2D.Raycast(transform.position + new Vector3(direction * 0.4f, 0, 0), Vector2.down, 1f, capaSuelo);

        if (!rycastSuelo | rycastPared)
        {
            Debug.Log("suelo");
            rb.velocity = Vector2.zero;
            direction *= -1;
            transform.localScale = new Vector2(-direction, 1);
            anim.SetTrigger("Turn");
            ChangeVelocity();
            StartCoroutine(StopMovement("Turn"));
        }
    }

    private void ChangeVelocity()
    {
        if (alive)
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
            gameObject.layer = 8;
        }
    }

    public void ChangeState(StateRobert newState)
    {
        stateType = newState;

        switch (newState)
        {
            case StateRobert.Patrol:
                Patrol();
                break;
            case StateRobert.Chase:
                Chase();
                break;
            case StateRobert.Atack:
                Atack();
                break;
            case StateRobert.Death:
                Death();
                break;
            case StateRobert.Search:
                Search();
                break;
        }
    }

    private void Patrol()
    {
        speed = patrolSpeed;
        Debug.Log("Patrol");
        anim.SetTrigger("Patrol");
    }

    private void Chase()
    {
        speed = chaseSpeed;
        anim.SetTrigger("Chase");
        ChangeVelocity();
    }

    private void Atack()
    {
        Debug.Log("Atacar");
        Player.Instance.Damage(2, transform.position);
    }

    public override void Death()
    {
        anim.SetTrigger("Death");
        GetComponent<SpriteRenderer>().color = Color.red;
        alive = false;
        
    }

    private void Search()
    {
        StartCoroutine(StopMovement("Search"));
    }

    public void EndClipAnim()
    {
        endClip = true;
    }

    IEnumerator StopMovement(string clip)
    {
        float auxSpeed = speed;
        speed = 0;
        ChangeVelocity();
        endClip = false;
        anim.SetTrigger(clip);
        yield return new WaitWhile(() => endClip == false);
        speed = auxSpeed;
        ChangeVelocity();

        if (clip == "Search" && distance < alertRange)
            ChangeState(StateRobert.Chase);
        else if (clip == "Search" && distance > alertRange)
            ChangeState(StateRobert.Patrol);
    }

    public override void TakeDamage(Vector3 damageDir)
    {
        Debug.Log("Damage");
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector3.Normalize(transform.position - damageDir) * knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(DelayKnockback());
    }

    IEnumerator DelayKnockback()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        ChangeVelocity();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, unFollowRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, atackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, alertRange);
    }
}