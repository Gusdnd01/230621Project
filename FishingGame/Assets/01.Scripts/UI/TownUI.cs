using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class TownUI : MonoBehaviour
{

    private UIDocument _uiDocument;
    private VisualElement _root;

    private VisualElement _healWindow;
    private VisualElement _healQuit;
    private VisualElement _shopWindow;
    private VisualElement _shopQuit;
    private VisualElement _houseWindow;
    private VisualElement _houseQuit;
    private VisualElement _optionWindow;
    private VisualElement _optionQuit;

    private void Awake() {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable() {
        _root = _uiDocument.rootVisualElement;

        _healWindow = _root.Q("Heal");
        _shopWindow = _root.Q("Shop");
        _houseWindow = _root.Q("House");
        _optionWindow = _root.Q("OptionWindow");

        _healQuit = _root.Q<Button>("HealQuit");
        _shopQuit= _root.Q<Button>("ShopQuit");
        _houseQuit= _root.Q<Button>("HouseQuit");
        _optionQuit= _root.Q<Button>("OptionQuit");
    }

    public void OnWindow(){
        
    }
}
