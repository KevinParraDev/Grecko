using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private int actualOrder = 0;
    public Transform lastCheckpoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            int newOrder = other.gameObject.GetComponent<Checkpoint>().order;
            if (newOrder > actualOrder)
            {
                actualOrder = newOrder;
                SaveCheckpoint(other.transform.GetComponent<Checkpoint>().pointToAppear);
            }
        }
    }

    private void SaveCheckpoint(Transform checkpoint)
    {
        lastCheckpoint = checkpoint;
    }
}
