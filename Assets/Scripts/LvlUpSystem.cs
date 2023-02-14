using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LvlUpSystem : MonoBehaviour
{
    //-----------------------------CONNECTIONS-----------------------------------
    private TowerSystem _towerSystem;
    public TowerSystem stats_tower;
    private Upgrades _upgrades;
    private GameManager _gm;
    private UiManager _uiM;
    //____________________________________________________________________________


    private SpriteRenderer spriteRenderer;


    public GameObject currentTower;
    

    public TextMeshProUGUI R1text;
    public TextMeshProUGUI R2text;
    public TextMeshProUGUI currentTowerText;

    public Button rama1Button;
    public Button rama2Button;


    private void Start()
    {
        _upgrades = FindObjectOfType<Upgrades>();
        _towerSystem = FindObjectOfType<TowerSystem>();
        _gm = FindObjectOfType<GameManager>();
        _uiM = FindObjectOfType<UiManager>();
    }

    public void SelectTower()
    {
        stats_tower = currentTower.GetComponent<TowerSystem>();
        spriteRenderer = currentTower.GetComponent<SpriteRenderer>();


        // ABRIR PANEL CUANDO SELECCIONES UNA TORRE
        if (_uiM.panelIsOpen.Equals(false)) 
        {
            _uiM.panelIsOpen = true;
            _uiM.animationCanvas.SetTrigger("Open");
            _uiM.animationCanvas.SetBool("isOpen", false);
        }


        //Update Selection
        currentTowerText.text = stats_tower.scriptable_Stats.TowerName;
        R1text.text = stats_tower.idxR1.ToString();
        R2text.text = stats_tower.idxR2.ToString();

        RefreshButtons();

    }

    public void OnButtonLvl(int added)
    {
        if(currentTower == null)
        {
            print("No tienes ningnuna torre seleccionada");
            return;
        }

        if(stats_tower.idxR1 != stats_tower.maxR1)
        {
            if (added.Equals(1))
            {
                if(_gm.points>= stats_tower.scriptable_Stats.priceR1[stats_tower.idxR1])
                {
                    _gm.points -= stats_tower.scriptable_Stats.priceR1[stats_tower.idxR1];
                    stats_tower.idxR1++;
                    _upgrades.UpgradeR1();
                }
                else
                {
                    print("No cuentas con puntos suficientes"); 
                }
                
            }
        }
        
        if(stats_tower.idxR2 != stats_tower.maxR2)
        {
            if(added.Equals(2))
            {
                if (_gm.points >= stats_tower.scriptable_Stats.priceR2[stats_tower.idxR2])
                {
                    _gm.points -= stats_tower.scriptable_Stats.priceR2[stats_tower.idxR2];
                    stats_tower.idxR2++;
                    _upgrades.UpgradeR2();
                }
                else
                {
                    print("No cuentas con puntos suficientes");
                }
            }
        }


        //LIMITA EL NIVEL
        if (stats_tower.idxR1 >= 3)
        {
            stats_tower.maxR2 = 2;
            RefreshButtons();
        }else if(stats_tower.idxR2 >= 3)
        {
            stats_tower.maxR1 = 2;
            RefreshButtons();
        }

        R1text.text = stats_tower.idxR1.ToString();
        R2text.text = stats_tower.idxR2.ToString();
        
    }

    void RefreshButtons()
    {
        if (stats_tower.maxR1.Equals(stats_tower.idxR1))
        {
            rama1Button.interactable = false;
        }
        else
        {
            rama1Button.interactable = true;
        }

        if (stats_tower.maxR2.Equals(stats_tower.idxR2))
        {
            rama2Button.interactable = false;
        }
        else
        {
            rama2Button.interactable = true;
        }
    }


    public void SellTower()
    {
        if(currentTower.gameObject != null)
        {
            _gm.points += stats_tower.price / 2;
            Destroy(stats_tower.gameObject);
            RefreshButtons();
        }
        
    }
}
