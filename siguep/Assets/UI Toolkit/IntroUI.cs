using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Rendering.Universal;

using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class IntroUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _root;

    private List<VisualElement> _btnList;
    private VisualElement _startBtn;
    private VisualElement _infinityBtn;
    private VisualElement _optionBtn;
    private VisualElement _quitBtn;
    private VisualElement _optionQuitBtn;

    private VisualElement _titleImage;
    private VisualElement _optionWindow;
    private VisualElement _quitWindow;

    private DropdownField _dropdown;
    private Slider _slider;
    private Toggle _toggle;

    private VisualElement _fadeImage;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioClip _uiSound;
    [SerializeField] private string _startClassName;
    [SerializeField] private List<string> _assetName = new List<string>();
    [SerializeField] private List<UniversalRenderPipelineAsset> assets = new List<UniversalRenderPipelineAsset>();
    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();

        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    private void OnEnable()
    {
        _root = _uiDocument.rootVisualElement;

        _startBtn = _root.Q("StartBtn");
        _optionBtn = _root.Q("OptionBtn");
        _quitBtn = _root.Q("QuitBtn");
        _infinityBtn = _root.Q("InfinityBtn");
        _optionWindow = _root.Q("OptionWindow");
        _optionQuitBtn = _root.Q("OptionQuitBtn");

        _titleImage = _root.Q("TitleImage");

        _fadeImage = _root.Q("fadeImage");

        _dropdown = _root.Q<DropdownField>("Dropdown");
        _slider = _root.Q<Slider>("slider");
        _toggle = _root.Q<Toggle>("toggle");
    }

    void LoadScene(string sceneName)
    {
        _fadeImage.AddToClassList(_startClassName);
        StartCoroutine(DelayCoroutine(1f, () =>
        {
            SceneManager.LoadSceneAsync(sceneName);
        }));
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.BGMPlay(_audioClip);
        _fadeImage.RemoveFromClassList(_startClassName);
        _startBtn.RegisterCallback<ClickEvent>(e =>
        {
            SoundManager.Instance.SFXPlay(_uiSound);
            _startBtn.RemoveFromClassList(_startClassName);
            _optionBtn.RemoveFromClassList(_startClassName);
            _quitBtn.RemoveFromClassList(_startClassName);
            _titleImage.RemoveFromClassList(_startClassName);
            _infinityBtn.AddToClassList(_startClassName);
            LoadScene("StageSelect");
        });

        _infinityBtn.RegisterCallback<ClickEvent>(e =>
        {
            SoundManager.Instance.SFXPlay(_uiSound);
            _startBtn.RemoveFromClassList(_startClassName);
            _optionBtn.RemoveFromClassList(_startClassName);
            _quitBtn.RemoveFromClassList(_startClassName);
            _titleImage.RemoveFromClassList(_startClassName);
            _infinityBtn.AddToClassList(_startClassName);
            LoadScene("InfinityScene");
        });
        _optionBtn.RegisterCallback<ClickEvent>(e =>
        {
            SoundManager.Instance.SFXPlay(_uiSound);
            _optionWindow.AddToClassList(_startClassName);//옵션 윈도우 켜기
        });
        _quitBtn.RegisterCallback<ClickEvent>(e =>
        {
            SoundManager.Instance.SFXPlay(_uiSound);
            _fadeImage.AddToClassList(_startClassName);
            StartCoroutine(DelayCoroutine(.5f, () =>
            {
                Application.Quit();
            }));
        });

        _optionQuitBtn.RegisterCallback<ClickEvent>(e =>
        {
            SoundManager.Instance.SFXPlay(_uiSound);
            _optionWindow.RemoveFromClassList(_startClassName);
        });
        _dropdown.choices = _assetName;
        _dropdown.value = _assetName[0];
        _dropdown.RegisterValueChangedCallback(e =>
        {
            GraphicsSettings.renderPipelineAsset = assets[_dropdown.index];
            print(GraphicsSettings.renderPipelineAsset.name);
        });
        StartCoroutine(DelayCoroutine(0.5f, () =>
        {
            _startBtn.AddToClassList(_startClassName);
            _optionBtn.AddToClassList(_startClassName);
            _quitBtn.AddToClassList(_startClassName);
            _titleImage.AddToClassList(_startClassName);
            _infinityBtn.AddToClassList(_startClassName);
        }));
    }

    private IEnumerator DelayCoroutine(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback.Invoke();
    }
}
