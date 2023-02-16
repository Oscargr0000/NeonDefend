using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]

    //CONFORMA LAS PISCINAS DONDE E INDICA LOS OBJETOS QUE SE ENCUENTRAN EN ELLA
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }


    //PERMITE EL ACCESO DESDE A OTROS SCRIPTS
    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion   


    //
    public List<Pool> pools;

    //EL DIRECTORIO QUE PERMITE ACCEDER A LAS POOLS
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    //CUANDO COMIENZA LA PARTIDA SE INSTANCIAN TODOS LOS OBJETOS DE LAS POOLS
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj =Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }


    public GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        //Si no existe ninguna pool con ese tag salta un error
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tah" + tag + " doesn't exist");
            return null;
        }

        //Accede al primer de la lista para spawnear y lo activa
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);

        //Accede a la posicion donde debe spawnear
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPoolInterface pooledObj = objectToSpawn.GetComponent<IPoolInterface>();

        if(pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        return objectToSpawn;
    }

    public void ReturnToQueue(string dictionaryTag, GameObject objectdestroyet)
    {
        poolDictionary[dictionaryTag].Enqueue(objectdestroyet);
        objectdestroyet.SetActive(false);
    }
}
