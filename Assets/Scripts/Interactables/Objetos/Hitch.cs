using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitch : InteractableObject
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject playerGO;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private GameObject tongue;
    [SerializeField] private float impulseForce;
    [SerializeField] private float delay;
    private bool canUse = true;
    private Vector2 dir;

    public override void Interact()
    {
        if(canUse)
        {
            canUse = false;
            Vector2 hitchPos = transform.position;

            if (playerGO.transform.position.x <= hitchPos.x)
                playerGO.GetComponent<Player>().Turn(false);
            else
                playerGO.GetComponent<Player>().Turn(true);

            tongue.GetComponent<Tongue>().Hook(this, hitchPos);
        }
    }

    public void HitchShot()
    {
        Vector2 hitchPos = transform.position;
        Vector2 playerPos = playerGO.transform.position;

        dir = (hitchPos - playerPos).normalized;
        playerRB = playerGO.GetComponent<Rigidbody2D>();
        playerRB.gravityScale = 0;
        playerGO.GetComponent<Player>().DisableMotion(false);

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
        playerGO.GetComponent<Player>().DisableMotion(true);
    }
}
