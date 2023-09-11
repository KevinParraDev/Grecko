using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitch : InteractableObject
{
    [SerializeField] private GameObject playerGO;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private GameObject tongue;
    [SerializeField] private float impulseForce;
    [SerializeField] private float delay;
    private Vector2 dir;

    public override void Interact()
    {
        Debug.Log("Lazo");
        Vector2 hitchPos = transform.position;

        if (playerGO.transform.position.x <= hitchPos.x)
            playerGO.GetComponent<Player>().Turn(false);
        else
            playerGO.GetComponent<Player>().Turn(true);

        tongue.GetComponent<Tongue>().Hook(this, hitchPos);
    }

    public void HitchShot()
    {
        Vector2 hitchPos = transform.position;
        Vector2 playerPos = playerGO.transform.position;

        dir = (hitchPos - playerPos).normalized;
        playerRB = playerGO.GetComponent<Rigidbody2D>();
        playerRB.gravityScale = 0;
        playerGO.GetComponent<Player>().DisableMotion(false);

        Impulse(20);
    }

    public void Impulse(float impulse)
    {
        playerRB.velocity = new Vector2(0, 0);
        playerRB.velocity = dir * impulse;
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
        playerGO.GetComponent<Player>().DisableMotion(true);
    }
}
