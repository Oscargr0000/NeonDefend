using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectailBoomerang : MonoBehaviour, IPoolInterface
{
    public float proyectailSpeed;
    

    public int damage;

    public bool bulletIsFire;
    public bool notDestroy;
    public bool seeCamo;

    private GameManager _gm;
    private LvlUpSystem _lvl;
    private Enemy _enemyS;
    private ObjectPooler _objP;

    private bool hasbeenDesactivated;

    public AudioClip[] sounds;

    public void OnObjectSpawn()
    {
        hasbeenDesactivated = false;
        //StartCoroutine(DestroyAfter(1));
        
    }

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

            //Si el enemigo es de fuego y la bala no, no hace NADA
            if (hittedEnemy.isFire.Equals(true) && !bulletIsFire)
            {
                if (notDestroy.Equals(false)) //Si la bala no tiene el booleano en true las balas atravesaran los enemigos
                {
                    string name = gameObject.name;
                    string a = "(Clone)";
                    name = name.Replace(a, "");

                    ObjectPooler.Instance.ReturnToQueue(name, gameObject);
                    hasbeenDesactivated = true;
                }

                return;
            }

            //Si el enemigo es de camuflaje y la bala no, no hace NADA
            if (hittedEnemy.isCammo.Equals(true) && !seeCamo)
            {
                if (notDestroy.Equals(false)) //Si la bala no tiene el booleano en true las balas atravesaran los enemigos
                {
                    string name = gameObject.name;
                    string a = "(Clone)";
                    name = name.Replace(a, "");

                    ObjectPooler.Instance.ReturnToQueue(name, gameObject);
                    hasbeenDesactivated = true;
                }

                return;
            }


            EnemyHitted(collision.GetComponent<Enemy>().rewardEnemy);


            hittedEnemy.armor -= damage;
            if (hittedEnemy.armor <= 0)
            {
                hittedEnemy.armor = 0;
            }


            hittedEnemy.UpdateArmor(true);
        }
    }

    IEnumerator DestroyAfter(int timeleft)
    {
        if (hasbeenDesactivated.Equals(false))
        {
            yield return new WaitForSeconds(timeleft);
            string name = gameObject.name;
            string a = "(Clone)";
            name = name.Replace(a, "");

            ObjectPooler.Instance.ReturnToQueue(name, gameObject);
            hasbeenDesactivated = true;
        }       
    }

    void EnemyHitted(int points)
    {
        _gm.points += points * damage;  //Cambiar en un futuro, acceder al daño de la bala y sumar esa cantidad de puntos
    }
}
