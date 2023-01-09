using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerSystem : MonoBehaviour
{

    public float damage;
    public float speed;

    private bool hasToShoot;

    public int currentLvl;

    public GameObject proyectile;


    //Mejoras de la torre
    public GameObject[] upgrade;

    //ACESOS
    private LvlUpSystem _LvlSystem;



    public int currentUpgrade;


    // Intervalo de tiempo entre disparos (en segundos)
    private float intervaloDisparo = 0.5f;

    // Tiempo del próximo disparo permitido
    private float tiempoSiguienteDisparo = 0.0f;


    // Distancia máxima del rayo
    private float radius = 5f;

    // Resultado de la detección
    private RaycastHit2D hit;



    private void Awake()
    {
        _LvlSystem = FindObjectOfType<LvlUpSystem>();
    }


    void Start()
    {
        //STATS
        currentLvl = 0;

    }

    void Update()
    {
        //hasToShoot = false;

        // Lanzar rayo circular desde la posición de la torre
        hit = Physics2D.CircleCast(transform.position, radius, Vector2.zero);

        // Dibujar rayo circular en la escena
        Debug.DrawRay(transform.position, Vector2.right * radius, Color.red);
        Debug.DrawRay(transform.position, Vector2.up * radius, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * radius, Color.red);
        Debug.DrawRay(transform.position, Vector2.down * radius, Color.red);

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
            tiempoSiguienteDisparo = Time.time + intervaloDisparo;
        }
    }



void Disparar()
    {
        Instantiate(proyectile, transform.position, transform.rotation);
    }

    //SELECIONA LA TORRE
    private void OnMouseDown()
    {
        _LvlSystem.currentTower = this.gameObject;
        _LvlSystem.SelectTower();
    }
    


}
