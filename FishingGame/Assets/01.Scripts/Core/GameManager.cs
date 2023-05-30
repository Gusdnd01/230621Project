using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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