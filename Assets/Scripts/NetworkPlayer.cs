using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class NetworkPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonView PV = GetComponent<PhotonView>();
        GameObject camera = this.transform.Find("Main Camera").gameObject;

        GameObject taxi = this.transform.Find("Taxi").gameObject;
        Transform focusPoint = taxi.transform.Find("CameraFocus");
        GameObject cinemachineObject = this.transform.Find("CM FreeLook1").gameObject;
        CinemachineFreeLook CMFL = this.GetComponentInChildren<CinemachineFreeLook>();

        CMFL.Follow = focusPoint;
        CMFL.LookAt = focusPoint;

        SpeedMeter speedMeter = GameObject.Find("Speedometer").GetComponent<SpeedMeter>();
        if (PV.IsMine) speedMeter.target = taxi.GetComponent<Rigidbody>();
        if (!PV.IsMine)
        {
            camera.SetActive(false);
            cinemachineObject.SetActive(false);

            MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
            {
                if (script is NetworkPlayer) continue;
                if (script is PhotonView) continue;
                script.enabled = false;
            }
        }
    }
}
