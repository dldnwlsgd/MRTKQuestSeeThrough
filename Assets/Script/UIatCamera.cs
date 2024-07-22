using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIatCamera : MonoBehaviour
{
    private Transform cameraTransform;
    void Start()
    {
        // 카메라의 Transform을 가져옵니다.
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // UI 오브젝트가 항상 카메라를 향하도록 설정합니다.
        Vector3 direction = cameraTransform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        // 180도 회전 추가
        rotation *= Quaternion.Euler(0, 180, 0);

        transform.rotation = rotation;
    }
}
