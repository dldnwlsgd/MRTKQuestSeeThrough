using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCubeRegistration : MonoBehaviour
{
    private Vector3 position;
    private Quaternion rotation;

    public NoitceStateUICtrl noitceStateUICtrl;

    public void SetForQuestObjectTransform() // button event quest cube
    {
        position = transform.position;
        rotation = transform.rotation;
        noitceStateUICtrl.ChangeStateOfForQuestCubeAssignSign();

    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public Quaternion GetRotation()
    {
        return rotation;
    }
}
