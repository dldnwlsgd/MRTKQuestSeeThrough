using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoticePositionToUI : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUIforTrackingObjectPostiion;
    public TextMeshProUGUI textMeshProUGUIforCameraPostiion;
    public TextMeshProUGUI registerCubePostiion;

    public GameObject textMeshProUGUIforTrackingObject;
    public GameObject textMeshProUGUIforCamera;
    public GameObject registerCube;

    private bool isNoticeToUI = false;
    // Start is called before the first frame update
    

    private void FixedUpdate()
    {
        if (isNoticeToUI)
        {
            textMeshProUGUIforTrackingObjectPostiion.text = textMeshProUGUIforTrackingObject.transform.position.ToString();
            textMeshProUGUIforCameraPostiion.text = textMeshProUGUIforCamera.transform.position.ToString();
            registerCubePostiion.text = registerCube.transform.position.ToString();
        }
    }
    

    public void NoticePostiionToUI()
    {
        isNoticeToUI = true;
    }
}
