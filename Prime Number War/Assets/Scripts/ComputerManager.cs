using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    private float trackRadius = 5f;
    private int smoothness = 24;
    private float lineWidth = 0.1f;

    public GameObject computerPrefab;
    private GameObject computer;

    public GameObject missilePrefab;
    private GameObject missile;
    private bool missileIsActive = false;

    public HexagonManager hexagonManager;

    private HexagonMesh target;
    private bool targetExists;


    // Start is called before the first frame update
    void Start()
    {
        GenerateComputer();
    }

    private void GenerateComputer()
    {
        computer = Instantiate(computerPrefab);
        computer.GetComponent<ComputerMesh>().CreateShape();
        computer.GetComponent<ComputerMesh>().SetLocation(new Vector3(trackRadius, 0, 0));
        computer.GetComponent<ComputerMesh>().SetTrackRadius(trackRadius);
    }

    // Update is called once per frame
    void Update()
    {
        if(!computer.GetComponent<ComputerMesh>().GetNeedsToMove() && !missileIsActive)
        {
            FireMissile();
            missileIsActive = true;
        }
    }

    private void FireMissile()
    {
        Destroy(missile);
        missile = Instantiate(missilePrefab);
        missile.GetComponent<MissileMesh>().CreateShape();
        missile.GetComponent<MissileMesh>().SetLocation(computer.transform.position);
        Vector2 worldPosition = Vector2.zero;
        float angle = Mathf.Atan2(worldPosition.y - computer.transform.position.y, worldPosition.x - computer.transform.position.x);
        missile.GetComponent<MissileMesh>().SetAngle(angle);
        missile.GetComponent<MissileMesh>().CalculateVelocity();
    }

    public void SetMissileIsActive(bool input)
    {
        missileIsActive = input;
    }
}
