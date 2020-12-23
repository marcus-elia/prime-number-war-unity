using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMesh : MonoBehaviour
{
    private float shapeRadius = 0.12f;
    private Color outerColor = Color.red;
    private Color innerColor = Color.white;

    private float speed = 0.05f;
    private float angle;
    private Vector3 velocity;

    private int smoothness = 24;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CreateShape()
    {
        Vector2[] vertices2D = new Vector2[smoothness+1];
        for (int i = 0; i < smoothness; i++)
        {
            vertices2D[i] = new Vector2(shapeRadius * Mathf.Cos(i * Mathf.PI * 2 / smoothness), shapeRadius * Mathf.Sin(i * Mathf.PI * 2 / smoothness));
        }
        vertices2D[smoothness] = new Vector2(0f, 0f);

        Vector3[] vertices3D = System.Array.ConvertAll<Vector2, Vector3>(vertices2D, v => v);

        int[] triangleIndices = new int[3* smoothness];

        for (int i = 0; i < smoothness - 1; i++)
        {
            triangleIndices[3 * i] = smoothness;
            triangleIndices[3 * i + 1] = i;
            triangleIndices[3 * i + 2] = i + 1;
        }
        triangleIndices[3 * smoothness - 3] = smoothness;
        triangleIndices[3 * smoothness - 2] = smoothness - 1;
        triangleIndices[3 * smoothness - 1] = 0;

        Color[] vertexColors = new Color[smoothness + 1];
        for (int i = 0; i < smoothness; i++)
        {
            vertexColors[i] = outerColor;
        }
        vertexColors[smoothness] = innerColor;

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

        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
        collider.radius = shapeRadius;
        collider.transform.position = transform.position;
    }

    public void SetAngle(float inputAngle)
    {
        angle = inputAngle;
    }

    public void SetLocation(Vector3 inputLocation)
    {
        transform.position = inputLocation;
    }

    public void CalculateVelocity()
    {
        velocity = new Vector3(Mathf.Cos(angle) * speed, Mathf.Sin(angle) * speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.GetComponent<HexagonMesh>().GetNumber().ToString());
    }

}
