using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScoreManager : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI computerScoreText;

    public TextMeshProUGUI result;

    // Start is called before the first frame update
    void Start()
    {
        playerScoreText.text = ScoreManager.playerScore.ToString();
        if(ScoreManager.gameMode == GameMode.VersusComputer)
        {
            computerScoreText.text = ScoreManager.computerScore.ToString();

            if(ScoreManager.playerScore > ScoreManager.computerScore)
            {
                result.text = "You Won";
            }
            else if (ScoreManager.playerScore < ScoreManager.computerScore)
            {
                result.text = "You Lost";
            }
            else
            {
                result.text = "You Tied";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
