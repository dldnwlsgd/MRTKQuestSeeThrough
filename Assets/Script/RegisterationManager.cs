using MRTK.Tutorials.MultiUserCapabilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RegisterationManager : MonoBehaviour
{

    public GameObject hololensCube;
    public GameObject questCube;

    public GameObject otherPlatformSettingObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    
    public void GetPositionRPCdataAndSetObject() // final button event
    {

        HololensCubeRegistration hololensRegCubeScript = hololensCube.GetComponent<HololensCubeRegistration>();


        if (hololensRegCubeScript == null)
        {
            Debug.LogError("RegistrationCube script is not assigned.");
            return;
        }

        Vector3 headPosition = hololensRegCubeScript.GetPosition();
        Quaternion headRotation = hololensRegCubeScript.GetRotation();

        if (headPosition == null || headRotation == null)
        {
            Debug.Log("Transform data not received.");
        }
        else
        {
            Debug.Log($"position: {headPosition}, rotation: {headRotation}");
            // photon view 1 must connected and same = RPC must called to same object
            // own, other registeration cube must same in other platform
            // therefore you must consider your object alien in your editor

            Vector3 relativePosition = headPosition - hololensRegCubeScript.transform.position;
            Quaternion relativeRotation = Quaternion.Inverse(hololensRegCubeScript.transform.rotation) * headRotation;

            otherPlatformSettingObject.transform.position = relativePosition + questCube.transform.position;
            otherPlatformSettingObject.transform.rotation = questCube.transform.rotation * relativeRotation;

            otherPlatformSettingObject.SetActive(true);

        }
    }
}
