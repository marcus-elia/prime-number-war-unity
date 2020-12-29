using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameMode { Alone, VersusComputer };

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI computerScoreText;

    public static int playerScore = 0;
    public static int computerScore = 0;

    public static GameMode gameMode;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        computerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GivePlayerPoints(int n)
    {
        playerScore += n;
        playerScoreText.text = playerScore.ToString();
    }

    public void GiveComputerPoints(int n)
    {
        computerScore += n;
        computerScoreText.text = computerScore.ToString();
    }
}

