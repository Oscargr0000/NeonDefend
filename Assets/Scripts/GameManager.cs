using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int points;
    public int rounds;
    public int maxRounds;

    public int playerHP;

    public int totalEnemyKill;

    private bool pauseMenu;

    public Canvas pauseCanvas;

    public AudioMixer musicMixer;
    public AudioMixer effectMixer;

    public Slider musicSlider;
    public Slider effectsSlider;

    void Start()
    {
        //STATS
        points = 150;
        playerHP = 100;

        UiManager.Instance.HpUpdate();

        // PAUSE MENU
        pauseCanvas.gameObject.SetActive(false);

        musicSlider.value = PlayerPrefs.GetFloat("Music");
        effectsSlider.value = PlayerPrefs.GetFloat("Efectoss");
    }

    private void Update()
    {

        // USED FOR THE PRESENTATION
        if (Input.GetKeyDown(KeyCode.F))
        {
            points += 1000;
            UiManager.Instance.UpdatePoints();
        }

        // CLOSE AND OPEN DE PAUSE MENU
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu)
            {
                OpenPauseMenu();
            }
            else
            {
                ClosePauseMenu();
            }
        }
    }

    // SAVE INFO ON GAME OVER
    public void GameOver()
    {
        PlayerPrefs.SetInt("TotalKill", totalEnemyKill);
        PlayerPrefs.SetInt("Rounds",SpawnManager.Instance.rounds);
       
        SceneManager.LoadScene(2);
    }


    



    void OpenPauseMenu()
    {
        pauseMenu = true;

        
        Time.timeScale = 0;
        pauseCanvas.gameObject.SetActive(true);


    }

    void ClosePauseMenu()
    {
        pauseMenu = false;


        Time.timeScale = 1;
        pauseCanvas.gameObject.SetActive(false);
    }

    public void ChangeMusicVol(float sliderValue)
    {
        PlayerPrefs.SetFloat("Music", sliderValue);
        musicMixer.SetFloat("MusicVol", Mathf.Log10(PlayerPrefs.GetFloat("Music")) * 20);

    }

    public void ChangeEffectVol(float sliderValue)
    {
        PlayerPrefs.SetFloat("Efectoss", sliderValue);
        effectMixer.SetFloat("EfectosVol", Mathf.Log10(PlayerPrefs.GetFloat("Efectoss")) * 20);

    }

    public void GoScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
