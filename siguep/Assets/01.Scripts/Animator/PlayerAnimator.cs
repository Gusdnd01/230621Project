using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private readonly int _runHash   = Animator.StringToHash("Run");
    private readonly int _walkHash  = Animator.StringToHash("Walk");
    private readonly int _onHitHash = Animator.StringToHash("Hit");
    private readonly int _dieHash   = Animator.StringToHash("Die");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetRun(bool value)
    {
        _animator.SetBool(_runHash, value);
    }

    public void SetDie(bool value)
    {
        _animator.SetBool(_dieHash,value);
    }

    public void SetWalk(bool value)
    {
        _animator.SetBool(_walkHash, value);
    }

    public void SetHitTrigger()
    {
        _animator.SetTrigger(_onHitHash);
    }
}                    
