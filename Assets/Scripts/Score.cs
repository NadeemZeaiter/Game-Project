using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public double updateScore=0;
   
    // Update is called once per frame
    public void updateScores()
    {
        updateScore +=0.07;
        scoreText.text= "Score: " + updateScore.ToString("0");
    }
}
