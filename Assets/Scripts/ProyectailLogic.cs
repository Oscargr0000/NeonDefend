using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectailLogic : MonoBehaviour
{
    public float proyectailSpeed;
    private int timeToDestroy = 5;

    public bool bulletIsFire;
    public bool notDestroy;

    private GameManager _gm;
    private LvlUpSystem _lvl;

    private void Awake()
    {
        _gm = FindObjectOfType<GameManager>();
        _lvl = FindObjectOfType<LvlUpSystem>();
    }


    private void Start()
    {
        StartCoroutine(DestroyAfter(timeToDestroy));
    }

    void Update()
    {
        transform.Translate(Vector2.up * proyectailSpeed * Time.deltaTime);
    }


    // DESTROY AFTER TIME
    IEnumerator DestroyAfter(int timeleft)
    {
        yield return new WaitForSeconds(timeleft);
        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHitted(1);
        }

        if (collision.gameObject.CompareTag("FireEnemy"))
        {
            if (bulletIsFire.Equals(true))
            {
                EnemyHitted(2);
            }
        }
    }

    void EnemyHitted(int points)
    {
        _gm.points += points;  //Cambiar en un futuro, acceder al daño de la bala y sumar esa cantidad de puntos

        if (notDestroy.Equals(false)) //Si la bala no tiene el booleano en true las balas atravesaran los enemigos
        {
            Destroy(this.gameObject);
        }
    }
}
