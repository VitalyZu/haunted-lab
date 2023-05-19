using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private RequireItemComponent _requireItem;

    private static int _openKey = Animator.StringToHash("is-opened");
    private AudioSource _doorAudio;
    private Animator _doorAnimator;
    private bool _isActive = true;

    private void Awake()
    {
        _doorAnimator = _door.GetComponent<Animator>();
        _doorAudio = _door.GetComponent<AudioSource>();
    }

    public void OnInteract()
    {
        //todo
        if (_door == null) return;
        if (_doorAudio != null && _doorAudio.enabled) return;
        if (!_isActive) return;
        var state = _doorAnimator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("open"))
        {
            _isActive = false;
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

    public void SetActive(bool state)
    {
        _isActive = state;
    }
}
