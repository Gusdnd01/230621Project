using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

using UnityEngine.SceneManagement;
public class MainUI : MonoBehaviour
{
    VisualElement _root;

    VisualElement _fade;


    private void OnEnable()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;

        _fade = _root.Q<VisualElement>("fadeImage");
    }

    public void Scenemove()
    {
        _fade.AddToClassList("on");

        StartCoroutine(DelayCoroutine(.5f, () =>
        {
            SceneManager.LoadSceneAsync("StageSelect");
        }));
    }

    public void TrunOn()
    {
        _fade.RemoveFromClassList("on");
    }

    private IEnumerator DelayCoroutine(float duration, Action callback)
    {
        yield return new WaitForSeconds(duration);
        callback?.Invoke();
    }
}
