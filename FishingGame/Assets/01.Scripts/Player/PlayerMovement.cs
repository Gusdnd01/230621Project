using System.Collections;
using System.Collections.Generic;
using Ditzelgames;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform Motor;
    public float SteerPower = 500f;
    public float Power = 5f;
    public float MaxSpeed = 1f;
    public float Drag = .5f;
    float fade;

    public ParticleSystem bubble;
    ParticleSystem.EmissionModule module;
    protected Rigidbody rb;
    protected Quaternion StartRotation;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        StartRotation = Motor.localRotation;
        module = bubble.emission;
    }

    private void FixedUpdate() {
        if(Input.GetKey(KeyCode.LeftShift)){
            MaxSpeed = 14f;
        }
        else{
            MaxSpeed = 8f;
        }
        var forceDirection = transform.forward;
        var steer = 0;

        if(Input.GetKey(KeyCode.A)){
            steer = -1;
        }
        if(Input.GetKey(KeyCode.D)){
            steer = 1;
        }

        rb.AddForceAtPosition(steer * transform.right * SteerPower/100f, Motor.position);

        var forward = Vector3.Scale(new Vector3(1,0,1), transform.forward);

        if(Input.GetKey(KeyCode.W)){
            PhysicsHelper.ApplyForceToReachVelocity(rb, forward * MaxSpeed, Power);
            
        }

        if(Input.GetKey(KeyCode.S)){
            PhysicsHelper.ApplyForceToReachVelocity(rb, forward * -MaxSpeed, Power);
        }
        fade = Input.GetAxis("Vertical");
        module.rateOverTime = fade * 30;

        Motor.SetPositionAndRotation(Motor.position, transform.rotation * StartRotation * Quaternion.Euler(0,30f * steer,0));
    }
}
