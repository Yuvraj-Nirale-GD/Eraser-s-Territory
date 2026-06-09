using UnityEngine;

public class GridCell : MonoBehaviour
{
    public bool hasCircuit;
    public bool hasTrap;
    public float trapTimer;
    public Vector2 GridPosition => new(transform.position.x, transform.position.y);

}