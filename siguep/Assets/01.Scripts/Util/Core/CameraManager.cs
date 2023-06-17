using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] private CinemachineVirtualCamera _vPCam;

    public bool cameraMove = false;

    private CinemachineBasicMultiChannelPerlin _vPPerlin;
    private void Awake()
    {
        instance = this;

       _vPPerlin = _vPCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        print(_vPPerlin);
    }

    public void PCamShake(float frequency, float power, float duration)
    {
        StartCoroutine(CamShake(frequency, power, duration));
    }

    private IEnumerator CamShake(float frequency, float power, float duration)
    {
        _vPPerlin.m_AmplitudeGain = power;
        _vPPerlin.m_FrequencyGain = frequency;
        yield return new WaitForSeconds(duration);
        _vPPerlin.m_AmplitudeGain = 0;
        _vPPerlin.m_FrequencyGain = 0;
    }
}
