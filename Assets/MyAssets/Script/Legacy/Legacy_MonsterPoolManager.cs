using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ======================================
//              Legacy Script
// ======================================

/*
// =================== MONSTER POOL MANAGER CLASS (Singleton) ===========================
// 몬스터 오브젝트를 풀링 하는 클래스
// =====================================================================================
public enum MONSTER_POOL
{
    // Normal Monster: 0 ~ 9999
    NORMAL_SKELETON_SWORDMAN = 0,
    NORMAL_SKELETON_ARCHER = 1,

    // Field Boss Monster: 10000 ~ 19999
    FIELD_BOSS_SKELETON_KNIGHT = 10000,

    // Raid Boss Monster: 20000 ~ 29999
    RAID_BOSS_BLACK_DRAGON = 20000,
    SIZE
}
public class MonsterPoolManager
{
    [System.Serializable]
    public class MonsterPool
    {
        public MONSTER_POOL key;
        public GameObject value;
        public int amount;
        public Queue<GameObject> queue = new Queue<GameObject>();

        public void Initialize()
        {
            for (int i = 0; i < amount; ++i)
            {
                GameObject poolObject = Instantiate(value, Instance.transform);
                poolObject.SetActive(false);
                queue.Enqueue(poolObject);
            }
        }
    }
    private Dictionary<MONSTER_POOL, MonsterPool> monsterPoolDictionary;
    [SerializeField] private MonsterPool[] monsterPoolArray;

    private void Start()
    {
        MonsterPoolDictionary = new Dictionary<MONSTER_POOL, MonsterPool>();

        foreach (MonsterPool objectPool in MonsterPoolArray)
        {
            objectPool.Initialize();
            MonsterPoolDictionary.Add(objectPool.key, objectPool);
        }
    }

    public GameObject RequestObject(MONSTER_POOL key)
    {
        MonsterPool objectPool = MonsterPoolDictionary[key];

        if (objectPool != null)
        {
            GameObject requestObject;

            // 사용할 수 있는 오브젝트가 있을 경우
            if (objectPool.queue.Count > 0)
            {
                requestObject = objectPool.queue.Dequeue();
            }

            // 모두 사용중일 경우
            else
            {
                requestObject = Instantiate(objectPool.value, transform);
            }
            requestObject.SetActive(true);

            return requestObject;
        }

        Debug.Log("Object Pool Manager: THERE IS NO KEY VALUES MATCHED - " + key);
        return null;
    }

    public void ReturnObject(MONSTER_POOL key, GameObject returnObject)
    {
        MonsterPool objectPool = MonsterPoolDictionary[key];

        if (objectPool != null)
        {
            returnObject.SetActive(false);
            objectPool.queue.Enqueue(returnObject);
            return;
        }

        Debug.Log("Object Pool Manager: THERE IS NO KEY VALUES MATCHED - " + key);
        return;
    }

    #region Property
    public Dictionary<MONSTER_POOL, MonsterPool> MonsterPoolDictionary
    {
        get { return monsterPoolDictionary; }
        private set { monsterPoolDictionary = value; }
    }
    public MonsterPool[] MonsterPoolArray
    {
        get { return monsterPoolArray; }
        private set { monsterPoolArray = value; }
    }
    #endregion
}
*/