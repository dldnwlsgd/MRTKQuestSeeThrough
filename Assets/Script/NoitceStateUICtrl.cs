using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NoitceStateUICtrl : MonoBehaviourPun
{
    public Material signOn;
    public Material signOff;

    public GameObject registrationModelsignSphere;
    public GameObject registrationCubesignSphere;
    public GameObject forQuestregistrationCubessignSphere;

    public GameObject finalButton;

    bool hololens_t = false, hololens_b = false;
    bool quest = false;

    void Start()
    {

    }

    void Update()
    {

    }

    [PunRPC]
    void ColorOnChanged(int sphereSignViewID)
    {
        PhotonView spherePhotonView = PhotonView.Find(sphereSignViewID);
        if (spherePhotonView != null)
        {
            GameObject sphereSign = spherePhotonView.gameObject;
            sphereSign.GetComponent<MeshRenderer>().material = signOn;
            
            if (hololens_t && hololens_b && quest)
            {
                finalButton.SetActive(true);
            }
        }
    }

    [PunRPC]
    void ColorOffChanged(int sphereSignViewID)
    {
        PhotonView spherePhotonView = PhotonView.Find(sphereSignViewID);
        if (spherePhotonView != null)
        {
            GameObject sphereSign = spherePhotonView.gameObject;
            sphereSign.GetComponent<MeshRenderer>().material = signOff;

        }
    }

    public void ChangeStateOfModelAssignOnSign()
    {
        hololens_t = true;
        photonView.RPC("ColorOnChanged", RpcTarget.All, registrationModelsignSphere.GetComponent<PhotonView>().ViewID);
    }

    public void ChangeStateOfModelAssignOffSign()
    {
        hololens_t = false;
        photonView.RPC("ColorOffChanged", RpcTarget.All, registrationModelsignSphere.GetComponent<PhotonView>().ViewID);
    }
    
    public void ChangeStateOfCubeAssignSign()
    {
        hololens_b = true;
        photonView.RPC("ColorOnChanged", RpcTarget.All, registrationCubesignSphere.GetComponent<PhotonView>().ViewID);
    }

    public void ChangeStateOfForQuestCubeAssignSign()
    {
        quest = true;
        photonView.RPC("ColorOnChanged", RpcTarget.All, forQuestregistrationCubessignSphere.GetComponent<PhotonView>().ViewID);
    }
}
