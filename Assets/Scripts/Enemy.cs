using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolInterface
{
    public int rewardEnemy;
    public int armor;
    private int maxArmor = 10;

    private SpriteRenderer enemyRender;

    public Color[] ArmorColor;

    public int speed;
    public int damage;

    public bool isFire;
    public bool isCammo;

    

    public void OnObjectSpawn()
    {
        enemyRender = this.GetComponent<SpriteRenderer>();
        //BORAR
        armor = Random.Range(1, maxArmor);
        //BORAR
        UpdateArmor();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * 3 * Time.deltaTime);
    }


    public void UpdateArmor()
    {
        enemyRender.color = ArmorColor[armor];
    }
}
