using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoSingleton<UIManager>
{
    public UIDocument _uiDocument;

    List<VisualElement> _popupList = new List<VisualElement>();
    VisualElement root;
    private void OnEnable() {
        root = _uiDocument.rootVisualElement;
        _popupList.Add(root.Q("FishingWindow"));
        _popupList.Add(root.Q("Fishingable"));
    }

    public void PopupWindow(int index, string className){
        _popupList[index].AddToClassList(className);
    }

    public void PopdownWindow(int index, string className){
        _popupList[index].RemoveFromClassList(className);
    }   
}
