using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTowerScript : MonoBehaviour
{ //Select the tower the script is applied
    public TowerType Type;  

    //-------------------------------CONNECTIONS--------------------------------
    public TowerScriptableObject scriptable_Stats;
    private GameManager _gm;

    private Enemy _enemyS;
    private ObjectPooler _objPool;

    private CircleCollider2D TowerCollider;
    //___________________________________________________________________________


    //--------------------------------STATS--------------------------------
    public string Name;
    public int damage;
    public int price;
    public float shootingSpeed;

    public float range;

    public bool fireBullet;
    public bool seeCamuf;

    //_____________________________________________________________________



    public int currentLvl;
    public LayerMask EnemyLayer;

    //Contadores de mejora
    public int idxR1;
    public int idxR2;

    //Precios para el sistema de compra
    public int priceR1;
    public int priceR2;

    //Maximo de mejora por rama
    public int maxR1 = 4;
    public int maxR2 = 4;


    private bool isSettingUp;



    void Start()
    {


        _gm = FindObjectOfType<GameManager>();
        _enemyS = FindObjectOfType<Enemy>();
        _objPool = ObjectPooler.Instance;
        TowerCollider = this.GetComponent<CircleCollider2D>();


        //-----------------STATS--------------------
        damage = scriptable_Stats.damage;
        shootingSpeed = scriptable_Stats.shootingSpeed;
        price = scriptable_Stats.price;

        range = scriptable_Stats.range;

        fireBullet = scriptable_Stats.fireBullet;
        seeCamuf = scriptable_Stats.seeCamuf;


        //---------------------------------------------

        isSettingUp = true;
        UpdateRange(range);

    }


    private void Update()
    {
        if (isSettingUp.Equals(true))
        {
            //Acede a la posicion del mouse cuando hace click

            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                this.transform.rotation = Quaternion.AngleAxis(angle,Vector3.back);
                isSettingUp = false;
            }
        }
    }

   

    public void UpdateRange(float newRange)
    {
        TowerCollider.radius = newRange;
    }

    IEnumerator DoDamage(float time)
    {

        yield return new WaitForSeconds(time);
    }
}
