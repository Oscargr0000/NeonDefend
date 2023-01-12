using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerSystem : MonoBehaviour
{

    public TowerScriptableObject scriptable_Stats;

    //--------------------------------STATS--------------------------------
    public int damage;
    public float shootingSpeed;

    public float range;

    public bool fireBullet;
    public bool seeCamuf;
    //---------------------------------------------------------------------

    private bool hasToShoot;
    public int currentLvl;

    public int idxR1;
    public int idxR2;

    public int maxR1 = 4;
    public int maxR2 = 4;


    //ACESOS
    private LvlUpSystem _LvlSystem;


    // Intervalo de tiempo entre disparos (en segundos)
    private float intervaloDisparo = 0.5f;

    // Tiempo del pr�ximo disparo permitido
    private float tiempoSiguienteDisparo = 0.0f;

    // Resultado de la detecci�n
    private RaycastHit2D hit;



    private void Awake()
    {
        _LvlSystem = FindObjectOfType<LvlUpSystem>();
    }


    void Start()
    {
        //-----------------STATS--------------------
        damage = scriptable_Stats.damage;
        shootingSpeed = scriptable_Stats.shootingSpeed;

        range = scriptable_Stats.range;

        fireBullet = scriptable_Stats.fireBullet;
        seeCamuf = scriptable_Stats.seeCamuf;
        //---------------------------------------------
}

    void Update()
    {
        // Lanzar rayo circular desde la posici�n de la torre
        hit = Physics2D.CircleCast(transform.position, range, Vector2.zero);

        // Dibujar rayo circular en la escena
        Debug.DrawRay(transform.position, Vector2.right * range, Color.red);
        Debug.DrawRay(transform.position, Vector2.up * range, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * range, Color.red);
        Debug.DrawRay(transform.position, Vector2.down * range, Color.red);

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
            // Disparamos
            Disparar();

            // Actualizamos el tiempo del pr�ximo disparo permitido
            tiempoSiguienteDisparo = Time.time + shootingSpeed;
        }
    }



    void Disparar()
    {
        Instantiate(scriptable_Stats.MainBullet, transform.position, transform.rotation);
    }

    //SELECIONA LA TORRE
    private void OnMouseDown()
    {
        _LvlSystem.currentTower = this.gameObject;
        _LvlSystem.SelectTower();
    }
}
