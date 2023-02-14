using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrailScript : MonoBehaviour
{
    private GameManager _gm;

    private void Start()
    {
        _gm = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyEnter = collision.GetComponent<Enemy>();
            int indxDmg = 1;

            if (enemyEnter.armor % 2 == 0)
            {
                indxDmg++;
            }
            

            print(indxDmg);
            _gm.playerHP -= enemyEnter.armor * indxDmg;
            

            //Lo desactiva y lo envia a la cola
            if (!ObjectPooler.Instance.poolDictionary["Enemy1"].Contains(collision.gameObject))
            {
                ObjectPooler.Instance.ReturnToQueue("Enemy1", collision.gameObject);
            }
        }  
    }
}
