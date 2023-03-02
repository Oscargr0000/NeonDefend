using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{


    #region Singleton
    public static UiManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion   

    private TowerSystem _ts;
    private LvlUpSystem _ls;
    private GameManager _gm;
    private SpawnManager _sp;


    public GameObject lvlCanvas;
    public Animator animationCanvas;
    public bool panelIsOpen;

    public TextMeshProUGUI roundsText;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI noPoints;

    public GameObject[] visualsR1;
    public GameObject[] visualsR2;

    private void Start()
    {
        _ts = FindObjectOfType<TowerSystem>();
        _ls = FindObjectOfType<LvlUpSystem>();
        _gm = FindObjectOfType<GameManager>();
        _sp = FindObjectOfType<SpawnManager>();

        noPoints.gameObject.SetActive(false);
        UpdatePoints();
        UpdateRounds();
        HpUpdate();
    }

    public void OpenLvlPanel()
    {
        if (panelIsOpen.Equals(false)) // ABRIR
        {
            panelIsOpen = true;
            animationCanvas.SetTrigger("Open");
            animationCanvas.SetBool("isOpen", false);

        }
        else // CERRAR
        {
            panelIsOpen = false;
            animationCanvas.SetTrigger("Close");
            animationCanvas.SetBool("isOpen", true);
        }

    }

    public void HpUpdate()
    {
        hpText.text = _gm.playerHP.ToString();
    }

    public void UpdatePoints()
    {
        pointsText.text = _gm.points.ToString();
    }

    public void UpdateRounds()
    {
        roundsText.text = _sp.rounds.ToString();
    }

    public void NoPointsAnoun()
    {
        noPoints.gameObject.SetActive(true);
        StartCoroutine(CoolDown(1, noPoints.gameObject));
    }

    IEnumerator CoolDown(int time, GameObject text)
    {
        yield return new WaitForSeconds(time);
        text.SetActive(false);
    }
}
