using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoSingleton<UIManager>
{
    List<VisualElement> _popupList = new List<VisualElement>();

    public void PopupWindow(int index, string className){
        _popupList[index].AddToClassList(className);
    }

    public void PopdownWindow(int index, string className){
        _popupList[index].RemoveFromClassList(className);
    }   
}
