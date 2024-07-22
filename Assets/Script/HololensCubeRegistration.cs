using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HololensCubeRegistration : MonoBehaviour
{
    private PhotonView pv;
    public GameObject trackingObject;
    public GameObject stoptrackingObject;

    public NoitceStateUICtrl noitceStateUICtrl;

    private Vector3 position;
    private Quaternion rotation;
    private Vector3 trackingObjectposition;
    private Quaternion trackingObjectrotation;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void RPC_SendPosition(Vector3 getposition, Quaternion getrotation)
    {
        Debug.Log("Received position: " + getposition + " and rotation: " + getrotation);
        trackingObjectposition = getposition;
        trackingObjectrotation = getrotation;
        Debug.Log(GetPosition());
        Debug.Log(GetRotation());

        // position과 rotation 값을 사용하는 추가 로직을 여기에 추가할 수 있습니다.
    }

    public void SetForHololensObjectTransform() // button event hololens cube
    {
        position = transform.position;
        rotation = transform.rotation;
        noitceStateUICtrl.ChangeStateOfCubeAssignSign();

    }



    public void SendTrackingObjectPosition() // button event when object
    {
        if (pv != null)
        {
            trackingObjectposition = trackingObject.transform.position;
            trackingObjectrotation = trackingObject.transform.rotation;
            pv.RPC("RPC_SendPosition", RpcTarget.Others, trackingObjectposition, trackingObjectrotation);
        }
        else
        {
            Debug.LogError("PhotonView is not assigned.");
        }
    }

    public void SetUnTrackingObjectTransform() // button event when object
    {
        stoptrackingObject.transform.position = trackingObject.transform.position;
        Debug.Log("SetUnTrackingObjectTransform" + trackingObject.transform.position);
        stoptrackingObject.transform.rotation = trackingObject.transform.rotation;
        //trackingObject.SetActive(false);
        stoptrackingObject.SetActive(true);
        noitceStateUICtrl.ChangeStateOfModelAssignOnSign();
    }

    public void SetTrackingObject() // button event when object track again
    {
        //trackingObject.SetActive(true);
        stoptrackingObject.SetActive(false);
        noitceStateUICtrl.ChangeStateOfModelAssignOffSign();
    }


    public Vector3 GetPosition()
    {
        return trackingObjectposition;
    }

    public Quaternion GetRotation()
    {
        return trackingObjectrotation;
    }

}
