using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Transform _levelComplete;
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite[] _levelCompleteIcons;

    private Hero _hero;

    private void Start()
    {
        _hero = FindObjectOfType<Hero>();
        ShowEndLevelComponent.OnShow += OnLevelComplete;
    }

    public void OnLevelComplete(int type)
    {
        if (_hero != null)
        {
            _hero.SetDirection(Vector2.zero);
            _hero.GetComponent<PlayerInput>().enabled = false;
        }

        _levelComplete.gameObject.SetActive(true);
        _icon.sprite = _levelCompleteIcons[type];
    }

    private void OnDestroy()
    {
        ShowEndLevelComponent.OnShow -= OnLevelComplete;
    }
}
