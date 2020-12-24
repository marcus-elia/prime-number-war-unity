using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonManager : MonoBehaviour
{
    public GameObject hexagonPrefab;

    public GameObject scoreManager;

    private List<GameObject> hexagons;

    public static float sideLength = 0.5f;
    public static int boardSize = 5; // There are 2*boardSize - 1 columns

    // Start is called before the first frame update
    void Start()
    {
        GenerateHexagons();
        CalculateHexagonNeighbors();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
