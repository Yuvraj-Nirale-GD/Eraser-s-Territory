using UnityEngine;

public class Eraser : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Eraserrb;
    [SerializeField] private float MoveSpeed = 3f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float eraseRadius = 0.5f;
    private int PencilCapture = 0;

    
    public Vector2 eraserMovementVector { get; private set; }
    private PencilTrail[] pencilTrail;

    
    void Start()
    {
        pencilTrail = FindObjectsOfType<PencilTrail>(); // Find all PencilTrail instances in the scene
    }
    // Update is called once per frame
    void Update()
    {
        eraserMovementVector = MovementManager.Instance.GetEraserMovement();
        EraseTrail();

    }
    void FixedUpdate()
    {
        Vector2 targetvelocity = eraserMovementVector * MoveSpeed;
        Eraserrb.linearVelocity = Vector2.MoveTowards(Eraserrb.linearVelocity,
        targetvelocity, 
        acceleration * Time.fixedDeltaTime);

    }
    void EraseTrail()
    {
        // if (PencilCapture == 0)
        // {
        //     Debug.Log(" You cannot erase the trail yet");
        //     return;
        // }

        foreach (PencilTrail pencilTrail in pencilTrail)
        {
            if ((pencilTrail.transform.position - transform.position).sqrMagnitude > eraseRadius * eraseRadius * 2f)
            {
                pencilTrail.EraseTrail(transform.position, eraseRadius, PencilCapture > 0);
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, eraseRadius);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pencil"))
        {
            PencilCapture++;
            Debug.Log("Pencil Captured! You can now erase trails.");
        }
    }
        
    
}
