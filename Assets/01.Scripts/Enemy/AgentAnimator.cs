using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimator : MonoBehaviour
{
    private Animator _animator;
    public Animator Animator => _animator;

    private readonly int stackAttackAniHash = Animator.StringToHash("Stab Attack");
    private readonly int walkForwardAniHash = Animator.StringToHash("Walk Forward");
    private readonly int DiedAniHash = Animator.StringToHash("Die");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetStackAttack(bool value)
    {
        if (value) { _animator.SetTrigger(stackAttackAniHash); }
        else { _animator.ResetTrigger(stackAttackAniHash); }
    }

    public void SetWalkForward(bool value)
    {
        _animator.SetBool(walkForwardAniHash, value);
    }

    public void SetDie()
    {
        _animator.SetTrigger(DiedAniHash);
        
    }

    #region 애니메이션 Action

    public event Action OnAnimationEvenTrigger = null;
    public event Action OnAnimationEndTrigger = null;

    public void OnAnimationEvent()
    {
        OnAnimationEvenTrigger?.Invoke();
    }

    public void OnAnimationEnd()
    {
        OnAnimationEndTrigger?.Invoke();
    }

    #endregion
}
