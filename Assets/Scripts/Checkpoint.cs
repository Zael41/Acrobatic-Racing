using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Car car;
    public int checkpointNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
            //gameController.FinishLap();
            car = other.gameObject.GetComponentInParent<Car>();
            car.checkpoints[checkpointNumber] = true;
        }
    }
}
