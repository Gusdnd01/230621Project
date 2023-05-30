using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T:Component
{
    public static T Instance;

    private void Awake() {
        if(!TryGetComponent<T>(out T t)){
            gameObject.AddComponent<T>();
        }
        Instance = FindObjectOfType<T>();
    }
}
