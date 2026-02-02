using UnityEngine;

public class PencilMovement : MonoBehaviour
{
    public TrailType CurrentType = TrailType.Default;
    [SerializeField] private Rigidbody2D Pencilrb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private PencilTrail pencilTrail;


    public Vector2 Movementvector { get; private set; }

    void Update()
    {

        Movementvector = MovementManager.Instance.GetPencilMovement(); // Get input from MovementManager 
        if (MovementManager.Instance.GetCurrentTrailType(out TrailType Trail))
        {
            CurrentType = Trail;
            pencilTrail.SetTrailType(CurrentType);
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 targetvelocity = Movementvector * moveSpeed; // Calculate target velocity 

        Pencilrb.linearVelocity = Vector2.MoveTowards(Pencilrb.linearVelocity,
        targetvelocity,
        acceleration * Time.fixedDeltaTime);// Smoothly adjust velocity, adding acceleration

    }
    // void PaintCircuit(Vector3 worldPos)
    // {
    //     Vector2Int cell = CircuitGrid.CircuitGridInstance.WorldToCell(worldPos);
    //     if (!CircuitGrid.CircuitGridInstance.isValid(cell))
    //         return;

    //     CircuitGrid.CircuitGridInstance.SetCircuit(cell, true);
    // }
}
