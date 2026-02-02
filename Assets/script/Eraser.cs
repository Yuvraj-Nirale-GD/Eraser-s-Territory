using UnityEngine;

public class Eraser : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Eraserrb;
    [SerializeField] private float MoveSpeed = 3f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float eraseRadius = 0.5f;
    [SerializeField] private float eraseInterval = 0.2f;
    float eraseTimer = 0f;
    private int PencilCapture = 0;


    public Vector2 eraserMovementVector { get; private set; }



    void Update()
    {
        eraserMovementVector = MovementManager.Instance.GetEraserMovement();
        eraseTimer -= Time.deltaTime;
        if (eraseTimer <= 0f && Eraserrb.linearVelocity.sqrMagnitude > 0.01f)
        {
            EraseTrail();
            DefuseCell();
            eraseTimer = eraseInterval;
        }

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
        foreach (var trail in TrailRegister.TrailRegisterInstance.Trails)
        {
            trail.EraseTrail(transform.position, eraseRadius, PencilCapture > 0);
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
            Debug.Log("Pencil Captured! You can now erase circuit trails.");
        }
    }

    void DefuseCell()
    {
        var grid = CircuitGrid.CircuitGridInstance;
        if ( grid == null)
        {
            return;
        }

        Vector2Int cell = grid.WorldToCell(transform.position);
        
        if ( grid.isValid(cell))
        {
           if(grid.HasCircuit(cell)) 
           {
            grid.SetCircuit(cell, false);
           Debug.Log("Defusing circuit at " + cell);
           }
           else
            {
                Debug.Log("No circuit at " + cell);
            }

        }
    
    }
}
