using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    public UnityEvent<string> _tutorialTextTrigger = null;
    public UnityEvent _spaceTrigger = null;
    public UnityEvent _textReset = null;
    public UnityEvent _tutorialEndTrigger = null;

    public List<string> _textList = new List<string>();

    public List<GameObject> _cubes = new List<GameObject>();

    private int index = 0;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1.0f);
        _tutorialTextTrigger?.Invoke(_textList[index++]);
        _spaceTrigger?.Invoke();
        StartCoroutine(tutorialTextStart());
    }

    private IEnumerator tutorialTextStart()
    {
        while (true)
        {
            yield return StartCoroutine(Desa());
            if(index >= _textList.Count)
            {
                break;
            }
        }
        StartCoroutine(lastMoment());
    }

    private IEnumerator Desa()
    {
        _textReset?.Invoke();
        _tutorialTextTrigger?.Invoke(_textList[index]);
        switch (index)
        {
            case 1:
                yield return new WaitUntil(() => CameraManager.instance.cameraMove);
                break;
            case 2:
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.S));
                break;
            case 3:
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftShift));
                break;
            case 4:
                Instantiate(_cubes[0], new Vector3(0,0,5), Quaternion.identity);
                Instantiate(_cubes[1], new Vector3(5,0,5), Quaternion.identity);
                Instantiate(_cubes[2], new Vector3(10,0,1), Quaternion.identity);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftShift));
                break;
            default:
                _spaceTrigger?.Invoke();
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                break;
        }
        yield return new WaitForSeconds(_textList[index].Length * .1f);
        index++;
    }

    private IEnumerator lastMoment()
    {
        yield return new WaitForSeconds(3.0f);
        yield return new WaitUntil(() => CameraManager.instance.cameraMove);
        _tutorialTextTrigger?.Invoke("3초 후 메인 화면으로 넘어갑니다.");
        yield return new WaitForSeconds(1.0f);
        SaveSystem.instance.Data.tutorial = true;
        SaveSystem.instance.Save();
    }
}
