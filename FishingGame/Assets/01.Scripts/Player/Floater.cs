using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float _floatingValue;
    [SerializeField] private float _displaceValue;

    [SerializeField] private int floaterCount;

    [SerializeField] private float waterDrag = .99f;
    [SerializeField] private float angularDrag = 2f;

    private void FixedUpdate() {
        rb.AddForceAtPosition(Physics.gravity/floaterCount,transform.position, ForceMode.Acceleration);

        float waveHeight = WaveManager.Instance.GetWaveHeight(transform.position.x);
        if(transform.position.y < waveHeight){
            float displacement = Mathf.Clamp01((waveHeight-transform.position.y) / _floatingValue ) * _displaceValue;
            rb.AddForceAtPosition(new Vector3(0f,Mathf.Abs(Physics.gravity.y) * displacement,0),transform.position,ForceMode.Acceleration);
            rb.AddForce(displacement * -rb.velocity *waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rb.AddTorque(displacement * -rb.angularVelocity *angularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
