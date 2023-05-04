using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemElement : MonoBehaviour
{
    [SerializeField] private string _id;
    [SerializeField] private Text _value;

    public string Id => _id;

    public void SetValue(int value)
    {
        _value.text = value.ToString();
    }
}
