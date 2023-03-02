using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(0);

        // HIGH SCORE
        if(totalEnemyKill > PlayerPrefs.GetInt("EnemyKill"))
        {
            PlayerPrefs.SetInt("EnemyKill", totalEnemyKill);
        }
        
    }
}
