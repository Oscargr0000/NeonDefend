using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int points;
    public int rounds;
    public int maxRounds;

    public int playerHP;

    public int totalEnemyKill;

    

    // Start is called before the first frame update
    void Start()
    {
        points = 150;
        playerHP = 100;

        UiManager.Instance.HpUpdate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            points += 1000;
            UiManager.Instance.UpdatePoints();
        }
    }

    public void GameOver()
    {
        PlayerPrefs.SetString("Resul", "GAME OVER");
        PlayerPrefs.SetInt("TotalKill", totalEnemyKill);
        PlayerPrefs.SetInt("Rounds",SpawnManager.Instance.rounds);
       
        SceneManager.LoadScene(2);
    }
}
