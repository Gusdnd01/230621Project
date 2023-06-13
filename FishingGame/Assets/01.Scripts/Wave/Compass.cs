using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] private Transform north;
    [SerializeField] private Transform compass;
    [SerializeField] private Transform player;

    [SerializeField] private float mulAmount = 3f;
    private void FixedUpdate() {
        north.position = player.position + (Vector3.forward * mulAmount);

        Vector3 Dir  = (player.position - north.position);
        Dir.y = -90;
        compass.rotation = Quaternion.LookRotation(Dir);
    }
}
