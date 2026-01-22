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
    public bool GetCurrentTrailType(out TrailType trailType)
    {
        if(playerInputActions.Player.SwitchCircuitTrail.triggered)
        {
            trailType = TrailType.Circuit;
            Debug.Log("Trail is Circuit Trail");
            return true;
        }
        else if (playerInputActions.Player.SwitchDefaultTrail.triggered)
        {
            trailType = TrailType.Default;
            Debug.Log("Trail is Default Trail");
            return true;
        }
         else if (playerInputActions.Player.SwitchTrapTrail.triggered)
        {
            trailType = TrailType.Trap;
            Debug.Log("Trail is chosen as Trap Trail");
            return true;
        }
        trailType = TrailType.Default;
        return false;
    }  
}