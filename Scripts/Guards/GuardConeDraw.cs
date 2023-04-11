using System;
using System.Collections.Generic;
using UnityEngine;
struct RaycastInfo
{
    public bool Hit;
    public Vector3 Point;
    public float Distance;
    public float Angle;

    public RaycastInfo(bool hit, Vector3 point, float distance, float angle)
    {
        Hit = hit;
        Point = point;
        Distance = distance;
        Angle = angle;
    }
}
public class GuardConeDraw : MonoBehaviour
{
    [SerializeField] private Transform drawingOrigin;
    [SerializeField] private float fovAngle;
    [SerializeField] private int meshRes;
    [SerializeField] private MeshFilter viewMeshFilter;
    private Mesh _viewMesh;
    [SerializeField] private float spottingDistance;
    [SerializeField] private LayerMask visionLayerMask;

    private void Awake()
    {
        
        _viewMesh = new Mesh();
        viewMeshFilter.mesh = _viewMesh;
    }

    private void LateUpdate()
    {
        DrawFOV();
    }

    private void DrawFOV()
    {
        int stepCount = Mathf.RoundToInt(meshRes * fovAngle);
        float stepAngle = fovAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = drawingOrigin.eulerAngles.y - fovAngle / 2 + stepAngle * i;
            RaycastInfo raycastInfo = GetRaycast(angle);
            viewPoints.Add(raycastInfo.Point);
        }

        int vertCount = viewPoints.Count + 1;
        Vector3[] verts = new Vector3[vertCount];
        int[] tris = new int[(vertCount - 2) * 3];
        
        verts[0] = Vector3.zero;
        for (int i = 0; i < vertCount - 1; i++)
        {
            verts[i + 1] = drawingOrigin.InverseTransformPoint(viewPoints[i]);
            if (i < vertCount - 2)
            {
                tris[3 * i] = 0;
                tris[3 * i + 1] = i + 1;
                tris[3 * i + 2] = i + 2;
            }
        }
        _viewMesh.Clear();
        _viewMesh.vertices = verts;
        _viewMesh.triangles = tris;
        _viewMesh.RecalculateNormals();
    }
    private RaycastInfo GetRaycast(float angle)
    {
        Vector3 direction = DirFromAngle(angle, true);
        RaycastHit hit;
        if (Physics.Raycast(drawingOrigin.position, direction, out hit, spottingDistance, visionLayerMask))
        {
            //Debug.DrawRay(drawingOrigin.position, direction * hit.distance, Color.blue);
            return new RaycastInfo(true, hit.point, hit.distance, angle);
        }
        else
        {
            //Debug.DrawRay(drawingOrigin.position, direction * spottingDistance, Color.yellow);
            return new RaycastInfo(false, drawingOrigin.position + direction * spottingDistance, spottingDistance, angle);
        }
    } 
    private Vector3 DirFromAngle(float angle, bool isGlobal)
    {
        if (!isGlobal)
        {
            angle += drawingOrigin.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0 , Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}