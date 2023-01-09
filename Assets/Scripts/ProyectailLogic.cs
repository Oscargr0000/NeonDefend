using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectailLogic : MonoBehaviour
{
    public float proyectailSpeed;
    private int timeToDestroy = 5;


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
}
