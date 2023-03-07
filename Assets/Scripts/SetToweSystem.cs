using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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


    public GameObject postProcess;
    private ChannelMixer cm;

    public AudioClip[] sounds;

    private void Start()
    {
        Volume postpro = postProcess.GetComponent<Volume>();
        postpro.profile.TryGet(out cm);

        cm.active = false;

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
                Volume postpro = postProcess.GetComponent<Volume>();

                postpro.profile.TryGet(out cm);

            }
            else
            {
                cm.active = false;
            }
        }

        if (settingMode && Input.GetKeyDown(KeyCode.Mouse0))
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    StartCoroutine(AnimationRed());
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


        //PLAY SOUNDS
        AudioManager.Instance.PlaySound(this.gameObject, sounds[0]);
       

        //Quita todo lo relacionado con la seleccion
        selectedTower = null;
        towerPrice = 0;
        settingMode = false;
        bluePrints[indxTowerSel].gameObject.SetActive(false);
        UiManager.Instance.UpdatePoints();
    }


    public void SelectMode(int towerToSelect)
    {
        if(_gm.points >= priceList[towerToSelect])
        {
            settingMode = true;

            AudioManager.Instance.PlaySound(this.gameObject, sounds[1]);

            indxTowerSel = towerToSelect;
            bluePrints[indxTowerSel].gameObject.SetActive(true);

            //Accede a la torre que pretende colocar
            selectedTower = towersObjets[towerToSelect];

            towerPrice = priceList[towerToSelect];
            UiManager.Instance.UpdatePoints();

        }
        else
        {
            UiManager.Instance.NoPointsAnoun();
        }
    }

   
    
    void SelectionGhost(Vector2 MousePos)
    {
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


    IEnumerator AnimationRed()
    {
        cm.active = true;
        yield return new WaitForSeconds(0.2f);
        cm.active = false;
    }

}
