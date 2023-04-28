using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableComponent : MonoBehaviour
{
    [SerializeField] UnityEvent _action;

    private void Start()
    {}

    public void Interact()
    {
        _action?.Invoke();
    }
}
