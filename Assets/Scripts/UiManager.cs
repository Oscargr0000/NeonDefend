using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{

    private TowerSystem _ts;
    private LvlUpSystem _ls;


    public GameObject lvlCanvas;
    public Animator animationCanvas;
    public bool panelIsOpen;

    private void Awake()
    {
        _ts = FindObjectOfType<TowerSystem>();
        _ls = FindObjectOfType<LvlUpSystem>();
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
