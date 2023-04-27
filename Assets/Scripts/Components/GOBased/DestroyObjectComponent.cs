using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectComponent : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _destroyAfterSec = 0f;
    public void DestroySelf()
    {
        if (_gameObject != null)
        {
            Destroy(_gameObject);
        }

        Destroy(gameObject);
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
        Destroy(target);
    }
}
