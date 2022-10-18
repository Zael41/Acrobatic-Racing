using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxGet : MonoBehaviour
{
    public Car car;
    public bool itemRespawning;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !itemRespawning)
        {
            //GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
            //gameController.FinishLap();
            car = other.gameObject.GetComponentInParent<Car>();
            //Car.items randItem = (Car.items)Random.Range(0, 3);
            car.GetRandomItem();
            StartCoroutine(itemCooldown());
        }
    }

    public IEnumerator itemCooldown()
    {
        itemRespawning = true;
        this.GetComponentInChildren<ParticleSystem>().Stop();
        this.GetComponentInChildren<MeshRenderer>().enabled = false;
        this.GetComponentInChildren<Collider>().enabled = false;
        yield return new WaitForSeconds(2.0f);
        this.GetComponentInChildren<ParticleSystem>().Play();
        this.GetComponentInChildren<MeshRenderer>().enabled = true;
        this.GetComponentInChildren<Collider>().enabled = true;
        this.itemRespawning = false;
        yield break;
    }
}
