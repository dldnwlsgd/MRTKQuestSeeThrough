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
        }

        // 최종 버튼 활성화 조건 확인
        if (hololens_t && hololens_b && quest)
        {
            finalButton.SetActive(true);
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

    [PunRPC]
    void UpdateHololensT(bool value)
    {
        hololens_t = value;
        CheckFinalButtonState();
    }

    [PunRPC]
    void UpdateHololensB(bool value)
    {
        hololens_b = value;
        CheckFinalButtonState();
    }

    [PunRPC]
    void UpdateQuest(bool value)
    {
        quest = value;
        CheckFinalButtonState();
    }

    void CheckFinalButtonState()
    {
        if (hololens_t && hololens_b && quest)
        {
            finalButton.SetActive(true);
        }
        else
        {
            finalButton.SetActive(false);
        }
    }

    public void ChangeStateOfModelAssignOnSign()
    {
        photonView.RPC("UpdateHololensT", RpcTarget.All, true);
        photonView.RPC("ColorOnChanged", RpcTarget.All, registrationModelsignSphere.GetComponent<PhotonView>().ViewID);
    }

    public void ChangeStateOfModelAssignOffSign()
    {
        photonView.RPC("UpdateHololensT", RpcTarget.All, false);
        photonView.RPC("ColorOffChanged", RpcTarget.All, registrationModelsignSphere.GetComponent<PhotonView>().ViewID);
    }

    public void ChangeStateOfCubeAssignSign()
    {
        photonView.RPC("UpdateHololensB", RpcTarget.All, true);
        photonView.RPC("ColorOnChanged", RpcTarget.All, registrationCubesignSphere.GetComponent<PhotonView>().ViewID);
    }

    public void ChangeStateOfForQuestCubeAssignSign()
    {
        photonView.RPC("UpdateQuest", RpcTarget.All, true);
        photonView.RPC("ColorOnChanged", RpcTarget.All, forQuestregistrationCubessignSphere.GetComponent<PhotonView>().ViewID);
    }
}
