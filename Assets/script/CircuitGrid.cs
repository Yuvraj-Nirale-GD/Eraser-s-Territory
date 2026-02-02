
using UnityEngine;

public class CircuitGrid : MonoBehaviour
{
    public static CircuitGrid CircuitGridInstance { get; private set; }
    [SerializeField] private int width = 100;
    [SerializeField] private int height = 100;
    [SerializeField] private float cellsize = 0.4f;
    [SerializeField] private bool showgrid = true;
    [SerializeField] private Vector2 gridorigin = Vector2.zero;

    private GridCell[,] cells;

    void Awake()
    {
        if (CircuitGridInstance == null)
        {
            CircuitGridInstance = this;

        }
        else
        {
            Destroy(gameObject);
        }
        BuildGrid();
    }
    void BuildGrid()
    {
        cells = new GridCell[width, height];

        gridorigin = new Vector2(
            -width * cellsize * 0.5f,
            -height * cellsize * 0.5f
        );

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                cells[x, y] = new GridCell();
            }
    }
    public Vector2Int WorldToCell(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt((worldPos.x - gridorigin.x) / cellsize);
        int y = Mathf.FloorToInt((worldPos.y - gridorigin.y) / cellsize);
        return new Vector2Int(x, y);
    }
    public bool isValid(Vector2Int c)
    {
        return c.x >= 0 && c.y >= 0 && c.x < width && c.y < height;
    }
    public void SetCircuit(Vector2Int c, bool value)
    {
        cells[c.x, c.y].hasCircuit = value;
    }
    public bool HasCircuit(Vector2Int c)
    {
        if (!isValid(c))
        {
            return false;
        }

        return cells[c.x, c.y].hasCircuit;
    }
    void OnDrawGizmos()
    {
        if (!showgrid || cells == null)
        {
            return;
        }


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 worldPos = new Vector3(
                    gridorigin.x + (x + 0.5f) * cellsize,
                    gridorigin.y + (y + 0.5f) * cellsize,
                    0f
                    );

                if (cells[x, y].hasCircuit)
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = new Color(1f, 1f, 1f, 0.1f);

                Gizmos.DrawWireCube(worldPos, Vector3.one * cellsize);
            }
        }
    }
}

