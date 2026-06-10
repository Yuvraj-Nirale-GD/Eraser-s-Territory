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
            eraseTimer = eraseInterval;
        }
            DefuseCell();

    }
    void FixedUpdate()
    {
        Vector2 targetvelocity = eraserMovementVector * MoveSpeed;
        Vector2 nextPos = Eraserrb.position + targetvelocity * Time.fixedDeltaTime;
        var grid = CircuitGrid.CircuitGridInstance;
        if (grid != null)
        {
            Vector2Int nextCell = grid.WorldToCell(nextPos);
            if (grid.isValid(nextCell))
            {
                var cell = grid.GetCell(nextCell);
                if (cell != null && cell.hasTrap)
                {
                    return;
                }
            }
        }
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
        if (grid == null)
        {
            return;
        }
        int gridRadius = Mathf.CeilToInt(eraseRadius / grid.cellsize);
        
        Vector2Int Centercell = grid.WorldToCell(transform.position);
        if (! (PencilCapture > 0)) {
            return;
        }
        else{
            for (int x = -gridRadius; x <= gridRadius; x++)
            {
                for (int y = -gridRadius; y <= gridRadius; y++)
                {
                    Vector2Int cell = new Vector2Int(Centercell.x + x, Centercell.y + y);
                    if (!grid.isValid(cell)) continue;
                    if (!grid.HasCircuit(cell)) continue;
                    {
                        Vector2 cellWorldPos = grid.CellToWorld(cell);
                        if (Vector2.Distance(transform.position, cellWorldPos) <= eraseRadius)
                        {
                            grid.SetCircuit(cell, false);
                            Debug.Log("Defusing circuit at " + cell);
                        }

                    }
                }

            }
        }
    }
}
