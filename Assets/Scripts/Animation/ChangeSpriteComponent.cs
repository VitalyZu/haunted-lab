using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeSpriteComponent : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private UnityEvent _OnChangeSpriteEvent;

    private int index = 0;
    private SpriteRenderer _renderer;

    public void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void SetNextSprite()
    {
        if (index < _sprites.Length)
        {
            if (_sprites[index] != null)
            {
                _renderer.sprite = _sprites[index];
                _OnChangeSpriteEvent?.Invoke();
            }
        }
        index++;
    }
}
