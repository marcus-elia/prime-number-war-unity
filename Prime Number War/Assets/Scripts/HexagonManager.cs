using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonManager : MonoBehaviour
{
    public GameObject hexagonPrefab;

    public GameObject scoreManager;

    private List<GameObject> hexagons;
    public static float trackRadius = 5f;

    public static float sideLength = 0.5f;
    public static int boardSize = 5; // There are 2*boardSize - 1 columns

    // Start is called before the first frame update
    void Start()
    {
        GenerateHexagons();
        CalculateHexagonNeighbors();
        CalculateHexagonShootingAngles();
    }

    private void GenerateHexagons()
    {
        GameObject h;
        hexagons = new List<GameObject>();

        HexagonMesh.sm = scoreManager.GetComponent<ScoreManager>();

        float sqrt3 = Mathf.Sqrt(3);

        // Left half and center column
        for(int i = 1; i < boardSize + 1; i++)
        {
            float x = -3 * sideLength / 2 * (boardSize + 1 - i) + 1;
            float y = -(boardSize + i - 3) * sideLength * sqrt3 / 2 - sideLength * sqrt3/2;
            for(int j = 1; j < boardSize + i; j++)
            {
                h = Instantiate(hexagonPrefab);
                h.GetComponent<HexagonMesh>().GenerateRandomNumber();
                h.GetComponent<HexagonMesh>().SetSideLength(sideLength);
                h.GetComponent<HexagonMesh>().SetTrackRadius(trackRadius);
                h.GetComponent<HexagonMesh>().SetLocation(x, y);
                h.GetComponent<HexagonMesh>().SetColor();
                h.GetComponent<HexagonMesh>().CreateHexagon();
                h.GetComponent<HexagonMesh>().CreateText();
                h.GetComponent<HexagonMesh>().InitializeNeighbors();

                hexagons.Add(h);

                y += sideLength * sqrt3;
            }
        }

        // Right half
        for(int i = 1; i < boardSize; i++)
        {
            float x = 3 * sideLength / 2 * (boardSize - 1 - i) + 1;
            float y = -(boardSize + i - 3) * sideLength * sqrt3 / 2 - sideLength * sqrt3/2;
            for(int j = 1; j < boardSize + i; j++)
            {
                h = Instantiate(hexagonPrefab);
                h.GetComponent<HexagonMesh>().GenerateRandomNumber();
                h.GetComponent<HexagonMesh>().SetSideLength(sideLength);
                h.GetComponent<HexagonMesh>().SetTrackRadius(trackRadius);
                h.GetComponent<HexagonMesh>().SetLocation(x, y);
                h.GetComponent<HexagonMesh>().SetColor();
                h.GetComponent<HexagonMesh>().CreateHexagon();
                h.GetComponent<HexagonMesh>().CreateText();
                h.GetComponent<HexagonMesh>().InitializeNeighbors();

                hexagons.Add(h);

                y += sideLength * sqrt3;
            }
        }
    }

    private void CalculateHexagonNeighbors()
    {
        // If distance between centers of hexagons is less than this, they are neighbors
        float neighborDistance = sideLength * Mathf.Sqrt(3) + 0.1f;

        for(int i = 0; i < hexagons.Count - 1; i++)
        {
            GameObject h1 = hexagons[i];
            for(int j = i + 1; j < hexagons.Count; j++)
            {
                GameObject h2 = hexagons[j];
                if(Vector3.Distance(h1.transform.position, h2.transform.position) < neighborDistance)
                {
                    h1.GetComponent<HexagonMesh>().AddNeighbor(h2);
                    h2.GetComponent<HexagonMesh>().AddNeighbor(h1);
                }
            }
        }
    }

    private void CalculateHexagonShootingAngles()
    {
        for(int i = 0; i < hexagons.Count; i++)
        {
            hexagons[i].GetComponent<HexagonMesh>().UpdatePositionAngleToShootingAngle();
        }
    }

    // To be called by missile when collisions happen
    public void RemoveHexagon(GameObject obj)
    {
        if(hexagons.Contains(obj))
        {
            hexagons.Remove(obj);
        }
        else
        {
            Debug.LogError("Cannot remove this Hexagon.");
        }
    }

    public HexagonMesh GetBestHexagon()
    {
        int max = 0;
        GameObject best = null;
        for(int i = 0; i < hexagons.Count; i++)
        {
            if(!hexagons[i].GetComponent<HexagonMesh>().GetIsVisible())
            {
                continue;
            }
            int cur = hexagons[i].GetComponent<HexagonMesh>().MaxTwoShotValue();
            if (cur > max)
            {
                max = cur;
                best = hexagons[i];
            }
        }
        return best.GetComponent<HexagonMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
