using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Random = UnityEngine.Random;

public class FishingRoutineController : ActionBase
{
    public UnityEvent OnFishingTrigger = null;

    public UnityEvent<int, string> OnFishingableTrigger;
    public UnityEvent<int, string> OnDisfishingableTrigger;

    public float fishingPercent = 5f;
    public float waitFishingRoutine = 5f;

    private bool _fishingable;
    private bool _isFishing = false;
    private bool _isActiveGame = true;

    public void RandomFishing(){
        float percent = Random.Range(0f, 100f);
        if(percent <= fishingPercent){
            print("is on fishing");
            _fishingable = true;
            OnFishingableTrigger?.Invoke(windowIndex, className);
        }
        else{
            _fishingable = false;
        }
        print("random fishing");
    }

    private void Start() {
        StartCoroutine(FishingRoutine());
    }

    private IEnumerator FishingRoutine(){
        while(true){
            yield return new WaitUntil(()=>_isActiveGame == true && _isFishing == false);
            RandomFishing();

            yield return new WaitForSeconds(waitFishingRoutine);
        }
    }

    private void Update() {
        if(_fishingable && Input.GetKeyDown(KeyCode.F)){
            _fishingable = false;
            _isFishing = true;
            OnDisfishingableTrigger?.Invoke(windowIndex, className);
            OnFishingTrigger?.Invoke();
        }
    }

    public void FishingEnd(){
        _isFishing = false;
    }
}
