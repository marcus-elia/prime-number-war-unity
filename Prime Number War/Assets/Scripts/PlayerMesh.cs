using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMesh : MonoBehaviour
{
    private float radius = 1f;

    private Color color = Color.black;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CreateShape()
    {
        Vector2[] vertices2D = new Vector2[13];
        for(int i = 0; i < 12; i++)
        {
            vertices2D[i] = new Vector2(radius * Mathf.Cos(i * Mathf.PI * 2 / 12), radius * Mathf.Sin(i * Mathf.PI * 2 / 12));
        }
        vertices2D[12] = new Vector2(0f, 0f);

        Vector3[] vertices3D = System.Array.ConvertAll<Vector2, Vector3>(vertices2D, v => v);

        int[] triangleIndices = new int[36];
        
        for(int i = 0; i < 11; i++)
        {
            triangleIndices[3 * i] = 12;
            triangleIndices[3 * i + 1] = i;
            triangleIndices[3 * i + 2] = i + 1;
        }
        triangleIndices[33] = 12;
        triangleIndices[34] = 11;
        triangleIndices[35] = 0;

        Color[] vertexColors = new Color[13];
        for(int i = 0; i < 13; i++)
        {
            vertexColors[i] = color;
        }

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


    public void SetLocation(Vector3 inputLocation)
    {
        transform.position = inputLocation;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
