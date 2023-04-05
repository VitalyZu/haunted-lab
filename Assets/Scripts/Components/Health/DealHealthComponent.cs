using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealHealthComponent : MonoBehaviour
{
    [SerializeField] private int _deltaHP;

    public void DealHealth(GameObject target)
    {
        var healthComponent = target.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.SetHealth(_deltaHP);
        }
    }
}
