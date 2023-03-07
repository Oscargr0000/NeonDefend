using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectailLaser : MonoBehaviour
{
    private GameManager _gm;

    public int damage = 1;

    public AudioClip[] sounds;

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

            StartCoroutine(MakeDamage(GetComponentInParent<TowerSystem>().shootingSpeed, hittedEnemy)); //MAKE DAMAGE

            //LIMIT DE ARMOR
            if (hittedEnemy.armor <= 0)
            {
                hittedEnemy.armor = 0;
            }

            //REFRESH ARMOR
            hittedEnemy.UpdateArmor(false);

            // PLAY SOUND OF HIT
            if (hittedEnemy.armor > 0)
            {
                AudioManager.Instance.PlaySound(this.gameObject, sounds[0]);
            }
            else
            {
                AudioManager.Instance.PlaySound(this.gameObject, sounds[1]);
            }

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

        StopAllCoroutines();

    }
    void EnemyHitted(int points)
    {
        _gm.points += points * damage;  //Cambiar en un futuro, acceder al daño de la bala y sumar esa cantidad de puntos
    }


    IEnumerator MakeDamage(float time, Enemy hitted)
    {
        hitted.armor -= damage;

        hitted.UpdateArmor(false);

        yield return new WaitForSeconds(time);
        StartCoroutine(MakeDamage(time, hitted));
    }

}
