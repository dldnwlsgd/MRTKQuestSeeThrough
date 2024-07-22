using MRTK.Tutorials.MultiUserCapabilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RegisterationManager : MonoBehaviour
{

    public GameObject ownRegistrationCube;
    public GameObject otherRegistrationCube;
    public GameObject trackingObject;
    public GameObject untrackingObject;

    public GameObject otherPlatformSettingObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // button -> tracking stoped and anchor object to space
    public void SetUnTrackingObjectTransform()
    {
        untrackingObject.transform.position = trackingObject.transform.position;
        untrackingObject.transform.rotation = trackingObject.transform.rotation;
        untrackingObject.SetActive(true);
    }


    public void GetPositionRPCdataAndSetObject()
    {

        RegisterationCube ownRegCubeScript = ownRegistrationCube.GetComponent<RegisterationCube>();


        if (ownRegCubeScript == null)
        {
            Debug.LogError("RegistrationCube script is not assigned.");
            return;
        }

        Vector3 headPosition = ownRegCubeScript.GetPosition();
        Quaternion headRotation = ownRegCubeScript.GetRotation();

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

            Vector3 relativePosition = headPosition - ownRegistrationCube.transform.position;
            Quaternion relativeRotation = Quaternion.Inverse(ownRegistrationCube.transform.rotation) * headRotation;

            otherPlatformSettingObject.transform.position = relativePosition + otherRegistrationCube.transform.position;
            otherPlatformSettingObject.transform.rotation = otherRegistrationCube.transform.rotation * relativeRotation;

            otherPlatformSettingObject.SetActive(true);

        }
    }
}
