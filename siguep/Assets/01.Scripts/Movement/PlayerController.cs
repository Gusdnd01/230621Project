using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Core;
using UnityEngine.UI;

using Random = UnityEngine.Random;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _maxHP;

    [SerializeField] private Slider _slider;

    [SerializeField] private float _hpMultiplier = 1.0f;
    [SerializeField] private float _damageMultiplier = 1.0f;

    private Vector3 moveDir;
    private CharacterController _controller;
    private PlayerAnimator _animator;
    private PlayerFXController _fxController;
    private PlayerColorChanger _colorChanger;

    private float _curHP;

    private bool _isDash = false;
    private bool _isBerserk = false;
    private bool _isDead = false;

    private Element _element;

    private float _dashCool = 3.0f;

    private int _dashCount = 3; 

    public float timer = 0;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<PlayerAnimator>();
        _fxController = GetComponent<PlayerFXController>();
        _colorChanger = GetComponent<PlayerColorChanger>();
        Init();
    }

    public CharacterController GetController()
    {
        return _controller;
    }

    private void Init()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _element = Element.FIRE;
        _isDash = false;
        _isBerserk = false;

        _curHP = _maxHP;
    }

    int elementIndex = 0;

    private void Update()
    {
        if (_isDead || !GameManager.Instance.isMoving) return;
        _curHP -= Time.deltaTime * _hpMultiplier;
        _curHP = Mathf.Clamp(_curHP, -1, _maxHP);
        _slider.value = _curHP/_maxHP;

        if( _curHP <= 0)
        {
            OnDead();
        }

        DashTrigger();
        CalcDashTimer();
        InputDir(ref moveDir);
        if (_isDash)
        {
            moveDir.z = _dashSpeed;
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _element = Element.FIRE;
                _colorChanger.ColorChange((int)_element);
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                _element = Element.POISON;

                _colorChanger.ColorChange((int)_element);
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                _element = Element.FOREST;

                _colorChanger.ColorChange((int)_element);
            }
        }
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

    public void OnDead()
    {
        _isDead = true;
        if(FindAnyObjectByType<Stage>() != null)
        {
            FindAnyObjectByType<Stage>().Dead();
        }
        else if(FindAnyObjectByType<MainGameBlock>() != null)
        {
            FindAnyObjectByType<MainGameBlock>().Dead();
        }
        
        _animator.SetDie();
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
        timer = 0;
        GameManager.Instance.SetDash(false);
        _animator.SetRun(false);
    }

    private void CalcDashTimer()
    {
        if (_isDash)
        {
            timer += Time.deltaTime;
            if (timer > 3f)
            {
                _animator.SetRun(false);
                _isDash = false;
                timer = 0;
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

    public void PHit(int damage, bool hitAnimation)
    {
        _curHP -= damage * _damageMultiplier;
        if (hitAnimation)
        {
            _animator.SetHitTrigger();
            EndDash();
        }
    }
}
