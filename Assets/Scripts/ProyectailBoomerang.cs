using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectailBoomerang : MonoBehaviour
{
    public float proyectailSpeed;
    

    public int damage;

    public bool bulletIsFire;
    public bool notDestroy;
    public bool seeCamo;

    private GameManager _gm;

    public AudioClip[] sounds;


    private void Start()
    {
        _gm = FindObjectOfType<GameManager>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy hittedEnemy = collision.GetComponent<Enemy>();

            // IF THE ENEMY IS FIRE THE BULLET DOESNT DO NOTHING
            if (hittedEnemy.isFire.Equals(true) && !bulletIsFire)
            {
                if (notDestroy.Equals(false))
                {
                    string name = gameObject.name;
                    string a = "(Clone)";
                    name = name.Replace(a, "");

                    ObjectPooler.Instance.ReturnToQueue(name, gameObject);
                }

                return;
            }

            // IF THE ENEMY IS CAMMO THE BULLET DOESNT DO NOTHING

            if (hittedEnemy.isCammo.Equals(true) && !seeCamo)
            {
                if (notDestroy.Equals(false)) 
                {
                    string name = gameObject.name;
                    string a = "(Clone)";
                    name = name.Replace(a, "");

                    ObjectPooler.Instance.ReturnToQueue(name, gameObject);
                }

                return;
            }

            //UPDATE THE ENEMY

            EnemyHitted(collision.GetComponent<Enemy>().rewardEnemy);


            hittedEnemy.armor -= damage;
            if (hittedEnemy.armor <= 0)
            {
                hittedEnemy.armor = 0;
            }


            hittedEnemy.UpdateArmor(true);
        }
    }

    void EnemyHitted(int points)
    {
        _gm.points += points * damage;
    }
}
