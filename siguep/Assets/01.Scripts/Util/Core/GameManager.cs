using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform playerTrm;
    public ParticleSystem _dashFX;
    private bool _isDash;
    public UnityEvent<string> scoreEvt;
    public UnityEvent<string> maxScoreEvt;
    public bool isMoving = true;

    private int _score = 0;
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            if(SceneManager.GetActiveScene().name == "InfinityScene")
            {
                scoreEvt?.Invoke(Score.ToString() + "점");
            }

            if(value > maxScore)
            {
                //maxScore = PlayerPrefs.GetInt("MaxScore");
                maxScore = value;
                PlayerPrefs.SetInt("MaxScore", maxScore);

                maxScoreEvt?.Invoke("최고 점수 : " + maxScore.ToString() + "점");
            }
        }
    }
    public int maxScore = 0;

    public bool _isInfi = false;
    public bool _isSelect = false;
    public string _sceneName = "New Scene";

    public bool[] stage;

    private void Awake()
    {
        Instance = this;

        if(_dashFX != null)
        {
            _dashFX.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        for(int i = 0; i < stage.Length; i++)
        {
            stage[i]= System.Convert.ToBoolean(PlayerPrefs.GetInt($"Stage{i}"));
        }

        if (_isSelect)
        {
            int isC = 0;
            for(int i = 0; i < stage.Length; i++)
            {
                if(stage[i] == true)
                {
                    isC++;
                }
            }

            if (isC >= 3)
            {
                Scenemove(_sceneName);
            }
        }
        maxScore = PlayerPrefs.GetInt("MaxScore");
        maxScoreEvt?.Invoke("최고 점수 : " + maxScore.ToString() + "점");
    }
    public void Scenemove(string a)
    {
        StartCoroutine(DelayCoroutine(.5f, () =>
        {
            SceneManager.LoadSceneAsync(a);
        }));
    }

    private IEnumerator DelayCoroutine(float duration, Action callback)
    {
        yield return new WaitForSeconds(duration);
        callback?.Invoke();
    }
    public void SetDash(bool value)
    {
        _dashFX.gameObject.SetActive(value);
        _isDash = value;
    }
    public bool GetDash()
    {
        return _isDash;
    }
}

namespace Core
{
    public enum Element
    {
        FIRE = 0,
        POISON,
        FOREST,
    }
}