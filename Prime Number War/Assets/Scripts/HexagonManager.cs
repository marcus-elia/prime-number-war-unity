using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonManager : MonoBehaviour
{
    public GameObject hexagonPrefab;

    private List<GameObject> hexagons;

    // Start is called before the first frame update
    void Start()
    {
        GameObject h1 = Instantiate(hexagonPrefab);
        h1.GetComponent<HexagonMesh>().SetLocation(0f, 0f);
        h1.GetComponent<HexagonMesh>().GenerateRandomNumber();
        h1.GetComponent<HexagonMesh>().CreateHexagon();
        h1.GetComponent<HexagonMesh>().CreateText();

        GameObject h2 = Instantiate(hexagonPrefab);
        h2.GetComponent<HexagonMesh>().SetLocation(1.5f, 0.866f);
        h2.GetComponent<HexagonMesh>().GenerateRandomNumber();
        h2.GetComponent<HexagonMesh>().CreateHexagon();
        h2.GetComponent<HexagonMesh>().CreateText();

        GameObject h3 = Instantiate(hexagonPrefab);
        h3.GetComponent<HexagonMesh>().SetLocation(1.5f, -0.866f);
        h3.GetComponent<HexagonMesh>().GenerateRandomNumber();
        h3.GetComponent<HexagonMesh>().CreateHexagon();
        h3.GetComponent<HexagonMesh>().CreateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
