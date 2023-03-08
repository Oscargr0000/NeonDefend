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

            // MAKE THE DAMAGE TO THE PLAYER BASED ON THE ARMOR
            _gm.playerHP -= enemyEnter.armor *2;
            

            // GAME OVER
            if(_gm.playerHP <= 0)
            {
                _gm.GameOver();
            }

            UiManager.Instance.HpUpdate();

            //DESACTIVATE THE ENEMY AND RETURN THE TO QUEUE
            if (!ObjectPooler.Instance.poolDictionary["Enemy1"].Contains(collision.gameObject))
            {
                ObjectPooler.Instance.ReturnToQueue("Enemy1", collision.gameObject);
            }
        }  
    }
}
