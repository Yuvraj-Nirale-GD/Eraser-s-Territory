using UnityEngine;

public class Eraser : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Eraserrb;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float acceleration = 12f;
    [SerializeField] private float eraseRadius = 0.5f;
    
    public Vector2 eraserMovementVector { get; private set; }
    private PencilTrail[] pencilTrail;   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pencilTrail = FindObjectsOfType<PencilTrail>();
    }

    // Update is called once per frame
    void Update()
    {
        eraserMovementVector = MovementManager.Instance.GetEraserMovement();
        EraseTrail();

    }
    void FixedUpdate()
    {
        Vector2 targetvelocity = eraserMovementVector * moveSpeed;
        Eraserrb.linearVelocity = Vector2.MoveTowards(Eraserrb.linearVelocity,
        targetvelocity, 
        acceleration * Time.fixedDeltaTime);

    }
    void EraseTrail()
    {
        foreach (PencilTrail pencilTrail in pencilTrail)
        {
            pencilTrail.EraseTrail(transform.position, eraseRadius);
        }
    }
}
