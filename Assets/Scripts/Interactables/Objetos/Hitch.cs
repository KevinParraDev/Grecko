using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitch : InteractableObject
{
    [SerializeField] private GameObject playerGO;
    [SerializeField] private float impulseForce;
    [SerializeField] private float delay;

    public override void Interact()
    {
        Debug.Log("Lazo");
        Vector2 hitchPos = transform.position;
        Vector2 playerPos = playerGO.transform.position;

        Vector2 dir = (hitchPos - playerPos).normalized;
        Rigidbody2D playerRB = playerGO.GetComponent<Rigidbody2D>();
        playerRB.velocity = new Vector2(0, 0);
        playerRB.velocity = dir * impulseForce;

        StartCoroutine(EnablePlayerMovement());
    }

    IEnumerator EnablePlayerMovement()
    {
        playerGO.GetComponent<Player>().DisableMotion(false);
        yield return new WaitForSeconds(delay);
        playerGO.GetComponent<Player>().DisableMotion(true);
    }
}
