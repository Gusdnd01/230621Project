using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateUpdater : MonoBehaviour
{
    private Light light;

    [SerializeField] private float _dateSpeed;
    float _curDate = 0.0f;

    private void Awake()
    {
        light = GetComponent<Light>();
    }

    private void Update()
    {
        _curDate += Time.deltaTime * _dateSpeed;

        light.intensity = 4 * Mathf.Sin(_curDate) + 2f;
    }
}
