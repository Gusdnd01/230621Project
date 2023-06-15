using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

using Random = UnityEngine.Random;
public class PlayerController : MonoBehaviour, IDamaged
{
    [SerializeField] private float _speed;
    [SerializeField] private float _dashSpeed;
    private Vector3 moveDir;
    private CharacterController _controller;
    private PlayerAnimator _animator;

    private bool _isDash = false;

    [SerializeField]
    private float _timer= 0;

    public UnityEvent chEvent;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        DashTrigger();
        CalcDashTimer();
        InputDir(ref moveDir);
        if (_isDash) Movement(transform.forward * _dashSpeed);
        else Movement(transform.forward * moveDir.z);
    }

    private void FixedUpdate()
    {
        if (moveDir.sqrMagnitude > 0.1f)
        {
            _animator.SetWalk(true);
        }
        else
        {
            _animator.SetRun(false);
            _animator.SetWalk(false);
        }
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

    public void OnDamaged(int damage)
    {
        _animator.SetHitTrigger();
        chEvent?.Invoke();
    }
}
