using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EndManager : MonoBehaviour
{
    UIDocument doc;
    VisualElement root;
    VisualElement _fade;
    public UnityEvent<string> _startTextTrigger = null;
    private void Awake()
    {
        doc = GetComponent<UIDocument>();
    }


    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        _fade = root.Q<VisualElement>("fadeImage");
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(.5f);
        _fade.RemoveFromClassList("on");
        yield return new WaitForSeconds(1f);
        _startTextTrigger?.Invoke("모든 테스트를 통과하셨습니다. 감사합니다.");
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        GameManager.Instance.stage.ToList().ForEach(b => b = false);
        Scenemove();
    }
    public void Scenemove()
    {
        _fade.AddToClassList("on");

        StartCoroutine(DelayCoroutine(.5f, () =>
        {
            for(int i = 0; i < 3; i++)
            {
                PlayerPrefs.SetInt($"Stage{i}", 0);
            }
            SceneManager.LoadSceneAsync("Intro");
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
