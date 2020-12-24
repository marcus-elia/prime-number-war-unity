using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI computerScoreText;

    private int playerScore = 0;
    private int computerScore = 0;

    // Start is called before the first frame update
    void Start()
    {

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

