using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    private CharacterController _controller;

    [Header("플레이어 움직임 관련 변수")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity;
    [SerializeField] private float _gravityCalculator;
    [SerializeField] private float _rotateAmount;

    private bool _isDash = false;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashCoolTime;

    public Action OnAttackTrigger = null;

    private Vector3 _inputVec;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MainAttackInput();
        DashMove();
        if (!_isDash)
        {
            MainMovement(InputDirection(_inputVec));
        }
    }

    private Vector3 InputDirection(Vector3 inputVec)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        inputVec =  new Vector3(h, 0,v);
        if (inputVec.sqrMagnitude > 0)
            return inputVec;
        else
            return Vector3.zero;
    }

    private void GravityCalc()
    {
        _gravity -= _gravityCalculator * Time.deltaTime;
        if (_controller.isGrounded) _gravity = 0;
    }

    //private void LookRotationToQuater(ref Vector3 moveDir)
    //{
    //    moveDir = Quaternion.Euler(0,_rotateAmount,0) * moveDir;
    //    if(moveDir.sqrMagnitude > 0)
    //    {
    //        transform.rotation = Quaternion.LookRotation(moveDir);
    //    }
    //}

    private void MainMovement(Vector3 moveDir)
    {
        GravityCalc();
        //LookRotationToQuater(ref moveDir);
        Vector3 move = transform.forward * moveDir.z + transform.right * moveDir.x + _gravity * Vector3.up;
        _controller.Move(move * _moveSpeed * Time.deltaTime);
    }
    private void DashMove()
    {
        if(_controller.isGrounded == true && _dashCoolTime <= 0 &&(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            _isDash = true;
            _controller.Move(_inputVec.normalized * _dashSpeed * Time.deltaTime);
        }
        _isDash = false;
    }

    private void MainAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnAttackTrigger?.Invoke();
        }
    }
}
