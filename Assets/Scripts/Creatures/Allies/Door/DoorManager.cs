using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private RequireItemComponent _requireItem;

    private static int _openKey = Animator.StringToHash("is-opened");

    private Animator _doorAnimator;

    private void Awake()
    {
        _doorAnimator = _door.GetComponent<Animator>();
    }

    public void OnInteract()
    {
        var state = _doorAnimator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("open"))
        {
            _requireItem.Check();
        }
        else 
        {
            Open();
        }
    }

    public void Open()
    {
        _doorAnimator.SetBool(_openKey, true);
    }
    public void Close()
    {
        _doorAnimator.SetBool(_openKey, false);
    }
}
