using UnityEngine;
public class TrapSystem : MonoBehaviour
{
    public static TrapSystem instance {get; private set;}
    void Awake()
    {
        instance = this;
    
    }
    void Update()
    {
        var grid = CircuitGrid.CircuitGridInstance;
        if(grid == null) return;

        foreach(var cell in grid.AllCells())
        {
            if(!cell.hasTrap) continue;

            cell.trapTimer -= Time.deltaTime;
            if(cell.trapTimer <= 0f)
            {
                cell.hasTrap = false;
            }
        }
    }
    public void PlaceTrap(Vector2Int cell, float duration)
    {
        var grid = CircuitGrid.CircuitGridInstance;
        if (!grid.isValid(cell))
        {
            return;
        }

        var c = grid.GetCell(cell);
        c.hasTrap = true;
        c.trapTimer = duration;

    }
}