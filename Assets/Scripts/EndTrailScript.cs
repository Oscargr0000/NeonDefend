using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrailScript : MonoBehaviour
{
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
            AudioManager.Instance.PlaySound(this.gameObject, sounds[0]);
            Enemy enemyEnter = collision.GetComponent<Enemy>();

            _gm.playerHP -= enemyEnter.armor *2;
            
            if(_gm.playerHP <= 0)
            {
                _gm.GameOver();
            }

            UiManager.Instance.HpUpdate();
            //Lo desactiva y lo envia a la cola
            if (!ObjectPooler.Instance.poolDictionary["Enemy1"].Contains(collision.gameObject))
            {
                ObjectPooler.Instance.ReturnToQueue("Enemy1", collision.gameObject);
            }
        }  
    }
}
