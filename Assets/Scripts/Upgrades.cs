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
            //---------MEJORA 1----------
            case 1:  

                switch (_lvlUpS.stats_tower.Type)
                {
                    case TowerType.Cannon:

                        print("mas danyo");
                        _lvlUpS.stats_tower.damage += 1;
                        break;

                    case TowerType.Sniper:

                        break;

                    case TowerType.Boomerang:

                        break;

                    case TowerType.Laser:

                        break;

                }
                break;

            //---------MEJORA 2----------
            case 2:

                switch (_lvlUpS.stats_tower.Type)
                {
                    case TowerType.Cannon:

                        print("dispara mas rapido");
                        _lvlUpS.stats_tower.shootingSpeed -= 0.2f;
                        break;

                    case TowerType.Sniper:

                        break;

                    case TowerType.Boomerang:

                        break;

                    case TowerType.Laser:

                        break;
                }
                break;

            //---------MEJORA 3----------
            case 3:

                switch (_lvlUpS.stats_tower.Type)
                {
                    case TowerType.Cannon:

                        print("Ve camuflados");
                        _lvlUpS.stats_tower.seeCamuf = true;
                        break;

                    case TowerType.Sniper:

                        break;

                    case TowerType.Boomerang:

                        break;

                    case TowerType.Laser:

                        break;

                }
                break;

            //---------MEJORA 4----------
            case 4:

                switch (_lvlUpS.stats_tower.Type)
                {
                    case TowerType.Cannon:

                        print("hace mucho danyo");
                        _lvlUpS.stats_tower.damage += 2;
                        break;

                    case TowerType.Sniper:

                        break;

                    case TowerType.Boomerang:

                        break;

                    case TowerType.Laser:

                        break;
                }
                break;
        }
    }

    public void UpgradeR2()
    {
        switch (_lvlUpS.stats_tower.idxR2)
        {

            //---------MEJORA 1----------
            case 1:
                switch (_lvlUpS.stats_tower.Type)
                {
                    case TowerType.Cannon:

                        _lvlUpS.stats_tower.range += 1f;
                        print("More Range");
                        break;

                    case TowerType.Sniper:

                        print("More Damage");

                        break;

                    case TowerType.Boomerang:

                        break;

                    case TowerType.Laser:

                        break;
                }
                break;

            //---------MEJORA 2----------
            case 2:

                switch (_lvlUpS.stats_tower.Type)
                {
                    case TowerType.Cannon:

                        DontDestroyBullet();

                        print("Dont't Destroy on hit");
                        break;

                    case TowerType.Sniper:

                        print("SeeCam");
                        break;

                    case TowerType.Boomerang:

                        break;

                    case TowerType.Laser:

                        break;
                }
                break;

            //---------MEJORA 3----------
            case 3:

                switch (_lvlUpS.stats_tower.Type)
                {
                    case TowerType.Cannon:

                        ActivateFire();
                        print("will shoot fire");
                        break;

                    case TowerType.Sniper:

                        print("MORE damage - slower");
                        break;

                    case TowerType.Boomerang:

                        break;

                    case TowerType.Laser:

                        break;
                }

                break;


            //---------MEJORA 4----------
            case 4:
                switch (_lvlUpS.stats_tower.Type)
                {
                    case TowerType.Cannon:

                        NumCannon();
                        print("2 cannons");
                        break;

                    case TowerType.Sniper:

                        print("La bala atraviesa globos");
                        break;

                    case TowerType.Boomerang:

                        break;

                    case TowerType.Laser:

                        break;
                }
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
