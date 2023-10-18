using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [System.Serializable]
    public struct Pool
    {
        public int type;
        public GameObject prefab;
        public int size;
    }
    public Transform spawnPos, hobSpanPos, trollSpanPos, castlePos;
    public List<Pool> pools;
    public Dictionary<int, Queue<GameObject>> poolDict;
    [SerializeField] GameObject troll, hobGoblin;
    private void Awake()
    {
        poolDict = new Dictionary<int, Queue<GameObject>>();
        foreach(var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i = 0; i< pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(spawnPos, false);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDict.Add(pool.type, objectPool);
        }
    }
    public GameObject SpawnFromPool(int type)
    {
        if (!poolDict.ContainsKey(type))
            return null;

        GameObject obj = poolDict[type].Dequeue();
        poolDict[type].Enqueue(obj);

        return obj;
    }
    void CreateEpicMob(GameObject mob)
    {
        GameObject obj = Instantiate(mob);

    }
}
