using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IPoolInterface
{
    //IA
    public NavMeshAgent agente;
    public GameManager _gm;
    public Transform[] GoPoints;
    private int indicePoints;

    //STATS
    public int rewardEnemy;
    public int armor;
    private int maxArmor;
    private int minArmor = 1;

    private SpriteRenderer enemyRender;

    public Color[] ArmorColor;

    public Color fireColor;
    public Color cammoColor;

    public int speed;
    public int damage;

    public bool isFire;
    public bool isCammo;

    public AudioClip[] enemySounds;

    public ParticleSystem dieParticle;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //STATS
        agente.speed = 4;
        indicePoints = 1;

        // FIX MOVEMENT FOR 2D IA
        agente.updateRotation = false;
        agente.updateUpAxis = false;

        _gm = FindObjectOfType<GameManager>();
        
    }
    public void OnObjectSpawn()
    {
        enemyRender = this.GetComponent<SpriteRenderer>();
        maxArmor = SpawnManager.Instance.upgradedRounds +1;
        minArmor = SpawnManager.Instance.minArmor;

        indicePoints = 1;
        if (maxArmor >= 10)
        {
            maxArmor = 10;
        }

        armor = Random.Range(minArmor, maxArmor);

        UpdateArmor(false);



        //SELECT FIRE OR CAMMO

        if (SpawnManager.Instance.rounds < 12)
        {
            return;
        }

        
        int selectType = Random.Range(1, 40);

        switch (selectType)
        {
            case 1:
                isFire = true;
                isCammo = false;

                enemyRender.color = cammoColor;
                break;
            case 2:
                isCammo = true;
                isFire = false;

                enemyRender.color = fireColor;

                break;

            default:
                isFire = false;
                isCammo = false;
                
                break;
        }
    }

    private void Update()
    {
        //FIX THE POSITION
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
      
        agente.SetDestination(GoPoints[indicePoints].position);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //SYSTEM FOR THE MOVEMENT OF THE IA
        //When arrive to point change the objective to the next one
        if (collision.gameObject.CompareTag("GoPoints"))
        {
            if (indicePoints < GoPoints.Length - 1)
            {
                indicePoints++;
            }
            else if (indicePoints == GoPoints.Length - 1)
            {
                print("Ha llegado a su destino");
            }
        }
       
    }

    // Aplicate the color for the new armor and reset the enemy in the queue if the armor is lower than 0
    public void UpdateArmor(bool playsound)
    {
        UiManager.Instance.UpdatePoints();

        if (armor <= 0)
        {
            armor = 0;
        }

        enemyRender.color = ArmorColor[armor];

        if(armor> 0 && playsound)
        {
            AudioManager.Instance.PlaySound(this.gameObject, enemySounds[0]);
        }


        if (armor <= 0)
        {
            if (!ObjectPooler.Instance.poolDictionary["Enemy1"].Contains(this.gameObject))
            {
                Instantiate(dieParticle.gameObject, this.transform.position, Quaternion.identity);

                ObjectPooler.Instance.ReturnToQueue("Enemy1", this.gameObject);
                _gm.totalEnemyKill++;
            }
        }
    }
}
