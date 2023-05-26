using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] Text _score;
    private void Start()
    {
        _score.text = PlayerPrefs.GetInt("score").ToString();
    }

    [ContextMenu("Clear Stats")]
    public void Clear()
    {
        PlayerPrefs.SetInt("score", 0);
    }
}
