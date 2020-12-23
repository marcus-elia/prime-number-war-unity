using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMesh : MonoBehaviour
{
    private float shapeRadius = 1f;

    private Color color = Color.white;

    private float trackRadius;

    private float maxAngularSpeed     = 0.05f;
    private float angularAcceleration = 0.001f;
    private float curAngularVelocity = 0f;
    private float curAngle = Mathf.PI;
    private float friction = 0.9f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CreateShape()
    {
        Vector2[] vertices2D = new Vector2[13];
        for(int i = 0; i < 12; i++)
        {
            vertices2D[i] = new Vector2(shapeRadius * Mathf.Cos(i * Mathf.PI * 2 / 12), shapeRadius * Mathf.Sin(i * Mathf.PI * 2 / 12));
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

    public void SetTrackRadius(float inputTrackRadius)
    {
        trackRadius = inputTrackRadius;
    }

    // Update is called once per frame
    void Update()
    {
        curAngle += curAngularVelocity;
        transform.position = new Vector3(trackRadius * Mathf.Cos(curAngle), trackRadius * Mathf.Sin(curAngle), 0);

        if(curAngle < 0)
        {
            curAngle += 2 * Mathf.PI;
        }
        else if(curAngle > 2*Mathf.PI)
        {
            curAngle -= 2 * Mathf.PI;
        }

        bool cw = Input.GetKey(KeyCode.A);
        bool ccw = Input.GetKey(KeyCode.D);

        if(cw && !ccw)
        {
            curAngularVelocity -= angularAcceleration;
        }
        else if(ccw && !cw)
        {
            curAngularVelocity += angularAcceleration;
        }
        else if(!cw && !ccw)
        {
            curAngularVelocity *= friction;
        }

        if(curAngularVelocity > maxAngularSpeed)
        {
            curAngularVelocity = maxAngularSpeed;
        }
        else if(curAngularVelocity < -maxAngularSpeed)
        {
            curAngularVelocity = -maxAngularSpeed;
        }
    }
}
