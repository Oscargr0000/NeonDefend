using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerSystem : MonoBehaviour
{
    //-------------------------------CONNECTIONS--------------------------------
    public TowerScriptableObject scriptable_Stats;
    private GameManager _gm;
    private LvlUpSystem _LvlSystem;
    //___________________________________________________________________________




    //--------------------------------STATS--------------------------------
    public int damage;
    public float shootingSpeed;

    public float range;

    public bool fireBullet;
    public bool seeCamuf;
    public bool destroyBullet;

    public GameObject proyectail;
    //_____________________________________________________________________



    private bool hasToShoot;
    public int currentLvl;

    //Contadores de mejora
    public int idxR1;
    public int idxR2;

    //Precios para el sistema de compra
    public int priceR1;
    public int priceR2;

    //Maximo de mejora por rama
    public int maxR1 = 4;
    public int maxR2 = 4;

    // Tiempo del próximo disparo permitido
    private float tiempoSiguienteDisparo = 0.0f;

    // Resultado de la detección
    private RaycastHit2D hit;



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

        range = scriptable_Stats.range;

        fireBullet = scriptable_Stats.fireBullet;
        seeCamuf = scriptable_Stats.seeCamuf;
        destroyBullet = scriptable_Stats.bulletIsDestroyed;
        proyectail = scriptable_Stats.notFireDestroy;
    //---------------------------------------------
}

    void Update()
    {
        // Lanzar rayo circular desde la posición de la torre
        hit = Physics2D.CircleCast(transform.position, range, Vector2.zero);

        // Dibujar rayo circular en la escena
        Debug.DrawRay(transform.position, Vector2.right * range, Color.red);
        Debug.DrawRay(transform.position, Vector2.up * range, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * range, Color.red);
        Debug.DrawRay(transform.position, Vector2.down * range, Color.red);

        // Si el rayo colisiona con algún collider
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
            // Disparamos
            Disparar();

            // Actualizamos el tiempo del próximo disparo permitido
            tiempoSiguienteDisparo = Time.time + shootingSpeed;
        }
    }



    void Disparar()
    {
        // CAMBIAR AL METODO DE POOL PULLING
        Instantiate(proyectail, transform.position, transform.rotation);
    }

    //SELECIONA LA TORRE
    private void OnMouseDown()
    {
        _LvlSystem.currentTower = this.gameObject;
        _LvlSystem.SelectTower();
    }
}
