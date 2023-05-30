using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IntroUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _root;

    private List<VisualElement> _btnList;
    private VisualElement _startBtn;
    private VisualElement _optionBtn;
    private VisualElement _quitBtn;

    private void Awake() {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable() {
        _root = _uiDocument.rootVisualElement;

        _startBtn = _root.Q("StartBtn");
        _optionBtn = _root.Q("OptionBtn");
        _quitBtn= _root.Q("QuitBtn");
    }

    void Start()
    {
        _startBtn.AddToClassList("start");
        _optionBtn.AddToClassList("start");
        _quitBtn.AddToClassList("start");
    }

    void Update()
    {
        
    }
}
