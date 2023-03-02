using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas optionsCanvas;

    public Slider loadbar;

    private void Start()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        optionsCanvas.gameObject.SetActive(false);
        loadbar.gameObject.SetActive(false);
    }
    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void GoOptions()
    {
        optionsCanvas.gameObject.SetActive(true);
        mainMenuCanvas.gameObject.SetActive(false);
    }

    public void GoMenu()
    {
        optionsCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);
    }

    public void SceneLoad(int sceneInx)
    {
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

}
