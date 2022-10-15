using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapCountCollider : MonoBehaviour
{
    public Car car;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
            //gameController.FinishLap();
            car = other.gameObject.GetComponentInParent<Car>();
            car.FinishLap();
        }
    }
}
