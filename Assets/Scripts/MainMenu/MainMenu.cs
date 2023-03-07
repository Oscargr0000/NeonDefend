using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas optionsCanvas;
    public Canvas howtoCanvas;

    public Slider loadbar;

    public AudioMixer mixerMusic;
    public AudioMixer mixerEffects;

    public Slider musicSlide;
    public Slider effectSlide;

    public AudioSource effectsMenu;

    public AudioClip[] sounds;

    private void Start()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        optionsCanvas.gameObject.SetActive(false);
        howtoCanvas.gameObject.SetActive(false);
        loadbar.gameObject.SetActive(false);

        musicSlide.value = PlayerPrefs.GetFloat("Music");
        effectSlide.value = PlayerPrefs.GetFloat("Efectoss");

        
    }
    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void GoOptions()
    {
        optionsCanvas.gameObject.SetActive(true);
        mainMenuCanvas.gameObject.SetActive(false);

        effectsMenu.PlayOneShot(sounds[1]);
    }

    public void GoMenu()
    {
        optionsCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);
        howtoCanvas.gameObject.SetActive(false);

        effectsMenu.PlayOneShot(sounds[0]);
    }

    public void GoHow()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        howtoCanvas.gameObject.SetActive(true);

        effectsMenu.PlayOneShot(sounds[1]);
    }

    public void SceneLoad(int sceneInx)
    {
        effectsMenu.PlayOneShot(sounds[2]);
        loadbar.gameObject.SetActive(true);

        StartCoroutine(LoadAsync(sceneInx));
    }

    IEnumerator LoadAsync(int sceneindx)
    {
        AsyncOperation asyncoperation = SceneManager.LoadSceneAsync(sceneindx);
        while (!asyncoperation.isDone)
        {
            Debug.Log(asyncoperation.progress);
            loadbar.value = asyncoperation.progress;
            yield return null;
        }
    }

    public void ChangeMusicVol(float sliderValue)
    {
        PlayerPrefs.SetFloat("Music", sliderValue);
        mixerMusic.SetFloat("MusicVol", Mathf.Log10(PlayerPrefs.GetFloat("Music")) *20);
        
    }

    public void ChangeEffectVol(float sliderValue)
    {
        PlayerPrefs.SetFloat("Efectoss", sliderValue);
        mixerEffects.SetFloat("EfectosVol", Mathf.Log10(PlayerPrefs.GetFloat("Efectoss")) * 20);

    }

    public void Exit()
    {
        Application.Quit();
    }

}
