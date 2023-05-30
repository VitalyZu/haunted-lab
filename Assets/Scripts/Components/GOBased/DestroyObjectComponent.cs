using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectComponent : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _destroyAfterSec = 0f;
    [SerializeField] private bool _onAwake;

    private void Start()
    {
        if (_onAwake) DestroySelf();
    }
    public void DestroySelf()
    {
        if (_gameObject != null)
        {
            Destroy(_gameObject, _destroyAfterSec);
        }

        Destroy(gameObject, _destroyAfterSec);
    }

    public void DestroyByParticle()
    {
        var parent = _gameObject.transform.parent;
        _gameObject.transform.parent = null;
        
        Destroy(_gameObject, _destroyAfterSec);
        if(parent != null) Destroy(parent.gameObject);
    }

    public void DestroyObject(GameObject target)
    {
        Destroy(target, _destroyAfterSec);
    }
}
