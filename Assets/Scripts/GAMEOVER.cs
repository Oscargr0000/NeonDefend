using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GAMEOVER : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI totalRounds;
    public TextMeshProUGUI enemyText;
    public TextMeshProUGUI highScore;

    public AudioClip[] sounds;
 

    void Start()
    {
        //GET INFORMATION FROM DATA
        int HighScore = PlayerPrefs.GetInt("EnemyKill");
        int totalKill = PlayerPrefs.GetInt("TotalKill");
        int currentRounds = PlayerPrefs.GetInt("Rounds");


        // CHANGE IF WIN OR LOSE DEPEND OF THE RESULT
        if(currentRounds>= 30)
        {
            resultText.text = "WIN";
            AudioManager.Instance.PlaySound(this.gameObject, sounds[0]);
        }
        else
        {
            resultText.text = "GAME OVER";
            AudioManager.Instance.PlaySound(this.gameObject, sounds[1]);

        }

        // HIGH SCORE
        if (totalKill > HighScore)
        {
            print("llega");
            PlayerPrefs.SetInt("EnemyKill", totalKill);
        }


        // SET DATA TO THE TEXT
        totalRounds.text = currentRounds.ToString();
        enemyText.text = totalKill.ToString();
        highScore.text = PlayerPrefs.GetInt("EnemyKill").ToString();

    }


    public void GoScene(int scene)
    {
        SceneManager.LoadScene(scene); 
    }

    
}
