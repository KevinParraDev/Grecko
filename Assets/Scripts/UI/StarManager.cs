using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [SerializeField] private Animator[] starsAnim;

    private void Start()
    {
        for (int i = 0; i < starsAnim.Length; i++)
        {
            starsAnim[i].SetInteger("Value", Random.Range(0, 6));
        }
    }
}
