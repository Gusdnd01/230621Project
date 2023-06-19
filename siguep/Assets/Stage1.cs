using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Stage1 : MonoBehaviour
{
    public UnityEvent<string> _startTextTrigger = null;
    public UnityEvent _textReset = null;

    public List<GameObject> _cubes = new List<GameObject>();
    public List<GameObject> mainCubes = new List<GameObject>();

    public int cubeCount;

    public Transform _initPosition;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(lastMoment());
    }


    private IEnumerator lastMoment()
    {
        StartGame();
        _startTextTrigger?.Invoke("테스트를 시작합니다.");
        yield return new WaitForSeconds(2.0f);
        _startTextTrigger?.Invoke("제한시간 안에 블록을 모두 부수세요!");
        yield return new WaitForSeconds(3.0f);
        _textReset?.Invoke();
        yield return new WaitForSeconds(1.0f);

    }

    private void StartGame()
    {
        for (int i = 0; i < _cubes.Count; ++i)
        {
            GameObject obj = Instantiate(_cubes[i], _initPosition.position + new Vector3(0,2.35f, 15f * i), Quaternion.identity);
            mainCubes.Add(obj);
        }
    }
}
