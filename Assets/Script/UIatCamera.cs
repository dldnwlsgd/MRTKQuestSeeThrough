using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIatCamera : MonoBehaviour
{
    private Transform cameraTransform;
    void Start()
    {
        // ī�޶��� Transform�� �����ɴϴ�.
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // UI ������Ʈ�� �׻� ī�޶� ���ϵ��� �����մϴ�.
        Vector3 direction = cameraTransform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        // 180�� ȸ�� �߰�
        rotation *= Quaternion.Euler(0, 180, 0);

        transform.rotation = rotation;
    }
}
