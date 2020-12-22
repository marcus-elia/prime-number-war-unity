using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonManager : MonoBehaviour
{
    public GameObject hexagonPrefab;

    private List<GameObject> hexagons;

    public static float sideLength = 0.5f;
    public static int boardSize = 5; // There are 2*boardSize - 1 columns

    // Start is called before the first frame update
    void Start()
    {
        generateHexagons();
    }

    private void generateHexagons()
    {
        GameObject h;
        hexagons = new List<GameObject>();

        float sqrt3 = Mathf.Sqrt(3);
        
        // Left half and center column
        for(int i = 1; i < boardSize + 1; i++)
        {
            float x = -3 * sideLength / 2 * (boardSize + 1 - i) + 1;
            float y = -(boardSize + i - 3) * sideLength * sqrt3 / 2 - sideLength * sqrt3;
            for(int j = 1; j < boardSize + i; j++)
            {
                h = Instantiate(hexagonPrefab);
                h.GetComponent<HexagonMesh>().GenerateRandomNumber();
                h.GetComponent<HexagonMesh>().SetSideLength(sideLength);
                h.GetComponent<HexagonMesh>().SetLocation(x, y);
                h.GetComponent<HexagonMesh>().SetColor();
                h.GetComponent<HexagonMesh>().CreateHexagon();
                h.GetComponent<HexagonMesh>().CreateText();

                hexagons.Add(h);

                y += sideLength * sqrt3;
            }
        }

        // Right half
        for(int i = 1; i < boardSize; i++)
        {
            float x = 3 * sideLength / 2 * (boardSize - 1 - i) + 1;
            float y = -(boardSize + i - 3) * sideLength * sqrt3 / 2 - sideLength * sqrt3;
            for(int j = 1; j < boardSize + i; j++)
            {
                h = Instantiate(hexagonPrefab);
                h.GetComponent<HexagonMesh>().GenerateRandomNumber();
                h.GetComponent<HexagonMesh>().SetSideLength(sideLength);
                h.GetComponent<HexagonMesh>().SetLocation(x, y);
                h.GetComponent<HexagonMesh>().SetColor();
                h.GetComponent<HexagonMesh>().CreateHexagon();
                h.GetComponent<HexagonMesh>().CreateText();

                hexagons.Add(h);

                y += sideLength * sqrt3;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
