using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetToweSystem : MonoBehaviour
{
    public Canvas towersCan;
    public TextMeshProUGUI editModeText;

    public Button[] towersButton;
    public GameObject[] towersObjets;
    private GameObject selectedTower;
    private GameManager _gm;

    private bool settingMode;

    public int[] priceList;
    private int towerPrice;

    private void Start()
    {
        _gm = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (settingMode && Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetTower();
        }
    }


    void SetTower()
    {
        //Acede a la posicion del mouse cuando hace click
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
        Vector2 objectPos2D = new Vector2(objectPos.x, objectPos.y);



        //Colocar la torre en la posicion del raton
        Instantiate(selectedTower, objectPos2D, Quaternion.identity);

        //Resta del precio a los puntos
        _gm.points = _gm.points -= towerPrice;


        //Quita todo lo relacionado con la seleccion
        editModeText.text = "OFF";
        selectedTower = null;
        towerPrice = 0;
        settingMode = false;
    }


    public void SelectMode(int towerToSelect)
    {
        if(_gm.points >= priceList[towerToSelect])
        {
            settingMode = true;

            //Accede a la torre que pretende colocar
            selectedTower = towersObjets[towerToSelect];

            towerPrice = priceList[towerToSelect];

            EditMode();
        }
    }

    void EditMode()
    {
        editModeText.text = "ON";
    }
}