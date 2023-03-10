using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{

    #region Singleton
    public static SpawnManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion   

    private ObjectPooler _objPool;
    public NavMeshAgent enemyPref;
    private GameManager _gm;
    private TowerSystem _ts;
    public bool hasToSpawn;
    public float spawRate;

    public int rounds;
    public int upgradedRounds = 1;
    public int minArmor = 1;
    public int dificultLvl;
    public int waitForRounds;

    private int spawnedEnemys;
    private int enemyLimit;
    private bool hasBeenUpraded;

    //Delimitar en el inspector
    private bool isWaiting;


    public AudioClip[] sounds;

    private void Start()
    {
        _objPool = ObjectPooler.Instance;
        _gm = FindObjectOfType<GameManager>();

        //Al principio siempre apareceran 8 globos
        enemyLimit = 8;
        OnRoundsStart();

    }

    private void Update()
    {

        //Si ha spawneado todos los enemigos que tocan en esa rondas, deja de spawnear
        if (spawnedEnemys >= enemyLimit)
        {
            hasToSpawn = false;
        }


        //Detecta cuando se termina la ronda y da paso a la siguiente
        int EnemyLeft = FindObjectsOfType<Enemy>().Length;
        if(EnemyLeft <= 0)
        {
            if (!isWaiting)
            {
                isWaiting = true;
                //Reset del contador de enemigos spawneados por ronda
                spawnedEnemys = 0;

                //Particulas de pase de rondas

                StartCoroutine(WaitToStart(waitForRounds));
            }
           
        }
    }

    void lvlDificultUp(int rondas)
    {
        if(rounds % rondas == 0 && hasBeenUpraded.Equals(false))
        {
            //Al aparecer el enemigo se seleccionara entre el 0 y el numero que constituya el "upgradedRounds"
                upgradedRounds++;

                //Aumenta la cantidad de globos por ronda o no
                enemyLimit = enemyLimit += Random.Range(1, 4);
                spawRate -= 0.05f;
                enemyPref.speed += 0.2f;
            hasBeenUpraded = true;
        }
    }

    IEnumerator WaitToStart(int timeToWait)
    {

        yield return new WaitForSeconds(timeToWait);

        OnRoundsStart();
    }

    void OnRoundsStart()
    {
        rounds++;
        if(rounds > 30)
        {
            PlayerPrefs.SetInt("Rounds", rounds - 1);
            SceneManager.LoadScene(2);
            
            return;
        }

        AudioManager.Instance.PlaySound(this.gameObject, sounds[0]);
        UiManager.Instance.UpdateRounds();
        UiManager.Instance.UpdatePoints();

        _gm.points += 50;

        // REMOVE THE POSIBILITY TO SPAWN WEAK ENEMYS
        if (rounds % 7 == 0 && hasBeenUpraded.Equals(false))
        {
            minArmor++;
        }

        //Aumenta la dificultad de los enemigos
        lvlDificultUp(3 * upgradedRounds);

        


        hasToSpawn = true;
        StartCoroutine(EnemySpawnTime(spawRate));
        hasBeenUpraded = false;
        isWaiting = false;


    }

    //ACCEDO A LA POOL Y SPAWNEO UN ENEMIGO
    IEnumerator EnemySpawnTime(float spawnTime)
    {
        while (hasToSpawn.Equals(true))
        {
            if (_objPool.poolDictionary["Enemy1"].Count.Equals(0))
            {
                _objPool.AddObject("Enemy1", enemyPref.gameObject);
            }

            _objPool.SpawnFromPool("Enemy1", transform.transform.position, Quaternion.identity);
            spawnedEnemys++;

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
