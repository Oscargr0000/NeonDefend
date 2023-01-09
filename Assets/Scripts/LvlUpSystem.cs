using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LvlUpSystem : MonoBehaviour
{
    private TowerSystem _towerSystem;
    private TowerSystem stats_tower;

    
    

    public GameObject currentTower;
    

    public TextMeshProUGUI lvltext;
    public TextMeshProUGUI currentTowerText;


    private void Awake()
    {
        _towerSystem = FindObjectOfType<TowerSystem>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void SelectTower()
    {
        
        stats_tower = currentTower.GetComponent<TowerSystem>();


        //Update Selection
        currentTowerText.text = currentTower.name;
        lvltext.text = stats_tower.currentLvl.ToString();

    }

    public void OnButtonLvl(int added)
    {
        stats_tower.currentLvl += added;
        lvltext.text = stats_tower.currentLvl.ToString();
        _towerSystem.currentUpgrade = added;
    }
}
