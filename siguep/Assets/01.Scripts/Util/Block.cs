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

    private void Awake()
    {
        ms = GetComponent<MeshRenderer>();
        mat = ms.material;

        index = 
            mat.name[0] == 'R' ? 0 : 
            mat.name[0] == 'P' ? 1 : 2;

        elt = (Element)index;
    }

    private void Start()
    {
        _controller = GameManager.Instance.playerTrm.GetComponent<PlayerController>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
        }
    }

    public void OnDamaged(int damage)
    {
        if (_controller.ElementCorrect(elt))
        {
            BreakABlock();
        }
        else
        {
            _controller.PHit();
        }
    }

    private void BreakABlock()
    {
        //이때 블록을 부술거임
        Instantiate(Resources.Load<GameObject>(resourceName), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
