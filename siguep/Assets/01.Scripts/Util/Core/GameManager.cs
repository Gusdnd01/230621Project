using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform playerTrm;
    public ParticleSystem _dashFX;
    private bool _isDash;

    private void Awake()
    {
        Instance = this;
        _dashFX.gameObject.SetActive(false);
    }
    public void SetDash(bool value)
    {
        _dashFX.gameObject.SetActive(value);
        _isDash = value;
    }
    public bool GetDash()
    {
        return _isDash;
    }
}

namespace Core
{
    public enum Element
    {
        FIRE = 0,
        POISON,
        FOREST,
    }
}