using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class HeroInput : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    

    public void OnHorizontalMovement(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        Debug.Log(direction);
        _hero.SetDirection(direction);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _hero.Attack();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _hero.Interact();
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _hero.Throw();
        }
    }

    public void OnSpawn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _hero.SpawnGuard();
        }
    }
}
