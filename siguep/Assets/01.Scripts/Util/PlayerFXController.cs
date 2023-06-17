using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerFXController : MonoBehaviour
{
    Dictionary<string, ParticleSystem> _fx = new Dictionary<string, ParticleSystem>();
    [SerializeField]List<ParticleSystem> _fxList = new List<ParticleSystem>();

    private void Awake()
    {
        _fx.Add("Dash", _fxList[0]);
        _fx.Add("Move", _fxList[1]);
        _fx.Add("Hit", _fxList[2]);
    }


    public void Play(string name)
    {
        _fx[name].Play();
    }

    internal void Stop(string v)
    {
        _fx[v].Stop();
    }
}
