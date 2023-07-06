using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainGameBlock : MonoBehaviour
{
    [SerializeField] private GameObject _map;
    [SerializeField] private Transform _initTransform;

    [SerializeField] private float _maxDistance;
    [SerializeField] private float _initDistance;
    [SerializeField] private int _minCubeCount = 4;

    public UnityEngine.Events.UnityEvent<string> _startTxt;

    [SerializeField] private Queue<GameObject> _queue = new Queue<GameObject>();

    [SerializeField] private List<GameObject> _cubeKinds = new List<GameObject>();
    public List<GameObject> _cubes = new List<GameObject>();

    [SerializeField] private Image _img;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(.5f);
        _startTxt?.Invoke("끝까지 살아남으세요");
        _img.rectTransform.DOScaleY(1, .5f);
        FindAnyObjectByType<MainUI>().TurnOn();
        yield return new WaitForSeconds(5f);
        _startTxt?.Invoke(" ");
    }
    private void FixedUpdate()
    {
        if(_cubes.Count < _minCubeCount)
        {
            CubeInst();
        }

        if (isMapInst())
        {
            MapInst();
        }
    }
    public void Dead()
    {
        StartCoroutine(OnDeadCoroutine());
    }
    private IEnumerator OnDeadCoroutine()
    {
        _startTxt?.Invoke("게임 종료.");
        _img.rectTransform.DOScaleY(0, .5f);
        yield return new WaitForSeconds(1.0f);
        FindObjectOfType<MainUI>().Scenemove();
    }
    private void CubeInst()
    {
        int randnum = 0;
        for(int i = 0; i < 5; ++i)
        {
            randnum = Random.Range(0, 8);

            GameObject obj = Instantiate(_cubeKinds[randnum], _initTransform.position + new Vector3(0,2.35f,(i+1)*30f), Quaternion.identity);

            if(obj.name[0] == 'N')
            {
                for(int j = 0; j < obj.transform.childCount; ++j)
                {
                    _cubes.Add(obj.transform.GetChild(j).gameObject);
                }
            }
            else
            {
                _cubes.Add(obj);
            }
        }
    }

    private void MapInst()
    {
        _initTransform.position = _initTransform.position + new Vector3(0, 0, _maxDistance);

        GameObject obj = Instantiate(_map, _initTransform.position, Quaternion.identity);
        _queue.Enqueue(obj);

        if(_queue.Count > 7)
        {
            Destroy(_queue.Dequeue());
        }
    }

    private bool isMapInst()
    {
        if(Vector3.Distance(_initTransform.position, GameManager.Instance.playerTrm.position) < _initDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
