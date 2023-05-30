using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

using UnityEngine.SceneManagement;

public class IntroUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _root;

    private List<VisualElement> _btnList;
    private VisualElement _startBtn;
    private VisualElement _optionBtn;
    private VisualElement _quitBtn;

    private VisualElement _titleImage;
    private VisualElement _optionWindow;
    private VisualElement _quitWindow;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _uiDocument.rootVisualElement;

        _startBtn = _root.Q("StartBtn");
        _optionBtn = _root.Q("OptionBtn");
        _quitBtn = _root.Q("QuitBtn");

        _titleImage = _root.Q("TitleImage");
    }

    void Start()
    {
        _startBtn.RegisterCallback<ClickEvent>(e=>{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        });
        _optionBtn.RegisterCallback<ClickEvent>(e=>{
            _optionWindow.AddToClassList("start");//옵션 윈도우 켜기
        });
        _quitBtn.RegisterCallback<ClickEvent>(e=>{
            Application.Quit();//ㅣㅇㅁ시로 놓은거고 나중에 디테일 작업해야함
        });

        StartCoroutine(DelayCoroutine(0.5f, () =>
        {
            _startBtn.AddToClassList("start");
            _optionBtn.AddToClassList("start");
            _quitBtn.AddToClassList("start");
            _titleImage.AddToClassList("start");
        }));
    }

    private IEnumerator DelayCoroutine(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback.Invoke();
    }

    void Update()
    {
        //Do nothing
    }
}
