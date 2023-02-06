using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectailBoomerang : MonoBehaviour
{
    public float proyectailSpeed;
    

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
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy hittedEnemy = collision.GetComponent<Enemy>();

            EnemyHitted(collision.GetComponent<Enemy>().rewardEnemy);


            hittedEnemy.armor -= damage;
            if (hittedEnemy.armor <= 0)
            {
                hittedEnemy.armor = 0;
            }

            hittedEnemy.UpdateArmor();

            if (hittedEnemy.armor <= 0)
            {
                if (_objP.poolDictionary["Enemy1"].Contains(collision.gameObject))
                {
                    _objP.poolDictionary["Enemy1"].Enqueue(collision.gameObject);
                    collision.gameObject.SetActive(false);
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
        _gm.points += points * damage;  //Cambiar en un futuro, acceder al daño de la bala y sumar esa cantidad de puntos


        if (notDestroy.Equals(false)) //Si la bala no tiene el booleano en true las balas atravesaran los enemigos
        {
            Destroy(this.gameObject);
        }
    }
}
