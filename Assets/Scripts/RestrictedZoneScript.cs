using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictedZoneScript : MonoBehaviour
{

    public bool isOnZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            isOnZone = true;
            print(isOnZone);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            isOnZone = false;
            print(isOnZone);
        }
    }
}
