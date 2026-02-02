using UnityEngine;
using System.Collections.Generic;
public class TrailSegment : MonoBehaviour
{
    public TrailType trailType;
    public LineRenderer lineRenderer;
    public List<Vector3> points = new List<Vector3>();

    public bool EraseSegment(Vector3 erasePos, float eraseRadius, bool canErase, out TrailSegment left, out TrailSegment right)
    {
        
        left = null;
        right = null;
        float r2 = eraseRadius * eraseRadius;



        if (trailType == TrailType.Circuit && !canErase)
        {
            return false; // Cannot erase Circuit trails without capture
        }
        int hitIndex = -1;
        for (int i = points.Count - 1; i >= 0; i--)
        {
            if (i > 0 && NearSegment(points[i - 1], points[i], erasePos, r2))

            {
                hitIndex = i;
                break;
            }
        }

        if (hitIndex == -1)
        {
            return false;
        }

        int leftCount = hitIndex;
        int rightCount = points.Count - hitIndex - 1; // number of points to the right of the hit point



        if (leftCount >= 1)
        {
            var LeftPoints = points.GetRange(0, leftCount); // getting the left segment points by excluding the hit point
            left = Instantiate(this, transform.parent); // create a new TrailSegment for the left part
            left.points = LeftPoints; // assign the left points
            left.Rebuild();
        }
        if (rightCount >= 1)
        {
            var RightPoints = points.GetRange(hitIndex + 1, rightCount);// getting the right segment points by excluding the hit point
            right = Instantiate(this, transform.parent);
            right.points = RightPoints;
            right.Rebuild();
        }
        Destroy(gameObject);
        return true;
    }
    bool NearSegment(Vector3 a, Vector3 b, Vector3 e, float r2)
    {
        Vector3 ab = b - a;
        Vector3 ae = e - a;
        float t = Vector3.Dot(ae, ab) / ab.sqrMagnitude;
        t = Mathf.Clamp01(t);
        Vector3 closestPoint = a + t * ab;
        return (closestPoint - e).sqrMagnitude <= r2;
    }
    public void Rebuild()
    {
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
