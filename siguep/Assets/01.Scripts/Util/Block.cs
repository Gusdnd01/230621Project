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

    private PlayerController _controller;

    private ParticleSystem _explosion;

    private MainGameBlock _blockCompo;
    public MainInfinity _mi;


    private void Awake()
    { 
        ms = GetComponent<MeshRenderer>();
        mat = ms.material;

        index = 
            mat.name[0] == 'R' ? 0 : 
            mat.name[0] == 'P' ? 1 : 2;

        elt = (Element)index;

        _mi = FindObjectOfType<MainInfinity>();

        if (_mi == null)
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

            _controller.PHit(-10, false);
        }
        else
        {
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
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
