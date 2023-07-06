using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System;

public class StageSelectManager : MonoBehaviour
{
    public int ChapterIndex = 0;

    [SerializeField] private List<CinemachineVirtualCamera> vcams = new List<CinemachineVirtualCamera>();

    private GlitchEffect _glitchEffect;

    private void Awake()
    {
        _glitchEffect = GetComponent<GlitchEffect>();
    }

    private void Start()
    {
        CamSetter();
    }

    public void SceneMovement(int index)
    {
        _glitchEffect.TurnOn();
        StartCoroutine(DelayCoroutine(1.0f, () =>
        {
            SceneManager.LoadSceneAsync(index);
        }));
    }

    IEnumerator DelayCoroutine(float time,Action callback)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }

    private void CamSetter()
    {
        vcams.ForEach(c => c.Priority = 10);

        vcams[ChapterIndex].Priority = 15;
    }

    public void SetChapterIndex(int index)
    {
        ChapterIndex = index;
        CamSetter();
    }
}
