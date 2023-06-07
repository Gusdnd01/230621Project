using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUI :MonoBehaviour
{   
    UIDocument doc;

    VisualElement root;
    VisualElement fishingWindow;

    [SerializeField]
    VisualTreeAsset arrow;

    [SerializeField]
    private List<Texture2D> _texList = new List<Texture2D>();



    private void Awake() {
        doc = GetComponent<UIDocument>();
    }

    private void OnEnable() {
        root = doc.rootVisualElement;
        fishingWindow = root.Q<VisualElement>("FishingWindow");
    }

    public void InstanceArrow(int randNum){
        VisualElement arrowXML = arrow.Instantiate();
        arrowXML.AddToClassList("arrow");
        arrowXML.Q("Image").style.backgroundImage = _texList[randNum];
        fishingWindow.Add(arrowXML);
    }

    public void Remover(){
        fishingWindow.RemoveAt(0);
    }

    public void ClearArrow(){
        fishingWindow.Clear();        
    }
}
