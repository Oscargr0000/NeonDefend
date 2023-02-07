using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Cannon,
    Sniper,
    Boomerang,
    Laser
}

public class TowerSystem : MonoBehaviour
{
    //Select the tower the script is applied
    public TowerType Type;

    #region Singleton
    public static TowerSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion  

    //-------------------------------CONNECTIONS--------------------------------
    public TowerScriptableObject scriptable_Stats;
    private GameManager _gm;
    
    private Enemy _enemyS;
    private ObjectPooler _objPool;

    private CircleCollider2D TowerCollider;
    //___________________________________________________________________________


    //--------------------------------STATS--------------------------------
    public string Name;
    public int damage;
    public int price;
    public float shootingSpeed;
    
    public float range;

    public bool fireBullet;
    public bool seeCamuf;
    public bool useRayCast;
    public bool destroyBullet;

    public GameObject proyectail;
    //_____________________________________________________________________



    private bool hasToShoot;
    public int currentLvl;
    public LayerMask EnemyLayer;
    private Vector2 enemyPos;

    //Contadores de mejora
    public int idxR1;
    public int idxR2;

    //Precios para el sistema de compra
    public int priceR1;
    public int priceR2;

    //Maximo de mejora por rama
    public int maxR1 = 4;
    public int maxR2 = 4;

    // Tiempo del próximo disparo permitido
    private float tiempoSiguienteDisparo = 0.0f;

    // Resultado de la detección
    private RaycastHit2D hit;

    private RaycastHit2D shottingHit;

    public bool notShoot;



    public Vector3 toGo;


    //Deteccion del globo
    public Queue<GameObject> EnemyQueue = new Queue<GameObject>();
    private GameObject currentTarget;



    void Start()
    {
        hasToShoot = false;
       
        _gm = FindObjectOfType<GameManager>();
        _enemyS = FindObjectOfType<Enemy>();
        _objPool = ObjectPooler.Instance;
        TowerCollider = this.GetComponent<CircleCollider2D>();
        

        //-----------------STATS--------------------
        damage = scriptable_Stats.damage;
        shootingSpeed = scriptable_Stats.shootingSpeed;
        price = scriptable_Stats.price;

        range = scriptable_Stats.range;

        fireBullet = scriptable_Stats.fireBullet;
        seeCamuf = scriptable_Stats.seeCamuf;
        destroyBullet = scriptable_Stats.bulletIsDestroyed;
        useRayCast = scriptable_Stats.usesRaycast;

        proyectail = scriptable_Stats.notFireDestroy;
        //---------------------------------------------

        UpdateRange(range);
    }


    private void Update()
    {
        if (notShoot)
        {
            return;
        }

        CheckForTargets();


        // Comprobamos si es el momento de disparar
        if (hasToShoot.Equals(true) && Time.time >= tiempoSiguienteDisparo)
        {
            if (idxR2 != 4)
            {
                // Disparamos
                Disparar();
            }
            else //UPGRADED
            {
                DobleCannonShot();
            }


            // Actualizamos el tiempo del próximo disparo permitido
            tiempoSiguienteDisparo = Time.time + shootingSpeed;
        }
    }


   

    void FixedUpdate()
    {
        // Lanzar rayo circular desde la posición de la torre
        hit = Physics2D.CircleCast(transform.position, range, Vector2.zero,EnemyLayer);

        // Dibujar rayo circular en la escena
        Debug.DrawRay(transform.position, Vector2.right * range, Color.red);
        Debug.DrawRay(transform.position, Vector2.up * range, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * range, Color.red);
        Debug.DrawRay(transform.position, Vector2.down * range, Color.red);   
    }



    void Disparar()
    {
        if(!useRayCast)
        {
            // CAMBIAR AL METODO DE POOL PULLING
            GameObject proyectailToSpawn = proyectail;
            if (Type.Equals(TowerType.Cannon))
            {
                proyectailToSpawn.GetComponent<ProyectailLogic>().damage = damage;
            }
            else if (Type.Equals(TowerType.Boomerang))
            {
                proyectailToSpawn.transform.GetChild(0).transform.GetChild(0).GetComponent<ProyectailBoomerang>().damage = damage;
            }
            
            Instantiate(proyectailToSpawn, this.gameObject.transform.GetChild(0).transform.position, transform.rotation);
        }
        else
        {
            //Lanza el rayo a la posicion del enemigo mas alto en la cola
            Color raycolor = Color.green;
            Vector2 ShotDirection = transform.up = currentTarget.transform.position - transform.position;

            shottingHit = Physics2D.Raycast(this.transform.position, ShotDirection , range, EnemyLayer);
            Debug.DrawRay(this.transform.position, ShotDirection, raycolor, range);

            if (shottingHit.collider)
            {
                if (shottingHit.collider.gameObject.CompareTag("Enemy"))
                {
                    //Comprueba que exista el gameobject en la cola y desactiva y devuelve a la cola
                    if (_objPool.poolDictionary["Enemy1"].Contains(shottingHit.collider.gameObject))
                    {
                        _gm.points += shottingHit.collider.GetComponent<Enemy>().rewardEnemy;

                        //Reset del Enemy
                        shottingHit.collider.gameObject.SetActive(false);
                        _objPool.poolDictionary["Enemy1"].Enqueue(shottingHit.collider.gameObject);

                        print("Le ha dado");
                    }
                }
                Debug.Log("EL FRANCOTIRADOR LE HA REVENTAO");
            }
        }
       
    }

    void CheckForTargets()
    {
        if (!useRayCast)
        {
            if (EnemyQueue.Count > 0)
            {

                currentTarget = EnemyQueue.Peek();
                
                //ROTACION PARA MIRAR AL ENEMIGO
                transform.up = new Vector3(currentTarget.transform.position.x - transform.position.x, currentTarget.transform.position.y - transform.position.y,0);



                // COMIENZA A DISPARAR CUANDO 
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject == currentTarget)
                    {
                        hasToShoot = true;
                    }
                }
            }
            else
            {
                hasToShoot = false;
            }
        }
        else if(useRayCast)
        {
            if(_objPool.poolDictionary["Enemy1"].Count > 0)
            {
                currentTarget = _objPool.poolDictionary["Enemy1"].Peek();

                transform.up = currentTarget.transform.position - transform.position;

                //CUANDO A DE DISPARAR (CAMBIAR CUANDO COMIENCE EL SISTEMA DE RONDAS)
                hasToShoot = true;
            }else
            {
                hasToShoot = false;
            }

        }
     }

    void DobleCannonShot()
    {
        Instantiate(proyectail, this.gameObject.transform.GetChild(1).GetChild(0).transform.position, transform.rotation);
        Instantiate(proyectail, this.gameObject.transform.GetChild(1).GetChild(1).transform.position, transform.rotation);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (notShoot)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyQueue.Enqueue(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (notShoot)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyQueue.Dequeue();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (notShoot)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            hasToShoot = true;
        }
        else
        {
            hasToShoot = false;
        }
    }

    public void UpdateRange(float newRange)
    {
        TowerCollider.radius = newRange;
    }
}
