using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int rewardEnemy;
    public int armor;
    private int maxArmor = 9;

    private SpriteRenderer enemyRender;

    public Color[] ArmorColor;

    public int speed;
    public int damage;

    public bool isFire;
    public bool isCammo;

    

    private void Start()
    {
        enemyRender = this.GetComponent<SpriteRenderer>();
    }

    void UpdateArmor()
    {
        enemyRender.color = ArmorColor[armor];
    }
}
