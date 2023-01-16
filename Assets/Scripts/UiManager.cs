using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{

    private TowerSystem _ts;
    private LvlUpSystem _ls;
    private GameManager _gm;


    public GameObject lvlCanvas;
    public Animator animationCanvas;
    public bool panelIsOpen;

    public TextMeshProUGUI pointsText;

    private void Awake()
    {
        _ts = FindObjectOfType<TowerSystem>();
        _ls = FindObjectOfType<LvlUpSystem>();
        _gm = FindObjectOfType<GameManager>();
    }


    private void LateUpdate()
    {
        pointsText.text = _gm.points.ToString();
    }
    public void OpenLvlPanel()
    {
        print("EL BOTON FUNCIONA");
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
