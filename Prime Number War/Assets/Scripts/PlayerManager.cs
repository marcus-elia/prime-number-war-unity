using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private float trackRadius = 5f;
    private int smoothness = 24;
    private float lineWidth = 0.1f;

    public GameObject playerPrefab;
    private GameObject player;

    public GameObject missilePrefab;
    private GameObject missile;

    // Start is called before the first frame update
    void Start()
    {
        GeneratePlayer();
    }

    private void GeneratePlayer()
    {
        player = Instantiate(playerPrefab);
        player.GetComponent<PlayerMesh>().CreateShape();
        player.GetComponent<PlayerMesh>().SetLocation(new Vector3(-5, 0, 0));
        player.GetComponent<PlayerMesh>().SetTrackRadius(trackRadius);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Destroy(missile);
            missile = Instantiate(missilePrefab);
            missile.GetComponent<MissileMesh>().CreateShape();
            missile.GetComponent<MissileMesh>().SetLocation(player.transform.position);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Atan2(worldPosition.y - player.transform.position.y, worldPosition.x - player.transform.position.x);
            missile.GetComponent<MissileMesh>().SetAngle(angle);
            missile.GetComponent<MissileMesh>().CalculateVelocity();
        }
    }
}
