using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TrailSystem : MonoBehaviour
{

    [SerializeField] private LineRenderer linerender;
    [SerializeField] private float TrailPointDist = 0.5f;
    [SerializeField] private int MaxTrailPoints = 100;

    
    private List<Vector2> trailPoints = new List<Vector2>();
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
        CurrentPosition = transform.position;
        if ( trailPoints.Count == 0 || Vector2.Distance(trailPoints[^1], CurrentPosition) >= TrailPointDist)
        {
            trailPoints.Add(CurrentPosition);
            if(trailPoints.Count > MaxTrailPoints)
            {
                trailPoints.RemoveAt(0);
            }

            linerender.positionCount = trailPoints.Count;
            linerender.SetPositions(trailPoints.ConvertAll(point => (Vector3)point).ToArray());// coverts all points in trailpoint to vector3 array
            
        }
        
       
    }

        
}