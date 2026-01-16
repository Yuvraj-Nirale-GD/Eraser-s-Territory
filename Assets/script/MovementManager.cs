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
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 InputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return InputVector.normalized;
    }
}
    