using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LvlUpSystem : MonoBehaviour
{
    private TowerSystem _towerSystem;
    private TowerSystem stats_tower;
    private SpriteRenderer spriteRenderer;


    public GameObject currentTower;
    

    public TextMeshProUGUI lvltext;
    public TextMeshProUGUI currentTowerText;

    public GameObject rama1Button;
    public GameObject rama2Button;


    private void Awake()
    {
        _towerSystem = FindObjectOfType<TowerSystem>();    
    }

    

    public void SelectTower()
    {
        
        stats_tower = currentTower.GetComponent<TowerSystem>();
        spriteRenderer = currentTower.GetComponent<SpriteRenderer>();


        //Update Selection
        currentTowerText.text = currentTower.name;
        lvltext.text = stats_tower.currentLvl.ToString();

    }

    public void OnButtonLvl(int added)
    {
        
        stats_tower.currentLvl += added;
        lvltext.text = stats_tower.currentLvl.ToString();
        spriteRenderer.sprite = _towerSystem.upgrade[stats_tower.currentLvl];
        stats_tower.LvlUps();

        switch (stats_tower.currentLvl)
        {
            case 8:
                rama2Button.SetActive(false);
                break;
            case 13:
                rama2Button.SetActive(false);
                break;

            case 15:
                rama1Button.SetActive(false);
                break;
            case 3:
                rama1Button.SetActive(false);
                break;

            default:
                rama1Button.SetActive(true);
                rama2Button.SetActive(true);
                break;
        }

    }
}
