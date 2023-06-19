using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class MainInfinity : MonoBehaviour
{
    public UnityEvent<string> _startTextTrigger = null;
    public UnityEvent _textReset = null;

    public List<Transform> transforms = new List<Transform>();

    public List<GameObject> _cubes = new List<GameObject> ();
    public List<GameObject> mainCubes = new List<GameObject>();

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(lastMoment());

        transforms = GameObject.Find("Transforms").GetComponentsInChildren<Transform>().ToList();

        transforms.RemoveAt(0);
    }


    private IEnumerator lastMoment()
    {
        _startTextTrigger?.Invoke("테스트를 시작합니다.");
        yield return new WaitForSeconds(2.0f);
        _startTextTrigger?.Invoke("랜덤으로 나오는 블록을 모두 부수세요!");
        yield return new WaitForSeconds(3.0f);
        _textReset?.Invoke();
        yield return new WaitForSeconds(1.0f);

        while (true)
        {
            yield return new WaitUntil(() => mainCubes.Count == 0);
            StartGame();
        }
    }

    private void StartGame()
    {
        int randnum = Random.Range(0, 6);
        Suffle();
        for(int i = 0; i < randnum; ++i)
        {
            GameObject obj = Instantiate(_cubes[Random.Range(0, _cubes.Count)], transforms[i].position + new Vector3(0, 2.35f, 0), Quaternion.identity);
            mainCubes.Add(obj);
        }
    }

    private void Suffle()
    {
        int idx1 = 0;
        int idx2 = 0;

        for (int i = 0; i < 50; ++i)
        {
            idx1 = Random.Range(0, transforms.Count);
            idx2 = Random.Range(0, transforms.Count);

            Transform temp = transforms[idx1];
            transforms[idx1] = transforms[idx2];
            transforms[idx2] = temp;
        }
    }
}
