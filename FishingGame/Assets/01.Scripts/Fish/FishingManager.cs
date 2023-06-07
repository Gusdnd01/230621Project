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
    public UnityEvent<int> OnFishingSetter = null;
    public UnityEvent<int, string> OnFishingEndTrigger = null;
    public UnityEvent OnFishingEnder = null;

    [SerializeField]
    private List<ARROW_TYPE> at = new List<ARROW_TYPE>();

    [SerializeField]
    private MainUI mainUI;

    private void Awake()
    {
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
        int randNum = 0;
        for (int i = 0; i < cnt; ++i)
        {
            randNum = Random.Range(0, 4);
            at.Add((ARROW_TYPE)randNum);
            OnFishingSetter?.Invoke(randNum);
        }

        while (waterDepth > 0)
        {
            if (at.Count <= 0)
            {
                waterDepth -= 2f;
                for (int i = 0; i < cnt; ++i)
                {
                    randNum = Random.Range(0, 4);
                    at.Add((ARROW_TYPE)randNum);
                    OnFishingSetter?.Invoke(randNum);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) && at[0] == ARROW_TYPE.UP)
                {
                    print("up");
                    mainUI.Remover();
                    at.RemoveAt(0);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) && at[0] == ARROW_TYPE.DOWN)
                {
                    print("down");
                    mainUI.Remover();
                    at.RemoveAt(0);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && at[0] == ARROW_TYPE.LEFT)
                {
                    print("left");
                    mainUI.Remover();
                    at.RemoveAt(0);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) && at[0] == ARROW_TYPE.RIGHT)
                {
                    print("right");
                    mainUI.Remover();
                    at.RemoveAt(0);
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

        at.Clear();

        OnFishingEndTrigger?.Invoke(windowIndex, className);
        OnFishingEnder?.Invoke();
    }
}
