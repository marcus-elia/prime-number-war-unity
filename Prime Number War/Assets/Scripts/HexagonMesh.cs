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
    private bool isPrime;
    private Color color;
    private TextMeshPro text;
    private Vector3 location;
    private float trackRadius;

    public static ScoreManager sm;

    // Store the 6 possible neighbors, counter clockwise (null where there is no neighbor)
    //         1
    //      2     0
    //      3     5
    //         4
    private GameObject[] neighbors;

    private bool isVisible;
    // A map containing angles to go to, and the corresponding angle to shoot the
    // missile towards
    private Dictionary<float, float> positionAngleToShootingAngle;

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
        isPrime = IsPrimeUnder100(number);
    }
    public void SetNumber(int n)
    {
        number = n;
        isPrime = IsPrimeUnder100(number);
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
    public void SetTrackRadius(float input)
    {
        trackRadius = input;
    }
    public void UpdatePositionAngleToShootingAngle()
    {
        Vector2 rayStart = transform.position;
        positionAngleToShootingAngle = new Dictionary<float, float>();
        if(neighbors[0] == null)
        {
            float angleToMoveTo = GetCircleIntersectionAngle(trackRadius, Mathf.PI/6, rayStart);
            positionAngleToShootingAngle[angleToMoveTo] = Mathf.PI / 6;
        }
        if(neighbors[1] == null)
        {
            float angleToMoveTo = GetCircleIntersectionAngle(trackRadius, Mathf.PI / 2, rayStart);
            positionAngleToShootingAngle[angleToMoveTo] = Mathf.PI / 2;
        }
        if(neighbors[2] == null)
        {
            float angleToMoveTo = GetCircleIntersectionAngle(trackRadius, 5*Mathf.PI/6, rayStart);
            positionAngleToShootingAngle[angleToMoveTo] = 5*Mathf.PI/6;
        }
        if(neighbors[3] == null)
        {
            float angleToMoveTo = GetCircleIntersectionAngle(trackRadius, 7*Mathf.PI/6, rayStart);
            positionAngleToShootingAngle[angleToMoveTo] = 7*Mathf.PI/6;
        }
        if(neighbors[4] == null)
        {
            float angleToMoveTo = GetCircleIntersectionAngle(trackRadius, 3*Mathf.PI/2, rayStart);
            positionAngleToShootingAngle[angleToMoveTo] = 3*Mathf.PI/2;
        }
        if(neighbors[5] == null)
        {
            float angleToMoveTo = GetCircleIntersectionAngle(trackRadius, 11*Mathf.PI/6, rayStart);
            positionAngleToShootingAngle[angleToMoveTo] = 11*Mathf.PI/6;
        }
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

    public void InitializeNeighbors()
    {
        neighbors = new GameObject[6];
    }

    public void AddNeighbor(GameObject obj)
    {
        float angle = Mathf.Atan2(obj.transform.position.y - transform.position.y, obj.transform.position.x - transform.position.x);
        if(Mathf.Abs(angle - Mathf.PI/6) < 0.1)
        {
            neighbors[0] = obj;
        }
        else if(Mathf.Abs(angle - Mathf.PI/2) < 0.1)
        {
            neighbors[1] = obj;
        }
        else if(Mathf.Abs(angle - 5*Mathf.PI/6) < 0.1)
        {
            neighbors[2] = obj;
        }
        else if(Mathf.Abs(angle - 7*Mathf.PI/6) < 0.1)
        {
            neighbors[3] = obj;
        }
        else if(Mathf.Abs(angle - 3*Mathf.PI/2) < 0.1)
        {
            neighbors[4] = obj;
        }
        else if(Mathf.Abs(angle - 11*Mathf.PI/6) < 0.1)
        {
            neighbors[5] = obj;
        }
        else
        {
            Debug.LogError("Invalid angle between a hexagon and its neighbor.");
        }
    }

    public void RemoveNeighbor(GameObject obj)
    {
        for(int i = 0; i < 6; i++)
        {
            if(neighbors[i] == obj)
            {
                neighbors[i] = null;
                return;
            }
        }
        Debug.LogError("Cannot remove a hexagon that is not a neighbor.");
    }

    // Adds this number to each of its neighbors
    public void AddToNeighbors()
    {
        for(int i = 0; i < 6; i++)
        {
            if (neighbors[i] == null)
            {
                continue;
            }
            neighbors[i].GetComponent<HexagonMesh>().Add(this.number);
        }
    }

    // Removes this node from all of its neighbors
    public void RemoveFromNeighbors()
    {
        for(int i = 0; i < 6; i++)
        {
            if (neighbors[i] == null)
            {
                continue;
            }
            neighbors[i].GetComponent<HexagonMesh>().RemoveNeighbor(this.gameObject);
        }
    }

    public void HitByMissile(MissileOwner owner)
    {
        if(isPrime)
        {
            if(owner == MissileOwner.Player)
            {
                sm.GivePlayerPoints(this.number);
            }
            else
            {
                sm.GiveComputerPoints(this.number);
            }
        }
        else
        {
            this.AddToNeighbors();
        }
        this.RemoveFromNeighbors();
        Destroy(gameObject);
        Destroy(text);
    }

    public void Add(int n)
    {
        number = (number + n) % 100;
        isPrime = IsPrimeUnder100(number);
        text.text = number.ToString();
    }

    public static bool IsPrimeUnder100(int n)
    {
        return (n == 2 || n == 3  || n == 5 || n == 7) || (n % 2 != 0) && (n % 3 != 0) && (n % 5 != 0) && (n % 7 != 0);
    }

    // Calculate the maximum value from shooting this and then a neighbor
    public int MaxTwoShotValue()
    {
        if(isPrime)
        {
            return number;
        }
        else
        {
            int max = 0;
            // Iterate through the neighbors and see if adding this to them makes a prime
            for(int i = 0; i < 6; i++)
            {
                if(neighbors[i] == null)
                {
                    continue;
                }
                int prev = neighbors[i].GetComponent<HexagonMesh>().GetNumber();
                int added = (prev + number) % 100;
                if(IsPrimeUnder100(added))
                {
                    if(added > max)
                    {
                        max = added;
                    }
                }
            }
            return max;
        }
    }


    // Assuming the track is a circle centered at the origin, find the angle the computer should move to
    // on the circle such as to shoot a missile at the input angle to hit the point at rayStart
    // Assume rayStart is inside the circle
    public static float GetCircleIntersectionAngle(float r, float missileAngle, Vector2 rayStart)
    {
        float x, y;

        // Special cases of vertical rays
        if(missileAngle == Mathf.PI/2)
        {
            x = rayStart.x;
            y = Mathf.Sqrt(r*r - x*x);
            return Mathf.Atan2(y, x);
        }
        if(missileAngle == 3*Mathf.PI/2)
        {
            x = rayStart.x;
            y = -Mathf.Sqrt(r*r - x*x);
            return Mathf.Atan2(y, x);
        }

        // Otherwise, make a line
        float m, b;
        m = Mathf.Tan(missileAngle);
        b = rayStart.y - m*rayStart.x;

        // Intersect circle and line
        float A = m*m + 1;
        float B = 2*b*m;
        float C = b*b - r*r;
        float disc = B * B - 4 * A * C;
        if(disc < 0)
        {
            Debug.LogError("Cannot solve quadratic with negative discriminant.");
        }
        // Get the x-coordinate in the correct quadrant
        if(missileAngle < Mathf.PI/2 || missileAngle > 3*Mathf.PI/2)
        {
            x = (-B + Mathf.Sqrt(disc)) / (2 * A);
        }
        else
        {
            x = (-B - Mathf.Sqrt(disc)) / (2 * A);
        }
        // Get the y-coordinate in the correct quadrant
        if(missileAngle < Mathf.PI)
        {
            y = Mathf.Sqrt(r*r - x*x);
        }
        else
        {
            y = -Mathf.Sqrt(r*r - x*x);
        }

        return Mathf.Atan2(y, x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
