using System.Collections.Generic;
using UnityEngine;


public class PencilTrail : MonoBehaviour
{

    [SerializeField] private LineRenderer linerender;
    [SerializeField] private float TrailPointDist = 0.5f;
    [SerializeField] private int MaxTrailPoints = 100;
    [SerializeField]private Rigidbody2D Pencilrb;
    

    
    private List<Vector3> trailPoints = new List<Vector3>();
    private Vector2 CurrentPosition;

    void Start()
    {
        
        linerender.positionCount = 0;

    }
    void Update()
    {
        AddPoint();
    }   
    void AddPoint()
    {
        if (Pencilrb.linearVelocity.magnitude < 0.1f)
        {
            return;
        }
        CurrentPosition = transform.position;
        if ( trailPoints.Count == 0 || Vector3.Distance(trailPoints[^1], CurrentPosition) >= TrailPointDist)
        {
            trailPoints.Add(CurrentPosition);
            if(trailPoints.Count > MaxTrailPoints)
            {
                trailPoints.RemoveAt(0);
            }

            linerender.positionCount = trailPoints.Count;
            linerender.SetPositions(trailPoints.ToArray());
        }
        
    }
    public void EraseTrail(Vector3 ErasePos, float EraseRadius)
    {
        if (trailPoints.Count == 0)
            {
                Debug.Log("0 index");
                return;
            }
            
        for (int i = trailPoints.Count -1; i>=0; i--)
        {
            
            if (Vector3.Distance(trailPoints[i], ErasePos)<= EraseRadius)
            {
                trailPoints.RemoveRange(i, trailPoints.Count - i );
                break;
            
            }
        }

            linerender.positionCount = trailPoints.Count;
            linerender.SetPositions(trailPoints.ToArray());
        
    }
        
}