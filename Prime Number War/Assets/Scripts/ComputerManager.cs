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
    private bool targetExists = false;

    private bool needsToGetNewTarget = true;
    private bool isMoving = false;

    private float moveToAngle = 0f;
    private float shootAtAngle = 0f;


    // Start is called before the first frame update
    void Start()
    {
        GenerateComputer();
    }

    private void GenerateComputer()
    {
        computer = Instantiate(computerPrefab);
        computer.GetComponent<ComputerMesh>().SetComputerManager(this);
        computer.GetComponent<ComputerMesh>().CreateShape();
        computer.GetComponent<ComputerMesh>().SetLocation(new Vector3(trackRadius, 0, 0));
        computer.GetComponent<ComputerMesh>().SetTrackRadius(trackRadius);
    }

    // Update is called once per frame
    void Update()
    {
        if(needsToGetNewTarget)
        {
            // Find new target
            target = hexagonManager.GetBestHexagonDistanceScaled(computer.GetComponent<ComputerMesh>().GetCurAngle());
            //target = hexagonManager.GetBestHexagonPeriod();
            Vector2 shootingData = target.GetShootingAngle(computer.GetComponent<ComputerMesh>().GetCurAngle());
            moveToAngle = shootingData[0];
            shootAtAngle = shootingData[1];


            // Tell ComputerMesh to move there
            computer.GetComponent<ComputerMesh>().SetTargetAngle(moveToAngle);

            Debug.Log("Moving to angle " + moveToAngle.ToString() + ", shooting at number " + target.GetNumber());

            needsToGetNewTarget = false;
            isMoving = true;
        }
        if(!isMoving && !missileIsActive && !needsToGetNewTarget)
        {
            FireMissile();

            missileIsActive = true;
            isMoving = false;
            needsToGetNewTarget = true;
        }
        
        // Check if missile is too far away
        if(missile && Vector2.Distance(missile.transform.position, Vector2.zero) > trackRadius + 1)
        {
            Destroy(missile);
            missileIsActive = false;
            needsToGetNewTarget = true;
        }
    }

    private void FireMissile()
    {
        Destroy(missile);
        missile = Instantiate(missilePrefab);
        missile.GetComponent<MissileMesh>().SetOwner(MissileOwner.Computer);
        missile.GetComponent<MissileMesh>().SetComputerManager(this);
        missile.GetComponent<MissileMesh>().SetHexagonManager(this.hexagonManager);
        missile.GetComponent<MissileMesh>().CreateShape();
        missile.GetComponent<MissileMesh>().SetLocation(computer.transform.position);
        missile.GetComponent<MissileMesh>().SetAngle(shootAtAngle);
        missile.GetComponent<MissileMesh>().CalculateVelocity();
    }

    // The missile should call this when it is destroyed
    public void SetMissileIsActive(bool input)
    {
        missileIsActive = input;
    }
    public void SetIsMoving(bool input)
    {
        isMoving = input;
    }
    public void SetNeedsToGetNewTarget(bool input)
    {
        needsToGetNewTarget = input;
    }
}
