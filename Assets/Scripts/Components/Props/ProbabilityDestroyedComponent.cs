using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteAnimation))]
[RequireComponent(typeof(EnterTriggerComponent))]
public class ProbabilityDestroyedComponent : MonoBehaviour
{
    [Range(0.0f, 1f)]
    [SerializeField] private float _destroyProbability;
    [SerializeField] private bool _forceDestroy = false;
    [Space]
    [SerializeField] private UnityEvent _onDestroy;

    private SpriteAnimation _animation;
    private EnterTriggerComponent _trigger;

    public bool ForceDestroy {get => _forceDestroy; set { _forceDestroy = value; } }
    private void Awake()
    {
        _animation = GetComponent<SpriteAnimation>();
        _trigger = GetComponent<EnterTriggerComponent>();
    }

    public void Check()
    {
        if (!enabled) return;
        var random = Random.Range(0f, 1f);

        if (random <= _destroyProbability || _forceDestroy)
        {
            enabled = false;
            _trigger.enabled = false;
            _animation.SetAnimationByName("destroy");

            _onDestroy?.Invoke();
        }
    }
}
