using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float length = 2f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float offset = 0f;

    private void FixedUpdate(){
        offset += Time.deltaTime * speed;
    }

    public float GetWaveHeight(float x){
        return amplitude * (GetSin(x, .2f, 1) + GetSin(x,.5f,.2f) + GetSin(x, .3f, .1f));
    }

    private float GetSin(float x,float size, float time){
        return size*Mathf.Sin(time*(x / length + offset));
    }
}
