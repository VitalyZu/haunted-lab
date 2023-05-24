using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevelComponent : MonoBehaviour
{
    [SerializeField] private float _restartDelay;
    [SerializeField] private CinemachineVirtualCamera _targetCamera;
    [SerializeField] private bool _makeTransition;
    [SerializeField] private Scene _HUDScene;
    private GameObject _target;
    private void Start()
    {
        _target = FindObjectOfType<MainTarget>().gameObject;
    }
    public void RestartLevel()
    {
        if (_makeTransition)
        {
            _targetCamera.Priority = 11;
        }
        StartCoroutine(ReloadSession());
    }

    private IEnumerator ReloadSession()
    {
        if (_makeTransition)
        {
            while (_target.transform.position.x != _targetCamera.gameObject.transform.position.x)
            {
                yield return new WaitForSeconds(.5f);
            }
        }
        yield return new WaitForSeconds(_restartDelay);
        
        var hud = GameObject.Find("DestroyAnchor").gameObject;
        Destroy(hud);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}
