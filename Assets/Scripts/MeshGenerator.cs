using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    private Mesh mesh;
    
    private Vector3[] vertices;
    private int[] triangles;

    public int xSize = 100;
    public int zSize = 100;
    
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        
        UpdateMesh();
    }
    

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        mesh.RecalculateNormals();
    }

    private void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                //float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2;
                float y = Mathf.PerlinNoise(x * .99f, z *.99f);
                y += Mathf.PerlinNoise(x * 0.1f, z * 0.1f) * 10;
                y += Mathf.PerlinNoise(x * 0.01f, z * 0.01f) * 100;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];
        
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
        
                triangles[tris +0] = vert + 0;
                triangles[tris +1] = vert + xSize +1;
                triangles[tris +2] = vert + 1;
                triangles[tris +3] = vert + 1;
                triangles[tris +4] = vert + xSize + 1;
                triangles[tris +5] = vert + xSize + 2;
                
                vert++;
                tris += 6;
                
                
            }

            vert++;
        }
        
        

    }


    // private void OnDrawGizmos()
    // {
    //
    //     if (vertices == null)
    //     {
    //         return;
    //     }
    //     for (int i = 0; i < vertices.Length; i++)
    //     {
    //         Gizmos.DrawSphere(vertices[i], .1f);
    //     }
    // }
}
