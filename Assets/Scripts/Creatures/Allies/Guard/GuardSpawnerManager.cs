using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;
    private GameObject _guard;
    private SpawnComponent _spawner;
    private RequireItemComponent _require;
    private SpriteAnimation _animation;

    private void Awake()
    {
        _spawner = GetComponent<SpawnComponent>();
        _require = GetComponent<RequireItemComponent>();
        _animation = GetComponent<SpriteAnimation>();
    }

    public void OnInteract()
    {
        if (_guard == null)
        {
            _require.Check();
            
        }
    }

    public void OnOpen()
    {
        _animation.SetAnimationByName("open"); 
    }

    public void OnCall()
    {
        var index = Random.Range(0, _prefabs.Length);
        _guard = SpawnUtil.Spawn(_prefabs[index], transform.position);
    }
}
