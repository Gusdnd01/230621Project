using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Stage : MonoBehaviour
{
    public UnityEvent<string> _startTextTrigger = null;
    public UnityEvent _textReset = null;

    public List<GameObject> _cubes = new List<GameObject>();
    public List<GameObject> _notes = new List<GameObject>();
    public List<GameObject> _mulnocu = new List<GameObject>();
    public List<GameObject> mainCubes = new List<GameObject>();

    public AudioClip mainNote;

    public int cubeCount;

    [SerializeField] private int _stageIndex;


    public Transform _initPosition;

    private IEnumerator Start()
    {
        SoundManager.Instance.BGMPlay(mainNote);

        yield return new WaitForSeconds(1.0f);
        
        FindAnyObjectByType<MainUI>().TurnOn();
        
        yield return new WaitForSeconds(1.0f);
        
        StartCoroutine(lastMoment());
    }

    public void Dead()
    {
        StartCoroutine(OnDeadCoroutine());
    }
    private IEnumerator OnDeadCoroutine()
    {
        _startTextTrigger?.Invoke("테스트를 실패하였습니다.");
        yield return new WaitForSeconds(1.0f);
        FindObjectOfType<MainUI>().Scenemove();
    }


    private IEnumerator lastMoment()
    {
        StartGame();
        _startTextTrigger?.Invoke("테스트가 시작되었습니다.");
        yield return new WaitForSeconds(1.0f);
        _startTextTrigger?.Invoke("제한시간 안에 블록을 모두 부수세요!");
        yield return new WaitForSeconds(1.0f);
        _startTextTrigger?.Invoke(" ");
        yield return new WaitForSeconds(1.0f);

    }

    private void StartGame()
    {
        switch (_stageIndex)
        {
            case 1:
                for (int i = 0; i < _cubes.Count; ++i)
                {
                    GameObject obj = Instantiate(_cubes[i], _initPosition.position + new Vector3(0, 2.35f, 15f * i), Quaternion.identity);
                    mainCubes.Add(obj);
                }
                break;
            case 2:
                int randNum = 0;
                for (int i = 0; i < 15; ++i)
                {
                    randNum = Random.Range(0, 8);
                    GameObject obj = Instantiate(_mulnocu[randNum], _initPosition.position + new Vector3(0, 2.35f, 30f * i), Quaternion.identity);
                    if(obj.name[0] == 'N')
                    {
                        for (int j = 0; j < obj.transform.childCount; ++j)
                        {
                            mainCubes.Add(obj.transform.GetChild(j).gameObject);
                        }
                    }
                    else
                    {
                        mainCubes.Add(obj);
                    }
                }
                break;
            case 3:
                for (int i = 0; i < 15; ++i)
                {
                    randNum = Random.Range(0, 5);
                    GameObject obj = Instantiate(_notes[randNum], _initPosition.position + new Vector3(0, 2.35f, 35f * i), Quaternion.identity);
                    for (int j = 0; j < obj.transform.childCount; ++j)
                    {
                        mainCubes.Add(obj.transform.GetChild(j).gameObject);
                    }
                }
                break;
        }
    }

    public void Clear()
    {
        StartCoroutine(ClearCycle());
    }

    private IEnumerator ClearCycle()
    {
        _startTextTrigger?.Invoke("테스트를 완료하였습니다.");
        yield return new WaitForSeconds(1.0f);
        PlayerPrefs.SetInt($"Stage{_stageIndex - 1}", 1);
        FindObjectOfType<MainUI>().Scenemove();
    }
}
