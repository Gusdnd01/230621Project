using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    [SerializeField] private float yDelta;
    [SerializeField] private Vector2 xRange;

    private PlayerCam _playerCam;

    private void Awake() {
        _playerCam = GetComponentInChildren<PlayerCam>();
    }

    private float RandPos(){
        float xDelta = Random.Range(xRange.x, xRange.y);
        return xDelta;
    }

    public void DoRecoil(){
        _playerCam.xNewRot -= yDelta;
        _playerCam.yNewRot += RandPos();
    }   
}
