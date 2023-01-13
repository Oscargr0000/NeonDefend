using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectailLogic : MonoBehaviour
{
    public float proyectailSpeed;
    private int timeToDestroy = 5;

    private GameManager _gm;
    private LvlUpSystem _lvl;

    private void Awake()
    {
        _gm = FindObjectOfType<GameManager>();
        _lvl = FindObjectOfType<LvlUpSystem>();
    }


    private void Start()
    {
        StartCoroutine(DestroyAfter(timeToDestroy));
    }
    void Update()
    {
        transform.Translate(Vector2.up * proyectailSpeed * Time.deltaTime);
    }

    IEnumerator DestroyAfter(int timeleft)
    {
        yield return new WaitForSeconds(timeleft);
        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _gm.points += 1;  //Cambiar en un futuro, acceder al da�o de la bala y sumar esa cantidad de puntos
            print(_gm.points);

            if (_lvl.stats_tower.destroyBullet.Equals(false))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
