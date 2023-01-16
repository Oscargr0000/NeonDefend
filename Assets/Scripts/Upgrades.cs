using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    [SerializeField]private TowerScriptableObject towerScriptable;
    private TowerSystem _ts;
    private LvlUpSystem _lvlUpS;
    private ProyectailLogic _pl;

    private void Awake()
    {
        _ts = FindObjectOfType<TowerSystem>();
        _lvlUpS = FindObjectOfType<LvlUpSystem>();
    }

    public void UpgradeR1()
    {
        switch (_lvlUpS.stats_tower.idxR1)
        {
            case 1:
                if()
                print("mas danyo");
                _lvlUpS.stats_tower.damage += 1;
                break;
            case 2:
                print("dispara mas rapido");
                _lvlUpS.stats_tower.shootingSpeed -= 0.2f;
                break;

            case 3:
                print("Ve camuflados");
                _lvlUpS.stats_tower.seeCamuf = true;
                break;
            case 4:
                print("hace mucho danyo");
                _lvlUpS.stats_tower.damage += 2;
                break;
        }
    }

    public void UpgradeR2()
    {
        switch (_lvlUpS.stats_tower.idxR2)
        {
            case 1:
                _lvlUpS.stats_tower.range += 2f;
                print("More Range");
                break;
            case 2:
                DontDestroyBullet();

                print("Dont't Destroy on hit");
                break;

            case 3:
                ActivateFire();
                print("will shoot fire");
               
                break;

            case 4:
                NumCannon();
                print("2 cannons");
                break;
        }
    }

    void ActivateFire()
    {
        _lvlUpS.stats_tower.fireBullet = true;

        if (_lvlUpS.stats_tower.destroyBullet.Equals(true))
        {
            _lvlUpS.stats_tower.proyectail = towerScriptable.fireDestroy;
        }
        else
        {
            _lvlUpS.stats_tower.proyectail = towerScriptable.fireDontDestroyObj;
        }
        
    }

    void DontDestroyBullet()
    {
        _lvlUpS.stats_tower.destroyBullet = false;

        if (_lvlUpS.stats_tower.fireBullet.Equals(true))
        {
            _lvlUpS.stats_tower.proyectail = towerScriptable.fireDontDestroyObj;
        }
        else
        {
            _lvlUpS.stats_tower.proyectail = towerScriptable.notFireDestroy;
        }
    }

    void NumCannon()
    {
        _lvlUpS.currentTower.transform.GetChild(0).gameObject.SetActive(false);
        _lvlUpS.currentTower.transform.GetChild(1).gameObject.SetActive(true);
    }

    
}
