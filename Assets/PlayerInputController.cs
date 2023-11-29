using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputDriver : NetworkBehaviour
{
    private Rigidbody _characterController;
    private Vector2 _moveInput;
    private Vector3 _moveDirection;
    private bool _jump;
    [SerializeField] public float jumpSpeed = 6f;
    [SerializeField] public float speed = 8f;
    [SerializeField] public float gravity = -9.8f;
    private void Start()
    {
        _characterController = GetComponent(typeof(Rigidbody)) as Rigidbody;
        _jump = false;
    }
    public override void OnStartClient()
    {
        base.OnStartClient();

        if(!base.IsOwner)
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            //rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            GetComponentInChildren<Camera>().enabled = false;
        }
        
    }
    private void Update()
    {
        if (!base.IsOwner)
            return;

        //_moveDirection = InputStateStorage.instance.MovementInputDirection;
        _moveDirection = new Vector3(InputStateStorage.instance.MovementInputDirection.x, 0.0f, InputStateStorage.instance.MovementInputDirection.y); ;
        _moveDirection *= speed;

        /*
        _moveDirection = new Vector3(_moveInput.x, 0.0f, _moveInput.y);
        _moveDirection *= speed;
        */

        _characterController.AddForce(_moveDirection);
        /*
        if (_characterController.isGrounded)
        {
            _moveDirection = new Vector3(_moveInput.x, 0.0f, _moveInput.y);
            _moveDirection *= speed;

            if (_jump)
            {
                _moveDirection.y = jumpSpeed;
                _jump = false;
            }
        }
        _moveDirection.y += gravity * Time.deltaTime;
        _characterController.Move(_moveDirection * Time.deltaTime);
        */
    }
    #region UnityEventCallbacks
    public void OnMovement(InputAction.CallbackContext context)
    {
        Debug.Log(base.IsOwner);
        if (!base.IsOwner)
            return;
        _moveInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!base.IsOwner)
            return;

        if (context.started || context.performed)
        {
            _jump = true;
        }
        else if (context.canceled)
        {
            _jump = false;
        }
    }
    #endregion
}