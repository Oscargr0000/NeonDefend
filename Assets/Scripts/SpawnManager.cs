using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private ObjectPooler _objPool;
    public bool hasToSpawn;
    public float spawRate;

    public int rounds;
    public int dificultLvl;

    private void Update()
    {
        int roundstoUpgrade;

        

        lvlDificultUp(3);
    }

    private void Start()
    {
        hasToSpawn = true;
        _objPool = ObjectPooler.Instance;
        StartCoroutine(EnemySpawnTime(spawRate));
    }

    IEnumerator EnemySpawnTime(float spawnTime)
    {
        while(hasToSpawn.Equals(true))
        {
            _objPool.SpawnFromPool("Enemy1", transform.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(spawnTime);
        }
    }

    void lvlDificultUp(int rondas)
    {
        if(rounds % rounds == 0)
        {

        }
    }

}
