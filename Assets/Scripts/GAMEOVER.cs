using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GAMEOVER : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI totalRounds;
    public TextMeshProUGUI enemyText;
    public TextMeshProUGUI highScore;
    // Start is called before the first frame update
    void Start()
    {

       

        int HighScore = PlayerPrefs.GetInt("EnemyKill");
        int totalKill = PlayerPrefs.GetInt("TotalKill");
        // HIGH SCORE
        if (totalKill > HighScore)
        {
            print("llega");
            PlayerPrefs.SetInt("EnemyKill", totalKill);
        }



        resultText.text = PlayerPrefs.GetString("Resul");
        totalRounds.text = PlayerPrefs.GetInt("Rounds").ToString();
        enemyText.text = PlayerPrefs.GetInt("TotalKill").ToString();
        highScore.text = PlayerPrefs.GetInt("EnemyKill").ToString();

    }
}
