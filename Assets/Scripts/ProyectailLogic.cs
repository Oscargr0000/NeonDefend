using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectailLogic : MonoBehaviour, IPoolInterface
{
    public float proyectailSpeed;
    private int timeToDestroy = 1;

    public int damage;

    public bool bulletIsFire;
    public bool notDestroy;
    public bool seeCamo;

    private GameManager _gm;

    private bool hasbeenDesactivated;

    public AudioClip[] sounds;


    public void OnObjectSpawn()
    {
        hasbeenDesactivated = false;
        StartCoroutine(DestroyAfter(timeToDestroy));
        
    }

    private void Start()
    {
        _gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        transform.Translate(Vector2.up * proyectailSpeed * Time.deltaTime);
    }


    // DESTROY AFTER TIME
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



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy hittedEnemy = collision.GetComponent<Enemy>();

            //Si el enemigo es de fuego y la bala no, no hace NADA
            if (hittedEnemy.isFire.Equals(true)&& !bulletIsFire)
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

            hittedEnemy.UpdateArmor(true);
        }
    }

    void EnemyHitted(int points)
    {
        _gm.points += points * damage;  //Cambiar en un futuro, acceder al daño de la bala y sumar esa cantidad de puntos

        if (notDestroy.Equals(false)) //Si la bala no tiene el booleano en true las balas atravesaran los enemigos
        {
            string name = gameObject.name;
            string a = "(Clone)";
            name = name.Replace(a, "");

            ObjectPooler.Instance.ReturnToQueue(name, gameObject);
            hasbeenDesactivated = true;
        }
    }
}
