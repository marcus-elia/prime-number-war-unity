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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
