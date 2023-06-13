using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float speed;
    private float offset = 0;
    private void FixedUpdate() {
        if(offset >= 360) offset = 0;

        transform.rotation = Quaternion.Euler(0,offset,0);
        offset += Time.deltaTime * speed;
    }
}
