using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RespawnCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            float minDistance = 10000f;
            float currentDistance;
            GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
            GameObject respawnCheckpoint = checkpoints[0];
            foreach (GameObject cp in checkpoints)
            {
                currentDistance = Vector3.Distance(other.transform.position, cp.transform.position);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    respawnCheckpoint = cp;
                }
            }
            other.transform.parent.position = respawnCheckpoint.transform.position;
            other.transform.parent.rotation = respawnCheckpoint.transform.rotation;
            other.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
