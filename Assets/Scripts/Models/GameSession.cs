using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    public PlayerData Data
    {
        get { return _playerData; }
        set { _playerData = value; }
    }
}
