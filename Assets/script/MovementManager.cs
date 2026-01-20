using UnityEngine;
using UnityEngine.InputSystem;

public class MovementManager : MonoBehaviour
{
    private InputSystem_Actions playerInputActions ;
    public static MovementManager Instance{get; private set;}
    
    private void Awake()
    {
        if ( Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    
        playerInputActions = new InputSystem_Actions();
        playerInputActions.Player.Enable();
        playerInputActions.Eraser.Enable();
    }
    public Vector2 GetPencilMovement()
    {
        Vector2 InputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return InputVector.normalized;
    }
    public Vector2 GetEraserMovement()
    {
        Vector2 InputVector = playerInputActions.Eraser.Move.ReadValue<Vector2>();
        return InputVector.normalized;
    }
    public void OnDisable()
    {
        playerInputActions.Player.Disable();
        playerInputActions.Eraser.Disable();
    }
}
    