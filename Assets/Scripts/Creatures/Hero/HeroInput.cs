using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInput : MonoBehaviour
{
    [SerializeField] private Hero _hero;

    public void OnHorizontalMovement(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        _hero.SetDirection(direction);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _hero.Attack();
        }
    }
}
