using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectailLogic : MonoBehaviour
{
    public float proyectailSpeed;
    private int timeToDestroy = 5;

    public int damage;

    public bool bulletIsFire;
    public bool notDestroy;

    private GameManager _gm;
    private LvlUpSystem _lvl;
    private Enemy _enemyS;
    private ObjectPooler _objP;


    private void Start()
    {
        _gm = FindObjectOfType<GameManager>();
        _lvl = FindObjectOfType<LvlUpSystem>();
        _objP = ObjectPooler.Instance;
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
            Enemy hittedEnemy = collision.GetComponent<Enemy>();

            EnemyHitted(collision.GetComponent<Enemy>().rewardEnemy);

            
            hittedEnemy.armor -= damage;
            hittedEnemy.UpdateArmor();

            if(hittedEnemy.armor <= 0)
            {
                if (_objP.poolDictionary["Enemy1"].Contains(collision.gameObject))
                {
                    collision.gameObject.SetActive(false);
                    _objP.poolDictionary["Enemy1"].Enqueue(collision.gameObject);
                }
            }
        }

        if (collision.gameObject.CompareTag("FireEnemy"))
        {
            if (bulletIsFire.Equals(true))
            {

                EnemyHitted(collision.GetComponent<Enemy>().rewardEnemy);
            }
        }
    }

    void EnemyHitted(int points)
    {
        _gm.points += points * damage;  //Cambiar en un futuro, acceder al da�o de la bala y sumar esa cantidad de puntos



        if (notDestroy.Equals(false)) //Si la bala no tiene el booleano en true las balas atravesaran los enemigos
        {
            Destroy(this.gameObject);
        }
    }
}
