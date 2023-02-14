using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int points;
    public int rounds;
    public int maxRounds;

    public int playerHP;

    // Start is called before the first frame update
    void Start()
    {
        points = 10000000;
        playerHP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
