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

    public bool[] allElement = { false, false, false };

    private int index = 0;
    public Transform _initTrm;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(tutorialTextStart());
    }

    private IEnumerator tutorialTextStart()
    {
        while (true)
        {
            yield return StartCoroutine(Desa());
            if (index >= _textList.Count)
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
            case 3:
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S));
                yield return new WaitForSeconds(2.0f);
                break;
            case 4:
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftShift));
                yield return new WaitForSeconds(2.0f);
                break;
            case 5:
                int randNum = 0;
                for (int i = 0; i < 15; ++i)
                {
                    randNum = Random.Range(0, 3);
                    Instantiate(_cubes[randNum], _initTrm.position + new Vector3(0, 0, 15 * i), Quaternion.identity);
                }
                GameManager.Instance.playerTrm.GetComponent<PlayerController>().EndDash();
                GameManager.Instance.playerTrm.position = new Vector3(0, 0, 0);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftShift));
                yield return new WaitForSeconds(3.0f);
                break;
            default:
                _spaceTrigger?.Invoke();
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                yield return new WaitForSeconds(1.0f);
                break;
        }
        index++;
    }

    private IEnumerator lastMoment()
    {
        _tutorialTextTrigger?.Invoke("3초 후 메인 화면으로 넘어갑니다.");
        yield return new WaitForSeconds(3.0f);
        _tutorialEndTrigger?.Invoke();
    }
}
