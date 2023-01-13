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
        switch (_ts.idxR1)
        {
            case 1:
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
        switch (_ts.idxR2)
        {
            case 1:
                print("More Range");
                break;
            case 2:
                _lvlUpS.stats_tower.destroyBullet = true;

                print("Dont't Destroy on hit");
                break;

            case 3:
                print("will shoot fire");
                _lvlUpS.stats_tower.fireBullet = true;  
                break;

            case 4:
                print("2 cannons");
                break;
        }
    }

    void ActivateFire()
    {
        towerScriptable.fireBullet = true;
    }

    void DontDestroyBullet()
    {

    }

    void NumCannon(int numcanyones)
    {
        if (numcanyones.Equals(1))
        {
            
        }else if (numcanyones.Equals(2))
        {

        }
    }

    
}
