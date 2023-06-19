using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public struct TabInfo
{
    public VisualElement Tab;
    public int Index;
}

public class TabBar : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement _rootUI;

    private List<TabInfo> _tabList;
    private List<VisualElement> _vsList;

    public List<Button> _buttonList;

    private VisualElement _fadeImage;

    public string _startClassName = "start";

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    void LoadScene(int index)
    {
        StartCoroutine(DelayCoroutine(2.3f, () => {
            _fadeImage.AddToClassList(_startClassName);
            StartCoroutine(DelayCoroutine(1f, () => {
                SceneManager.LoadSceneAsync(index);
            }));
        }));
    }
    void LoadScene(string sceneName )
    {
        StartCoroutine(DelayCoroutine(2.3f, () => {
            _fadeImage.AddToClassList(_startClassName);
            StartCoroutine(DelayCoroutine(1f, () => {
                SceneManager.LoadSceneAsync(sceneName);
            }));
        }));
    }

    public List<Button> soundBtn;

    private void OnEnable()
    {
        _tabList = new List<TabInfo>();
        _vsList = new List<VisualElement>();
        _buttonList = new List<Button>();
        _rootUI = _document.rootVisualElement;

        VisualElement content = _rootUI.Q<VisualElement>("Content");

        _fadeImage = _rootUI.Q("fadeImage");

        int idx = 0;
        _rootUI.Query<VisualElement>(className: "tab").ToList().ForEach(t =>
        {
            int myIdx = idx;
            _tabList.Add(new TabInfo { Tab = t, Index = myIdx });
            _vsList.Add(t);
            idx++;

            t.RegisterCallback<ClickEvent>(evt =>
            {
                Off(_vsList);
                content.style.left = new Length(-myIdx * 100, LengthUnit.Percent);
                t.AddToClassList("on");
            });
        });

        int sceneIndex = 0;

        _rootUI.Q<Button>("TutoBtn").RegisterCallback<ClickEvent>(evt => LoadScene("Tutorial"));
        _rootUI.Q<Button>("s1Btn").RegisterCallback<ClickEvent>(evt => LoadScene("Stage1"));
        _rootUI.Q<Button>("s2Btn").RegisterCallback<ClickEvent>(evt => LoadScene("Stage2"));
        _rootUI.Q<Button>("s3Btn").RegisterCallback<ClickEvent>(evt => LoadScene("Stage3"));
        _rootUI.Q<Button>("ComingSoonBtn").RegisterCallback<ClickEvent>(evt => LoadScene("Intro"));
    }

    private void Off(List<VisualElement> list)
    {
        list.ForEach(t =>
        {
            t.RemoveFromClassList("on");
        });
    }

    private IEnumerator DelayCoroutine(float duration, Action callback)
    {
        yield return new WaitForSeconds(duration);
        callback?.Invoke();
    }

    private void QuitGame()
    {
        Application.Quit();
        Debug.Log("게임 종료");
    }
}
