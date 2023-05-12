using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowCostComponent : MonoBehaviour
{
    [SerializeField] private int _cost;
    [SerializeField] private TextMeshPro _value;

    private void Awake()
    {
        _value.text = _cost.ToString();
    }
}
