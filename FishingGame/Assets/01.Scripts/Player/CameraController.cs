using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientaion;

    private float yRotation;
    private float xRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        float h = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float v = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += h;

        xRotation -= v;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientaion.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
