﻿// This code is based on https://medium.com/@hyperparticle/draw-2d-physics-shapes-in-unity3d-2e0ec634381c

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonMesh : MonoBehaviour
{
    public static float sideLength = 0.9f;

    // Start is called before the first frame update
    void Start()
    {
        createHexagon();
    }

    private void createHexagon()
    {
        float sqrt3 = Mathf.Sqrt(3);
        Vector2[] vertices2D = new Vector2[]
        {
            new Vector2(sideLength/2, -sideLength*sqrt3/2),
            new Vector2(sideLength, 0),
            new Vector2(sideLength/2, sideLength*sqrt3/2),
            new Vector2(-sideLength/2, sideLength*sqrt3/2),
            new Vector2(-sideLength, 0),
            new Vector2(-sideLength/2, -sideLength*sqrt3/2)
        };

        Vector3[] vertices3D = System.Array.ConvertAll<Vector2, Vector3>(vertices2D, v => v);

        // Four triangles make a hexagon
        int[] triangleIndices = new int[]
        {
            0, 1, 2,
            0, 2, 5,
            2, 3, 5,
            3, 4, 5
        };

        Color[] vertexColors = new Color[]
        {
            Color.cyan,
            Color.cyan,
            Color.cyan,
            Color.cyan,
            Color.cyan,
            Color.cyan
        };

        Mesh mesh = new Mesh
        {
            vertices = vertices3D,
            triangles = triangleIndices,
            colors = vertexColors
        };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // Set up game object with mesh;
        var meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));

        var filter = gameObject.AddComponent<MeshFilter>();
        filter.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}