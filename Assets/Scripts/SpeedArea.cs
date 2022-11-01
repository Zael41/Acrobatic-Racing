using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponentInParent<PhotonView>().IsMine && !other.gameObject.GetComponentInParent<UseItem>().isBoosting)
            {
                other.gameObject.GetComponentInParent<Car>().motorTorque = 25f;
                Vector3 boostForce = other.transform.forward * 5f;
                other.gameObject.GetComponentInParent<Rigidbody>().velocity = boostForce;
                Debug.Log("PURO SLOWING");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponentInParent<PhotonView>().IsMine)
            {
                other.gameObject.GetComponentInParent<Car>().motorTorque = 100f;
                Debug.Log("BACK TO SPEED");
            }
        }
    }
}
