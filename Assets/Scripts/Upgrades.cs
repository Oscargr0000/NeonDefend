using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    [SerializeField]private TowerScriptableObject towerScriptable;
    private TowerSystem _ts;
    private LvlUpSystem _lvlUpS;
    private ProyectailLogic _pl;
    private TowerSystem mejorar;

    public GameObject botonR1;
    public GameObject botonR2;

    public AudioClip[] sounds;

    private void Start()
    {
        _ts = FindObjectOfType<TowerSystem>();
        _lvlUpS = FindObjectOfType<LvlUpSystem>();
    }

    public void UpgradeR1()
    {
        AudioManager.Instance.PlaySound(this.gameObject, sounds[0]);
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
                        print("mas rango");
                        _lvlUpS.stats_tower.UpdateRange(_lvlUpS.stats_tower.range += 2);
                        break;

                    case TowerType.Boomerang:
                        print("mas velocidad ataque");
                        _lvlUpS.stats_tower.shootingSpeed -= 0.2f;
                        break;

                    case TowerType.Laser:
                        print("velocidad");
                        _lvlUpS.stats_tower.shootingSpeed -= 0.1f; //EL DAÑO DEL LASER FUNCIONA POR VELOCIDAD
                        break;

                }
                break;

            //---------MEJORA 2----------
            case 2:

                switch (_lvlUpS.stats_tower.Type)
                {
                    case TowerType.Cannon:

                        print("dispara mas rapido");
                        _lvlUpS.stats_tower.shootingSpeed -= 0.1f;
                        break;

                    case TowerType.Sniper:
                        print("danyo");
                        _lvlUpS.stats_tower.damage += 1;
                        break;

                    case TowerType.Boomerang:
                        print("mas rango");

                        _lvlUpS.stats_tower.UpdateRange(_lvlUpS.stats_tower.range += 1);
                        break;

                    case TowerType.Laser:
                        print("Rango");
                        _lvlUpS.stats_tower.GetComponent<LaserTowerScript>().UpdateRange(0.3f);
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
                        print("velocidad");
                        _lvlUpS.stats_tower.shootingSpeed -= 0.2f;
                        break;

                    case TowerType.Boomerang:
                        print("Camuflados");
                        _lvlUpS.stats_tower.seeCamuf = true;
                        break;

                    case TowerType.Laser:
                        print("danyo");
                        _lvlUpS.stats_tower.shootingSpeed -= 0.2f;
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
                        print("velocidad");
                        _lvlUpS.stats_tower.shootingSpeed -= 0.2f;
                        break;

                    case TowerType.Boomerang:
                        print("velocidad");
                        _lvlUpS.stats_tower.shootingSpeed -= 0.2f;
                        break;

                    case TowerType.Laser:
                        print("danyo");
                        _lvlUpS.stats_tower.shootingSpeed -= 0.3f;
                        break;
                }
                break;
        }


        UiManager.Instance.UpdatePoints();
    }

    public void UpgradeR2()
    {
        AudioManager.Instance.PlaySound(this.gameObject, sounds[0]);

        switch (_lvlUpS.stats_tower.idxR2)
        {

            //---------MEJORA 1----------
            case 1:
                switch (_lvlUpS.stats_tower.Type)
                {
                    case TowerType.Cannon:

                        _lvlUpS.stats_tower.UpdateRange(_lvlUpS.stats_tower.range += 1f);

                        print("More Range");
                        break;

                    case TowerType.Sniper:

                        print("More Damage");
                        _lvlUpS.stats_tower.damage += 1;

                        break;

                    case TowerType.Boomerang:
                        print("DAMAGE");
                        _lvlUpS.stats_tower.damage += 1;
                        break;

                    case TowerType.Laser:
                        print("range");
                        _lvlUpS.stats_tower.GetComponent<LaserTowerScript>().UpdateRange(0.2f);
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
                        _lvlUpS.stats_tower.seeCamuf = true;
                        break;

                    case TowerType.Boomerang:
                        print("range");
                        _lvlUpS.stats_tower.UpdateRange(_lvlUpS.stats_tower.range += 1);

                        break;

                    case TowerType.Laser:
                        print("danyo");
                        _lvlUpS.stats_tower.shootingSpeed -= 0.1f;
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
                        _lvlUpS.stats_tower.damage += 2;
                        _lvlUpS.stats_tower.shootingSpeed += 0.8f;
                        break;

                    case TowerType.Boomerang:
                        print("volocidad");
                        _lvlUpS.stats_tower.shootingSpeed -= 0.2f;
                        break;

                    case TowerType.Laser:
                        _lvlUpS.stats_tower.seeCamuf = true;
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

                        print("damage");

                        _lvlUpS.stats_tower.damage += 2;
                        break;

                    case TowerType.Boomerang:
                        print("danyo");
                        _lvlUpS.stats_tower.damage += 2;
                        break;

                    case TowerType.Laser:
                        print("muxo rango");
                        _lvlUpS.stats_tower.GetComponent<LaserTowerScript>().UpdateRange(0.5f);
                        break;
                }
                break;
        }
        UiManager.Instance.UpdatePoints();
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
            _lvlUpS.stats_tower.proyectail = towerScriptable.notFireDontDestroy;
        }
    }

    void NumCannon()
    {
        _lvlUpS.currentTower.transform.GetChild(0).gameObject.SetActive(false);
        _lvlUpS.currentTower.transform.GetChild(1).gameObject.SetActive(true);
    }

    
}
