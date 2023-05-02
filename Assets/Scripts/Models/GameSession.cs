using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    public PlayerData Data
    {
        get { return _playerData; }
        set { _playerData = value; }
    }

    private void Awake()
    {
        StartSession();
        InitModels();
    }

    private void StartSession()
    {
        LoadUI();
    }

    private void LoadUI()
    {
        SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
    }

    private void InitModels()
    {
        _playerData.HP.Value = DefFacade.I.Player.MaxHealth;
    }

    private void OnDestroy()
    {
    
    }
}
