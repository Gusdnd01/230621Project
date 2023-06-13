using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCam : MonoBehaviour
{
    [Header("Cam Sensitivity")]
    public float sensX;
    public float sensY;

    [Space(10)]
    public float _rotSpeed;

    [Header("Transforms")]
    public Transform orientation;
    public Transform camPos;

    private float xRotation;
    private float yRotation;

    [Header("Other")]
    public float xNewRot;
    public float yNewRot;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MoveCam();
    }

    private void MoveCam()
    {

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yNewRot += mouseX;

        xNewRot -= mouseY;
        xNewRot = Mathf.Clamp(xNewRot, -90f, 90f);

        yRotation = Mathf.Lerp(yRotation, yNewRot, Time.deltaTime * _rotSpeed);
        xRotation = Mathf.Lerp(xRotation, xNewRot, Time.deltaTime * _rotSpeed);

        camPos.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }

    public void DoFov(float endvalue)
    {
        GetComponent<Camera>().DOFieldOfView(endvalue, .25f);
    }

    public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), .25f);
    }
}
