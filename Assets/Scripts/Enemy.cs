using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolInterface
{
    public int rewardEnemy;
    public int armor;
    private int maxArmor;

    private SpriteRenderer enemyRender;

    public Color[] ArmorColor;

    public int speed;
    public int damage;

    public bool isFire;
    public bool isCammo;


    public void OnObjectSpawn()
    {
        maxArmor = SpawnManager.Instance.upgradedRounds + 1;
        if (maxArmor >= 10)
        {
            maxArmor = 10;
        }

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

        if (armor <= 0)
        {
            if (ObjectPooler.Instance.poolDictionary["Enemy1"].Contains(this.gameObject))
            {
                ObjectPooler.Instance.poolDictionary["Enemy1"].Enqueue(this.gameObject);
                this.gameObject.SetActive(false);
            }
            else
            {
                return;
            }
        }
    }
}
