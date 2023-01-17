using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Cannon,
    Sniper,
    Boomerang,
    Laser
}
public class TowerSystem : MonoBehaviour
{
    //Select the tower the script is applied
    public TowerType Type;

    //-------------------------------CONNECTIONS--------------------------------
    public TowerScriptableObject scriptable_Stats;
    private GameManager _gm;
    private LvlUpSystem _LvlSystem;
    //___________________________________________________________________________


    //--------------------------------STATS--------------------------------
    public string Name;
    public int damage;
    public int price;
    public float shootingSpeed;
    
    public float range;

    public bool fireBullet;
    public bool seeCamuf;
    public bool useRayCast;
    public bool destroyBullet;

    public GameObject proyectail;
    //_____________________________________________________________________



    private bool hasToShoot;
    public int currentLvl;
    public LayerMask EnemyLayer;
    private Vector2 enemyPos;

    //Contadores de mejora
    public int idxR1;
    public int idxR2;

    //Precios para el sistema de compra
    public int priceR1;
    public int priceR2;

    //Maximo de mejora por rama
    public int maxR1 = 4;
    public int maxR2 = 4;

    // Tiempo del pr�ximo disparo permitido
    private float tiempoSiguienteDisparo = 0.0f;

    // Resultado de la detecci�n
    private RaycastHit2D hit;

    private RaycastHit2D shottingHit;


    private void Awake()
    {
        _LvlSystem = FindObjectOfType<LvlUpSystem>();
        _gm = FindObjectOfType<GameManager>();
    }


    void Start()
    {
        //-----------------STATS--------------------
        damage = scriptable_Stats.damage;
        shootingSpeed = scriptable_Stats.shootingSpeed;
        price = scriptable_Stats.price;

        range = scriptable_Stats.range;

        fireBullet = scriptable_Stats.fireBullet;
        seeCamuf = scriptable_Stats.seeCamuf;
        destroyBullet = scriptable_Stats.bulletIsDestroyed;
        useRayCast = scriptable_Stats.usesRaycast;

        proyectail = scriptable_Stats.notFireDestroy;     
        //---------------------------------------------
    }
    private void Update()
    {
        // Si el rayo colisiona con alg�n collider
        if (hit.collider != null)
        {
            // Acceder al objeto golpeado
            GameObject hitObject = hit.collider.gameObject;

            // Si el objeto tiene el tag "Enemy"
            if (hitObject.tag == "Enemy")
            {
                transform.up = hitObject.transform.position - transform.position;
                hasToShoot = true;
            }
            else
            {
                hasToShoot = false;
            }
        }


        // Comprobamos si es el momento de disparar
        if (hasToShoot.Equals(true) && Time.time >= tiempoSiguienteDisparo)
        {
            if (idxR2 != 4)
            {
                // Disparamos
                Disparar();
            }
            else //UPGRADED
            {
                DobleCannonShot();
            }


            // Actualizamos el tiempo del pr�ximo disparo permitido
            tiempoSiguienteDisparo = Time.time + shootingSpeed;
        }
    }

    void FixedUpdate()
    {
        // Lanzar rayo circular desde la posici�n de la torre
        hit = Physics2D.CircleCast(transform.position, range, Vector2.zero,EnemyLayer);

        // Dibujar rayo circular en la escena
        Debug.DrawRay(transform.position, Vector2.right * range, Color.red);
        Debug.DrawRay(transform.position, Vector2.up * range, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * range, Color.red);
        Debug.DrawRay(transform.position, Vector2.down * range, Color.red);
    }



    void Disparar()
    {
        if(!useRayCast)
        {
            // CAMBIAR AL METODO DE POOL PULLING
            Instantiate(proyectail, this.gameObject.transform.GetChild(0).transform.position, transform.rotation);
        }
        else
        {
            Color raycolor = Color.green;

            shottingHit = Physics2D.Raycast(this.transform.position, transform.up = hit.transform.position - transform.position, range,EnemyLayer);
            Debug.DrawRay(this.transform.position, transform.up = hit.transform.position - transform.position, raycolor, range);

            if (shottingHit.collider)
            {
                Debug.Log("EL FRANCOTIRADOR LE HA REVENTAO");
            }
        }
       
    }

    void DobleCannonShot()
    {
        Instantiate(proyectail, this.gameObject.transform.GetChild(1).GetChild(0).transform.position, transform.rotation);
        Instantiate(proyectail, this.gameObject.transform.GetChild(1).GetChild(1).transform.position, transform.rotation);
    }

    //SELECIONA LA TORRE
    private void OnMouseDown()
    {
        _LvlSystem.currentTower = this.gameObject;
        _LvlSystem.SelectTower();
    }
}
