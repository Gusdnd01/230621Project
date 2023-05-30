using System;
using UnityEngine;

public class ActionBase : MonoBehaviour
{
    [SerializeField] protected int windowIndex;
    [SerializeField] protected string className;

    public virtual void StartAction()
    {
        UIManager.Instance.PopupWindow(windowIndex, className);
    }
}
