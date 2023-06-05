using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpComponent : MonoBehaviour
{
    [SerializeField] private GameObject _powered;

    public void PowerUp(GameObject target)
    {
        var hero = target.GetComponent<Hero>();
        if (hero != null)
        {
            hero.PowerUp(_powered);
        }
    }
}
