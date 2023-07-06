using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

using UnityEngine.SceneManagement;
public class MainUI : MonoBehaviour
{
    private GlitchEffect _glitchEffect;
    private void Awake()
    {
        _glitchEffect = GetComponent<GlitchEffect>();
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        _glitchEffect.TurnOff();
    }

    public void Scenemove()
    {
        _glitchEffect.TurnOn();
        StartCoroutine(DelayCoroutine(.5f, () =>
        {
            SceneManager.LoadSceneAsync("StageSelect");
        }));
    }

    public void TurnOn()
    {
    }

    private IEnumerator DelayCoroutine(float duration, Action callback)
    {
        yield return new WaitForSeconds(duration);
        callback?.Invoke();
    }
}
