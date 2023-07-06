using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChapterRotator : MonoBehaviour
{
    private StageSelectManager stageSelectManager;

    [SerializeField] private int _stageCnt = 4;
    [SerializeField] private int _viewIndex;

    [SerializeField] private float _rotateSpeed = 1f;

    [SerializeField] private Transform[] chapterTrms;

    Dictionary<string, ImageChange> keys = new Dictionary<string, ImageChange>();

    private Transform panel;

    private GlitchEffect _glitchEffect;

    private bool _complete = true;
    private float _timer = 0.0f;
    private void Awake()
    {
        _glitchEffect = GetComponent<GlitchEffect>();
    }
    private void Start()
    {
        stageSelectManager = GetComponent<StageSelectManager>();
        panel = GameObject.Find("Canvas").transform.GetChild(0);

        panel.GetComponentsInChildren<Transform>().ToList().ForEach(d =>
        {
            keys.Add(d.name, d.GetComponent<ImageChange>());
        });
        _glitchEffect.TurnOff();
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        _timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _glitchEffect.TurnOn();
            switch (_viewIndex + stageSelectManager.ChapterIndex * 4)
            {
                case 0:
                    stageSelectManager.SceneMovement(3);
                    break;
                case 1:
                    stageSelectManager.SceneMovement(4);
                    break;
                case 2:
                    stageSelectManager.SceneMovement(5);
                    break;
                case 3:
                    stageSelectManager.SceneMovement(0);
                    break;
                case 4:
                    stageSelectManager.SceneMovement(6);
                    break;
                case 5:
                    stageSelectManager.SceneMovement(2);
                    break;
            }
        }
        if (h != 0 && _complete)
        {
            _viewIndex = (_viewIndex + Mathf.FloorToInt(h)) % _stageCnt;
            if(_viewIndex < 0)
            {
                _viewIndex = 3;
            }
            if (h > 0)
            {
                keys["D"].Change();
            }
            else
            {
                keys["A"].Change();
            }
            _complete = false;
        }
        if (v != 0 && _complete)
        {
            stageSelectManager.SetChapterIndex(stageSelectManager.ChapterIndex + Mathf.FloorToInt(v));
            if (stageSelectManager.ChapterIndex <= 0) stageSelectManager.SetChapterIndex(0);
            if (stageSelectManager.ChapterIndex >= chapterTrms.Length) stageSelectManager.SetChapterIndex(chapterTrms.Length);
            if (v > 0)
            {
                keys["W"].Change();
            }
            else
            {
                keys["S"].Change();
            }
            _complete = false;
        }

        if (_timer >= 2.0f)
        {
            _complete = true;
            _timer = 0.0f;
        }
        stageSelectManager.ChapterIndex = Mathf.Clamp(stageSelectManager.ChapterIndex, 0, chapterTrms.Length);
        chapterTrms[stageSelectManager.ChapterIndex].rotation = Quaternion.Lerp(chapterTrms[stageSelectManager.ChapterIndex].rotation, Quaternion.Euler(-90, 90 * _viewIndex, 90), Time.deltaTime * _rotateSpeed);
    }
}
