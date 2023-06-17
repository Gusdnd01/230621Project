using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Core;

using Random = UnityEngine.Random;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _dashSpeed;
    private Vector3 moveDir;
    private CharacterController _controller;
    private PlayerAnimator _animator;
    private PlayerFXController _fxController;

    private bool _isDash = false;
    private bool _isBerserk = false;
    private Element _element;

    private float _dashCool = 3.0f;

    private int _dashCount = 3; 

    [SerializeField]
    private float _timer = 0;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<PlayerAnimator>();
        _fxController = GetComponent<PlayerFXController>();
        Init();
    }

    private void Init()
    {
        _element = Element.FIRE;
        _isDash = false;
        _isBerserk = false;
    }

    private void Update()
    {
        DashTrigger();
        CalcDashTimer();
        InputDir(ref moveDir);
        if (_isDash) moveDir.z = _dashSpeed;
        Movement(transform.forward * moveDir.z);
    }

    private void FixedUpdate()
    {
        GameManager.Instance.SetDash(_isDash);
        if (moveDir.sqrMagnitude > 0.1f)
        {
            _fxController.Play("Move");
            _animator.SetWalk(true);
        }
        else
        {
            _fxController.Stop("Move");
            _animator.SetRun(false);
            _animator.SetWalk(false);
        }
    }

    //버서크, 그냥 광폭화 효과임
    public void Berserk()
    {
        _isBerserk = true;
    }

    public bool ElementCorrect(Element elt)
    {
        if (_element == elt)
        {
            //블록을 부술거임
            return true;
        }
        else
        {
            //그냥 부딫히기만 할거임 
            return false;
        }
    }

    public void EndDash()
    {
        _isDash = false ;
        _timer = 0;
        GameManager.Instance.SetDash(false);
        _animator.SetRun(false);
    }

    private void CalcDashTimer()
    {
        if (_isDash)
        {
            _timer += Time.deltaTime;
            if (_timer > 3f)
            {
                _animator.SetRun(false);
                _isDash = false;
                _timer = 0;
            }
        }
    }

    private void DashTrigger()
    {
        if (_isDash == false && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            _dashCool = 3.0f;
            _dashCount--;
            _fxController.Play("Dash");
            _isDash = true;
            _animator.SetRun(true);
        }
    }

    private void InputDir(ref Vector3 dir)
    {
        if (!_isDash)
        {
            float v = Input.GetAxis("Vertical");

            dir = new Vector3(0, 0, v);
        }
    }

    private void Movement(Vector3 input)
    {
        _controller.Move(input * Time.deltaTime * _speed);
    }

    public void PHit()
    {
        _animator.SetHitTrigger();
        EndDash();
    }
}
