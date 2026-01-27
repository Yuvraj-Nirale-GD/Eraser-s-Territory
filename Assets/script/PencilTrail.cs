using System;
using System.Collections.Generic;
using UnityEngine;


public class PencilTrail : MonoBehaviour
{



    [SerializeField] private float TrailPointDist = 0.3f;
    [SerializeField] private int MaxTrailPoints = 100;
    [SerializeField] private Rigidbody2D Pencilrb;
    TrailType currentTrailType = TrailType.Default;

    [SerializeField] private TrailSegment trailSegmentPrefab;

    private List<TrailSegment> Segments = new List<TrailSegment>(); // creating a list of datatype TrailSegment to store trail segments
    private TrailSegment currentSegment;
    private Vector3 CurrentPosition;


    void OnEnable()
    {
        TrailRegister.TrailRegisterInstance.Register(this);
    }
    void OnDisable()
    {
        if (TrailRegister.TrailRegisterInstance != null)
        {
            TrailRegister.TrailRegisterInstance.Unregister(this);
        }
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

        if (currentSegment == null)
        {
            NewSegment(currentTrailType);
        }


        if (currentSegment.points.Count == 0 || (currentSegment.points[^1] - CurrentPosition).sqrMagnitude >= TrailPointDist
        * TrailPointDist)
        {


            currentSegment.points.Add(CurrentPosition);

            if (currentSegment.points.Count > MaxTrailPoints)
            {
                currentSegment.points.RemoveAt(0);
            }

            currentSegment.lineRenderer.positionCount = currentSegment.points.Count;
            currentSegment.lineRenderer.SetPositions(currentSegment.points.ToArray());
        }

    }
    public void EraseTrail(Vector3 ErasePos, float EraseRadius, bool canErase)
    {
        // if (currentSegment == null || currentSegment.points.Count == 0)
        // {
        //     return;
        // }
        for (int i = Segments.Count - 1; i >= 0; i--)
        {
            var segment = Segments[i];
            {
                if (segment.EraseSegment(ErasePos, EraseRadius, canErase, out TrailSegment left, out TrailSegment right))
                {
                    Segments.RemoveAt(i);
                    if (left != null)
                    {
                        Segments.Add(left);
                    }
                    if (right != null)
                    {
                        Segments.Add(right);
                    }
            
                
                    break;
                }
            }
        }
    }
    public void SetTrailType(TrailType TrailType)
    {
        if (currentSegment != null && currentSegment.trailType == TrailType)
        {
            return;
        }
        currentTrailType = TrailType;
        NewSegment(TrailType);

    }
    void NewSegment(TrailType trailType)
    {
        currentSegment = Instantiate(trailSegmentPrefab, transform);
        currentSegment.trailType = trailType;

        Color color = trailType switch
        {
            TrailType.Default => Color.white,
            TrailType.Circuit => Color.cyan,
            TrailType.Trap => Color.red,
            _ => Color.white
        };
        currentSegment.lineRenderer.startColor = color;
        currentSegment.lineRenderer.endColor = color;
        Segments.Add(currentSegment);
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