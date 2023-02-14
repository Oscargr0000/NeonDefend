using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{

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

    public GameObject[] visualsR1;
    public GameObject[] visualsR2;

    private void Start()
    {
        _ts = FindObjectOfType<TowerSystem>();
        _ls = FindObjectOfType<LvlUpSystem>();
        _gm = FindObjectOfType<GameManager>();
        _sp = FindObjectOfType<SpawnManager>();
    }


    private void LateUpdate()
    {
        roundsText.text = _sp.rounds.ToString(); // CAMBIAR ESTO y colocar las actualzacion cada vez que ocurra algo
        pointsText.text = _gm.points.ToString();
        hpText.text = _gm.playerHP.ToString();
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
}
