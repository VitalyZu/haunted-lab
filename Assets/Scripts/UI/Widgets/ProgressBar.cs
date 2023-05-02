using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void SetBar(float value)
    {
        _image.fillAmount = value;
    }
}
