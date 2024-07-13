using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitch : InteractableObject
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject tongue;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private float impulseForce;
    [SerializeField] private float delay;
    public bool canUse = true;
    private Vector2 dir;

    public override void Interact()
    {
        if(canUse)
        {
            canUse = false;
            Vector2 hitchPos = transform.position;

            if (Player.Instance.transform.position.x < hitchPos.x && Player.Instance.direction == -1)
                Player.Instance.GetComponent<Player>().Turn(false);
            else if (Player.Instance.transform.position.x > hitchPos.x && Player.Instance.direction == 1)
                Player.Instance.GetComponent<Player>().Turn(true);

            Debug.Log("Hook 1");
            tongue.GetComponent<Tongue>().Hook(this, hitchPos);
        }
    }

    public void HitchShot()
    {
        Vector2 hitchPos = transform.position;
        Vector2 playerPos = Player.Instance.transform.position;

        dir = (hitchPos - playerPos).normalized;
        playerRB = Player.Instance.GetComponent<Rigidbody2D>();
        playerRB.gravityScale = 0;
        Player.Instance.DisableMotion(false);

        Impulse(1.5f);
    }

    public void Impulse(float multiply)
    {
        if (multiply == 1)
            anim.SetBool("hook", false);
        else
            anim.SetBool("hook", true);

        playerRB.velocity = new Vector2(0, 0);
        playerRB.velocity = dir * impulseForce * multiply;
        playerRB.gravityScale = 0;
    }

    public void DisableMotion()
    {
        playerRB.gravityScale = 3;
        StartCoroutine(EnablePlayerMovement());
    }

    IEnumerator EnablePlayerMovement()
    {
        yield return new WaitForSeconds(delay);
        canUse = true;
        Player.Instance.DisableMotion(true);
    }
}
