using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeAnimatorComponent : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController[] _hitAnimatorControllers;
    [SerializeField] private UnityEvent _OnChangeAnimatorEvent;
    
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
            if (_hitAnimatorControllers[index] != null)
            {
                _animator.runtimeAnimatorController = _hitAnimatorControllers[index];
                _OnChangeAnimatorEvent?.Invoke();
            }
        }
        index++;
    }    
}
