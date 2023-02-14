using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecctionSystem : MonoBehaviour
{
    private LvlUpSystem _LvlSystem;

    private void Start()
    {
        _LvlSystem = FindObjectOfType<LvlUpSystem>();
    }


    //SELECIONA LA TORRE
    private void OnMouseDown()
    {
        if(this.transform.GetChild(0).gameObject != null)
        {
            _LvlSystem.currentTower = this.transform.GetChild(0).gameObject;
            _LvlSystem.SelectTower();
        }
       
    }
}
