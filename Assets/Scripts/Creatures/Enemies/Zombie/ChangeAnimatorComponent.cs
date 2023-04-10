using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeAnimatorComponent : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController[] _hitAnimatorControllers;
    
    private int index = 0;
    private Animator _animator;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetNextAnimator()
    {        
        if (index < _hitAnimatorControllers.Length)
        {
            if(_hitAnimatorControllers[index] != null) _animator.runtimeAnimatorController = _hitAnimatorControllers[index];
        }
        index++;
    }
    
}
