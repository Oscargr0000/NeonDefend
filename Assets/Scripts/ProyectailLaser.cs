using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectailLaser : MonoBehaviour
{
    private GameManager _gm;

    public int damage = 1;

    private void Start()
    {
        _gm = FindObjectOfType<GameManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !GetComponentInParent<LaserTowerScript>().isSettingUp)
        {
            Enemy hittedEnemy = collision.GetComponent<Enemy>();

            EnemyHitted(collision.GetComponent<Enemy>().rewardEnemy);

            StartCoroutine(MakeDamage(GetComponentInParent<LaserTowerScript>().shootingSpeed, hittedEnemy)); //MAKE DAMAGE

            //LIMIT DE ARMOR
            if (hittedEnemy.armor <= 0)
            {
                hittedEnemy.armor = 0;
            }

            //REFRESH ARMOR
            hittedEnemy.UpdateArmor();

            if (hittedEnemy.armor <= 0)
            {
                if (ObjectPooler.Instance.poolDictionary["Enemy1"].Contains(collision.gameObject))
                {
                    ObjectPooler.Instance.poolDictionary["Enemy1"].Enqueue(collision.gameObject);
                    collision.gameObject.SetActive(false);
                }
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy hittedEnemy = collision.GetComponent<Enemy>();

        StopCoroutine(MakeDamage(GetComponentInParent<LaserTowerScript>().shootingSpeed, hittedEnemy));

    }
    void EnemyHitted(int points)
    {
        _gm.points += points * damage;  //Cambiar en un futuro, acceder al daño de la bala y sumar esa cantidad de puntos
    }


    IEnumerator MakeDamage(float time, Enemy hitted)
    {
        hitted.armor -= damage;
        yield return new WaitForSeconds(time);
    }

}
