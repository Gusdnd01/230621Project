using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameManager : MonoSingleton<GameManager>
{
    private void Awake() {
        Init();
    }
    private void Init(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}