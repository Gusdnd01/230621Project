using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    [SerializeField] private Sprite before;
    [SerializeField] private Sprite after;

    private Image img;

    private void Start()
    {
        img = GetComponent<Image>();
    }

    public void Change()
    {
        StartCoroutine(ChangeCo(.1f));
    }

    private IEnumerator ChangeCo(float time)
    {
        img.sprite = after;   
        yield return new WaitForSeconds(time);
        img.sprite = before;   
    }
}
