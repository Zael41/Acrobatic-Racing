using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public GameObject itemSlot;

    public Car car;
    public Rigidbody rb;

    public bool isBoosting;

    Vector3 boostForce;

    public PhotonView[] views;

    private Coroutine endBooster;

    // Start is called before the first frame update
    void Start()
    {
        car = GetComponent<Car>();
        rb = GetComponentInChildren<Rigidbody>();
        isBoosting = false;
        itemSlot = GameObject.Find("ItemSlot");
        if (car.currentItem == Car.items.None)
        {
            itemSlot.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (car.photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (car.currentItem == Car.items.Boost)
                {
                    Booster();
                }
                else if (car.currentItem == Car.items.Cannon)
                {
                    boostForce = new Vector3(0f, 250000f, 0f);
                    Debug.Log(boostForce);
                    rb.AddForce(boostForce);
                    rb.velocity = transform.GetChild(0).forward * 12f;
                }
                else if (car.currentItem == Car.items.Thunder)
                {
                    //car.photonView.RPC("ThunderHit", RpcTarget.All);
                    views = GameObject.FindObjectsOfType<PhotonView>();
                    foreach (PhotonView v in views)
                    {
                        if (v != car.photonView && v.gameObject.tag == ("Player"))
                        {
                            v.RPC("ThunderHit", RpcTarget.All);
                        }
                    }
                }
                car.currentItem = Car.items.None;
                itemSlot.SetActive(false);
            }
            if (isBoosting)
            {
                boostForce = transform.GetChild(0).forward * 25f;
                rb.velocity = boostForce;
            }
        }
    }

    IEnumerator EndBoost(float time)
    {
        yield return new WaitForSeconds(2f);
        isBoosting = false;
        rb.velocity = transform.GetChild(0).forward * 12f;
    }

    public void Booster()
    {
        if (car.photonView.IsMine)
        {
            if (endBooster != null) StopCoroutine(endBooster);
            boostForce = transform.GetChild(0).forward * 25f;
            isBoosting = true;
            endBooster = StartCoroutine(EndBoost(2));
        }
    }
}
