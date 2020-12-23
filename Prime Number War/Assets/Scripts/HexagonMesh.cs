// The mesh code is based on https://medium.com/@hyperparticle/draw-2d-physics-shapes-in-unity3d-2e0ec634381c

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HexagonMesh : MonoBehaviour
{
    private static float bigSideLength;
    public static float sideLengthRatio = 0.9f;
    private float sideLength;

    GameObject textHolder;

    private int number;
    private Color color;
    private TextMeshPro text;
    private Vector3 location;

    public static Color low = new Color(0.15f, 0.58f, 0.49f);
    public static Color medium = new Color(0.42f, 0.42f, 0.42f);
    public static Color high = new Color(0.92f, 0.75f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
    }

    public void GenerateRandomNumber()
    {
        number = (int)Random.Range(0, 100);
    }
    public void SetSideLength(float inputBigSideLength)
    {
        bigSideLength = inputBigSideLength;
        sideLength = sideLengthRatio * bigSideLength;
    }
    public void SetLocation(float x, float y)
    {
        location = new Vector3(x, y, 0f);
        transform.position = location;
    }
    public void SetColor()
    {
        if(number < 33)
        {
            color = low;
        }
        else if(number < 67)
        {
            color = medium;
        }
        else
        {
            color = high;
        }
    }

    public void CreateHexagon()
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
            color,
            color,
            color,
            color,
            color,
            color
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

        PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
        collider.transform.position = transform.position;
        //collider.radius = 2f;
        collider.isTrigger = true;
    }

    public void CreateText()
    {
        textHolder = new GameObject();
        textHolder.transform.position = transform.position;
        text = textHolder.AddComponent<TextMeshPro>();
        text.text = number.ToString();
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.white;
        text.fontSize = 6;
    }

    public int GetNumber()
    {
        return number;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
