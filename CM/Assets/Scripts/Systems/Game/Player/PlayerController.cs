using Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        inputActions.Player.Interact.performed -= OnInteract;
        inputActions.Disable();
    }

    private void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        Debug.Log("Click detectado");
    }
}