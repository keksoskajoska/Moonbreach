using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputStateStorage : MonoBehaviour
{
    public Vector2 MovementInputDirection = Vector2.zero;
    [SerializeField] private PlayerInput input;
    public static InputStateStorage instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple Input Storages are not allowed to exist!");
            Destroy(this.gameObject);
        }
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        if ((context.canceled))
        {
            MovementInputDirection = Vector2.zero;
        }
        else if(context.started || context.performed)
        {
            MovementInputDirection = context.ReadValue<Vector2>();
        }
    }
}
