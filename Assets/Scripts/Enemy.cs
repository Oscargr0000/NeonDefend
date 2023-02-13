using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IPoolInterface
{
    //IA
    private NavMeshAgent agente;
    public Transform[] GoPoints;
    private int indicePoints;



    public int rewardEnemy;
    public int armor;
    private int maxArmor;

    private SpriteRenderer enemyRender;

    public Color[] ArmorColor;

    public int speed;
    public int damage;

    public bool isFire;
    public bool isCammo;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        indicePoints = 1;
        agente.updateRotation = false;
        agente.updateUpAxis = false;
    }
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
        //Seguro de posicion
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

      
        agente.SetDestination(GoPoints[indicePoints].position);
        //transform.Translate(Vector2.up * 3 * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoPoints"))
        {
            if (indicePoints < GoPoints.Length - 1)
            {
                indicePoints++;
            }
            else if (indicePoints == GoPoints.Length - 1)
            {
                print("Ha llegado a su destino");
                //Desactiva el enemgio y hace danyo al jugador
            }
        }
       
    }


    public void UpdateArmor()
    {
        enemyRender.color = ArmorColor[armor];

        if (armor <= 0)
        {
            if (!ObjectPooler.Instance.poolDictionary["Enemy1"].Contains(this.gameObject))
            {
                ObjectPooler.Instance.ReturnToQueue("Enemy1", this.gameObject);
            }
        }
    }
}
