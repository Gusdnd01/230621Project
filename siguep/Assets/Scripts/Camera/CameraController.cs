using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private float _sensitivity;
    float xRotation = 0;

    private void Update()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        float h = Input.GetAxis("Mouse X");
        xRotation += h * _sensitivity;

        _root.rotation = Quaternion.Euler(0, xRotation, 0);
    }
}
