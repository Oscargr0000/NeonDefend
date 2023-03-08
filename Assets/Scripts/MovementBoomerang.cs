using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBoomerang : MonoBehaviour, IPoolInterface
{
    public float proyectailSpeed;
    private int timeToDestroy = 5;

    public void OnObjectSpawn()
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
            string name = gameObject.name;
            string a = "(Clone)";
            name = name.Replace(a, "");

            ObjectPooler.Instance.ReturnToQueue(name, gameObject);        
    }
}
