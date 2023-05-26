using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ShowEndLevelComponent : MonoBehaviour
{
    [SerializeField] private float _restartDelay;
    [SerializeField] private bool _startOnAwake;
    [SerializeField] private CinemachineVirtualCamera _targetCamera;
    [SerializeField] private bool _makeTransition;

    public static UnityAction<int> OnShow;

    private GameObject _target;
    private void Start()
    {
        _target = FindObjectOfType<MainTarget>().gameObject;

        if (_startOnAwake) Show(0);
    }
    public void Show(int type)
    {
        if (_makeTransition)
        {
            _targetCamera.Priority = 11;
        }
        StartCoroutine(MakeTransition(type));
    }

    private IEnumerator MakeTransition(int type)
    {
        if (_makeTransition)
        {
            while (_target.transform.position.x != _targetCamera.gameObject.transform.position.x)
            {
                yield return new WaitForSeconds(.5f);
            }
        }
        yield return new WaitForSeconds(_restartDelay);

        OnShow?.Invoke(type);

    }
}
