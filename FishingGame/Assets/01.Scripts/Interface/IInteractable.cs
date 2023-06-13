using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class IInteractable: MonoBehaviour 
{
    public UnityEvent OnInteractTrigger = null;

    public virtual void Interact(){
        OnInteractTrigger?.Invoke();
    }
}
