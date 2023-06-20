using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
public class Block : MonoBehaviour, IDamageable
{
    public string resourceName;

    private Element elt;
    int index = 0;
    MeshRenderer ms;
    Material mat;

    public List<AudioClip> clip = new List<AudioClip>();

    private PlayerController _controller;

    private ParticleSystem _explosion;

    private MainGameBlock _blockCompo;
    public Stage1 _mi;
    public MainGameBlock _mgb;


    private void Awake()
    { 
        ms = GetComponent<MeshRenderer>();
        mat = ms.material;

        index = 
            mat.name[0] == 'R' ? 0 : 
            mat.name[0] == 'P' ? 1 : 2;

        elt = (Element)index;

        _mi = FindObjectOfType<Stage1>();
        _mgb = FindObjectOfType<MainGameBlock>();
        if (_mi == null || _mgb == null)
        {
            return;
        }
    }

    private void Start()
    {
        _controller = GameManager.Instance.playerTrm.GetComponent<PlayerController>();
    }
    public void OnDamaged(int damage)
    {
        if (_controller.ElementCorrect(elt))
        {
            BreakABlock();
            SoundManager.Instance.SFXPlay(clip[0]);

            _controller.PHit(-10, false);
        }
        else
        {
            SoundManager.Instance.SFXPlay(clip[1]);
            _controller.PHit(10, true);
        }
    }

    private void BreakABlock()
    {
        GameManager.Instance.playerTrm.GetComponent<PlayerController>().timer = 0f;
        //이때 블록을 부술거임
        Instantiate(Resources.Load<GameObject>(resourceName), transform.position, Quaternion.identity);
        if (_mi != null)
        {
            _mi.mainCubes.Remove(gameObject);

            if (_mi.mainCubes.Count <= 0)
            {
                _mi.Clear();
            }

            Destroy(gameObject);
        }
        else if (_mgb != null)
        {
            _mgb._cubes.Remove(gameObject);
            GameManager.Instance.Score += 10;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
