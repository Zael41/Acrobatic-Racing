using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public float ScrollX = -1f;
    public float ScrollY = 0f;

    // Update is called once per frame
    void Update()
    {
        float OffsetX = Time.time * ScrollX;
        float OffsetY = Time.time * ScrollY;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(OffsetX, OffsetY);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("marselo");
            if (other.gameObject.GetComponentInParent<PhotonView>().IsMine)
            {
                other.gameObject.GetComponentInParent<UseItem>().Booster();
            }
        }
    }
}
