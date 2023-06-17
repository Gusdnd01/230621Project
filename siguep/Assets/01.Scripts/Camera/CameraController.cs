using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private float _sensitivity;
    float xRotation = 0;

    private PlayerAnimator _animator;


    private void Awake()
    {
        _animator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        float h;
        if (!GameManager.Instance.GetDash())
        {
            h = Input.GetAxis("Mouse X");

            if (h != 0)
            {
                CameraManager.instance.cameraMove = true;
            }
            else
            {
                CameraManager.instance.cameraMove = false;
            }
        }
        else
        {
            h = 0;
        }

        xRotation += h * _sensitivity;

        _root.rotation = Quaternion.Euler(0, xRotation, 0);
    }
}
