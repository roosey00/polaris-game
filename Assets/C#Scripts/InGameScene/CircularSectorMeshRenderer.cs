using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class CircularSectorMeshRenderer : MonoBehaviour
{
    public float degree = 180;
    public float intervalDegree = 5;
    public float beginOffsetDegree = 0;
    public float radius = 10;

    Mesh mesh;
    MeshFilter meshFilter;

    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;

    int i;
    float beginDegree;
    float endDegree;
    float beginRadian;
    float endRadian;
    float uvRadius = 0.5f;
    Vector2 uvCenter = new Vector2(0.5f, 0.5f);
    float currentIntervalDegree = 0;
    float limitDegree;
    int count;
    int lastCount;

    float beginCos;
    float beginSin;
    float endCos;
    float endSin;

    int beginNumber;
    int endNumber;
    int triangleNumber;

    // Use this for initialization 
    void Start()
    {
        mesh = new Mesh();
        meshFilter = (MeshFilter)GetComponent("MeshFilter");
    }

    // Update is called once per frame 
    void Update()
    {
        currentIntervalDegree = Mathf.Abs(intervalDegree);

        count = (int)(Mathf.Abs(degree) / currentIntervalDegree);
        if (degree % intervalDegree != 0)
        {
            ++count;
        }
        if (degree < 0)
        {
            currentIntervalDegree = -currentIntervalDegree;
        }

        if (lastCount != count)
        {
            mesh.Clear();
            vertices = new Vector3[count * 2 + 1];
            triangles = new int[count * 3];
            uvs = new Vector2[count * 2 + 1];
            vertices[0] = Vector3.zero;
            uvs[0] = uvCenter;
            lastCount = count;
        }

        i = 0;
        beginDegree = beginOffsetDegree;
        limitDegree = degree + beginOffsetDegree;

        while (i < count)
        {
            endDegree = beginDegree + currentIntervalDegree;

            if (degree > 0)
            {
                if (endDegree > limitDegree)
                {
                    endDegree = limitDegree;
                }
            }
            else
            {
                if (endDegree < limitDegree)
                {
                    endDegree = limitDegree;
                }
            }

            beginRadian = Mathf.Deg2Rad * beginDegree;
            endRadian = Mathf.Deg2Rad * endDegree;

            beginCos = Mathf.Cos(beginRadian);
            beginSin = Mathf.Sin(beginRadian);
            endCos = Mathf.Cos(endRadian);
            endSin = Mathf.Sin(endRadian);

            beginNumber = i * 2 + 1;
            endNumber = i * 2 + 2;
            triangleNumber = i * 3;

            vertices[beginNumber].x = beginCos * radius;
            vertices[beginNumber].y = beginSin * radius;
            vertices[beginNumber].z = 0;
            vertices[endNumber].x = endCos * radius;
            vertices[endNumber].y = endSin * radius;
            vertices[endNumber].z = 0;

            triangles[triangleNumber] = 0;
            if (degree > 0)
            {
                triangles[triangleNumber + 1] = endNumber;
                triangles[triangleNumber + 2] = beginNumber;
            }
            else
            {
                triangles[triangleNumber + 1] = beginNumber;
                triangles[triangleNumber + 2] = endNumber;
            }

            if (radius > 0)
            {
                uvs[beginNumber].x = beginCos * uvRadius + uvCenter.x;
                uvs[beginNumber].y = beginSin * uvRadius + uvCenter.y;
                uvs[endNumber].x = endCos * uvRadius + uvCenter.x;
                uvs[endNumber].y = endSin * uvRadius + uvCenter.y;
            }
            else
            {
                uvs[beginNumber].x = -beginCos * uvRadius + uvCenter.x;
                uvs[beginNumber].y = -beginSin * uvRadius + uvCenter.y;
                uvs[endNumber].x = -endCos * uvRadius + uvCenter.x;
                uvs[endNumber].y = -endSin * uvRadius + uvCenter.y;
            }

            beginDegree += currentIntervalDegree;
            ++i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter.sharedMesh = mesh;
        meshFilter.sharedMesh.name = "CircularSectorMesh";
    }
}