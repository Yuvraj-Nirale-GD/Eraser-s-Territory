using System.Collections.Generic;
using UnityEngine;


public class PencilTrail : MonoBehaviour
{


    [SerializeField] private LineRenderer linerender;
    [SerializeField] private float TrailPointDist = 0.3f;
    [SerializeField] private int MaxTrailPoints = 100;
    [SerializeField] private Rigidbody2D Pencilrb;
    TrailType currentTrailType = TrailType.Default;

    public void SetTrailType(TrailType TrailType)
    {
        currentTrailType = TrailType;
        switch (currentTrailType)
        {
            case TrailType.Default:
                linerender.startColor = Color.white;
                linerender.endColor = Color.white;
                break;
            case TrailType.Circuit:
                linerender.startColor = Color.cyan;
                linerender.endColor = Color.cyan;
                break;
            case TrailType.Trap:
                linerender.startColor = Color.red;
                linerender.endColor = Color.red;
                break;
        }
    }

    private List<TrailPoint> trailPoints = new List<TrailPoint>(); // creating a list of datatype TrailPoint(struct) to store trail points
    private Vector3 CurrentPosition;

    void Start()
    {

        linerender.positionCount = 0;// Initialize line renderer with 0 points

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
       
            //     trailPoints.Add(CurrentPosition);


            //     linerender.positionCount = trailPoints.Count;
            //     linerender.SetPositions(trailPoints.ToArray());
            
        if (trailPoints.Count == 0 || (trailPoints[^1].position - CurrentPosition).sqrMagnitude >= TrailPointDist 
        * TrailPointDist)
        { 
            trailPoints.Add(new TrailPoint
            {
                position = CurrentPosition,
                trailType = currentTrailType
            });
        }                       // adding new TrailPoint struct to the list

        if (trailPoints.Count > MaxTrailPoints)
        {
            trailPoints.RemoveAt(0);
        }

        linerender.positionCount = trailPoints.Count;
        linerender.SetPositions(trailPoints.ConvertAll(tp => tp.position).ToArray());// this line converts TrailPoint list to Vector3 array


    }
    public void EraseTrail(Vector3 ErasePos, float EraseRadius, bool canErase)
    {
        if (trailPoints.Count == 0)
        {
            Debug.Log("0 index");
            return;
        }
        float r2 = EraseRadius * EraseRadius;

        for (int i = trailPoints.Count - 1; i >= 0; i--)
        {

            if ((trailPoints[i].position - ErasePos).sqrMagnitude <= r2) // checking squared distance for performance and comparing with squared radius
            {

                if (trailPoints[i].trailType == TrailType.Circuit && !canErase) // cheking if the trail point is of type Circuit and if erasing is not allowed
                {
                    Debug.Log("Cannot erase Circuit trail");
                    continue;
                }
                trailPoints.RemoveAt(i);
                Debug.Log("Position of errased point: " + ErasePos);
                break;

            }
        }
        
        linerender.positionCount = trailPoints.Count;
        linerender.SetPositions(trailPoints.ConvertAll(p => p.position).ToArray());

    }

}
public enum TrailType
{
    Default,
    Circuit,
    Trap
}
[System.Serializable]
public struct TrailPoint
{
    public Vector3 position;
    public TrailType trailType;
}