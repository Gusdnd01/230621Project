using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Core;
using System;

using Random = UnityEngine.Random;

[System.Serializable]
public enum ARROW_TYPE
{
    UP = 0,
    DOWN,
    LEFT,
    RIGHT,
};
public class FishingManager : ActionBase
{
    public static FishingManager Instance;
    [SerializeField] private float time;
    [SerializeField] private int cnt;
    [SerializeField] private float mulAmount = 1.0f;

    [SerializeField] private float waterDepth;

    public UnityEvent<int, string> OnFishingStartTrigger = null;
    public UnityEvent<int, string> OnFishingEndTrigger = null;

    [SerializeField]
    private List<ARROW_TYPE> at = new List<ARROW_TYPE>();

    private void Awake() {
        Instance = this;
    }

    public void Fishing()
    {
        waterDepth = Random.Range(20f, 40f);
        StartCoroutine(StartFishing(time));
    }

    private IEnumerator StartFishing(float time)
    {
        OnFishingStartTrigger?.Invoke(windowIndex, className);
        for (int i = 0; i < cnt; ++i)
        {
            at.Add((ARROW_TYPE)Random.Range(0, 4));
        }

        while (waterDepth > 0)
        {
            //그리는 작업
            /* for(int i =0; i < cnt; ++i){

            } */

            if (at.Count <= 0)
            {
                for (int i = 0; i < cnt; ++i)
                {
                    at.Add((ARROW_TYPE)Random.Range(0, 4));
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) && at[0] == ARROW_TYPE.UP)
                {
                    print("up");
                    at.RemoveAt(0);
                    waterDepth -= 0.5f;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) && at[0] == ARROW_TYPE.DOWN)
                {
                    print("down");
                    at.RemoveAt(0);
                    waterDepth -= 0.5f;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && at[0] == ARROW_TYPE.LEFT)
                {
                    print("left");
                    at.RemoveAt(0);
                    waterDepth -= 0.5f;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) && at[0] == ARROW_TYPE.RIGHT)
                {
                    print("right");
                    at.RemoveAt(0);
                    waterDepth -= 0.5f;
                }
                else if ((Input.GetKeyDown(KeyCode.UpArrow) && at[0] != ARROW_TYPE.UP) ||
                        (Input.GetKeyDown(KeyCode.DownArrow) && at[0] != ARROW_TYPE.DOWN) ||
                        (Input.GetKeyDown(KeyCode.LeftArrow) && at[0] != ARROW_TYPE.LEFT) ||
                        (Input.GetKeyDown(KeyCode.RightArrow) && at[0] != ARROW_TYPE.RIGHT))
                {
                    waterDepth += 1f;
                }
            }
            waterDepth -= Time.deltaTime * mulAmount;
            yield return null;
        }

        OnFishingEndTrigger?.Invoke(windowIndex, className);
    }
}
