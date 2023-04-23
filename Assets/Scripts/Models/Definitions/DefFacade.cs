using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/DefFacade", fileName = "DefFacade")]
public class DefFacade : ScriptableObject
{
    [SerializeField] private PlayerDef _playerFacade;
    [SerializeField] private ItemsDef _itemsFacade;

    public PlayerDef Player => _playerFacade;
    public ItemsDef Items => _itemsFacade;

    private static DefFacade _instance;
    public static DefFacade I => _instance == null ? Load() : _instance;

    private static DefFacade Load()
    {
        return Resources.Load<DefFacade>("DefFacade");
    }
}
