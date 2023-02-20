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

    private int indxTowerSel;
    public SpriteRenderer[] bluePrints;
    public LayerMask carrilLayer;


    public float radius = 1f;
    public Color rayColor = Color.green;


    private void Start()
    {
        settingMode = false;
        _gm = FindObjectOfType<GameManager>();

        for(int i = 0; i < bluePrints.Length; i++)
        {
            bluePrints[i].gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(MousePos(), radius, Vector2.zero, 0, carrilLayer);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                rayColor = Color.red;
                print("No puedes colocar aqui");

            }
        }

        if (settingMode && Input.GetKeyDown(KeyCode.Mouse0))
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {

                    return;
                }
            }

            SetTower(MousePos());
            return;
        }
        
        if (settingMode)
        {
            SelectionGhost(MousePos());
        }


        
            

    }


    void SetTower(Vector2 MousePos)
    {
        //Colocar la torre en la posicion del raton
        Instantiate(selectedTower, MousePos, Quaternion.identity);

        //Resta del precio a los puntos
        _gm.points = _gm.points -= towerPrice;

       

        //Quita todo lo relacionado con la seleccion
        editModeText.text = "OFF";
        selectedTower = null;
        towerPrice = 0;
        settingMode = false;
        bluePrints[indxTowerSel].gameObject.SetActive(false);
    }


    public void SelectMode(int towerToSelect)
    {
        if(_gm.points >= priceList[towerToSelect])
        {
            settingMode = true;

            indxTowerSel = towerToSelect;
            bluePrints[indxTowerSel].gameObject.SetActive(true);

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
    
    void SelectionGhost(Vector2 MousePos)
    {
        print("AGARRA");
        bluePrints[indxTowerSel].transform.position = MousePos;
    }


    Vector2 MousePos()
    {
        //Acede a la posicion del mouse cuando hace click
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
        Vector2 objectPos2D = new Vector2(objectPos.x, objectPos.y);

        return objectPos2D;
    }

    void OnDrawGizmosSelected()
    {
        DrawCircleCast(MousePos());
    }

    void OnDrawGizmos()
    {
        DrawCircleCast(MousePos());
    }

    private void DrawCircleCast(Vector3 center)
    {
        Gizmos.color = rayColor;
        Vector3 direction = (center - transform.position).normalized;
        Vector3 endPoint = transform.position + direction * radius;

        Gizmos.DrawWireSphere(center, radius);
        Gizmos.DrawLine(center, endPoint);
    }


}
