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

            _gm.playerHP -= enemyEnter.armor;
            print(_gm.playerHP);
            
            if(_gm.playerHP <= 0)
            {

            }

            //Lo desactiva y lo envia a la cola
            if (!ObjectPooler.Instance.poolDictionary["Enemy1"].Contains(collision.gameObject))
            {
                ObjectPooler.Instance.ReturnToQueue("Enemy1", collision.gameObject);
            }
        }  
    }
}
