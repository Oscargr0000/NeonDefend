using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBoomerang : MonoBehaviour
{
    public float proyectailSpeed;
    private int timeToDestroy = 5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfter(timeToDestroy));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * proyectailSpeed * Time.deltaTime);
    }

    // DESTROY AFTER TIME
    IEnumerator DestroyAfter(int timeleft)
    {
        yield return new WaitForSeconds(timeleft);
        Destroy(gameObject);
    }
}
