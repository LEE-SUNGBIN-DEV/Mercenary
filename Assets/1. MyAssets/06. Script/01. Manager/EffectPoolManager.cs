using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// =================== EFFECT POOL MANAGER CLASS (Singleton) ===========================
// ����Ʈ ������Ʈ�� Ǯ�� �ϴ� Ŭ����
// =====================================================================================
public enum EFFECT_POOL
{
    // Player Default Effect: 0 ~ 999
    PLAYER_ATTACK = 0,
    PLAYER_DEFENCE = 1,
    PLAYER_PERFECT_DEFENCE = 2,
    PLAYER_SMASH = 3,
    PLAYER_COUNTER = 4,
    PLAYER_COMPETE_START = 5,

    // All Class Effect: 1000~9999, Interval: 200 
    // Lancer Effect: 1000~1199


    // Monster Effect: 10000 ~ 14999, Interval: 20
    MONSTER_ARROW = 10000,
    MONSTER_ARROW_HIT = 10001,

    // Boss Monster Effect: 15000 ~ 24999, Interval: 100
    BLACK_DRAGON_FIRE_BALL = 15000,
    BLACK_DRAGON_FIRE_BALL_HIT = 15001,
    BLACK_DRAGON_FIRE_BALL_AFTER = 15002,
    BLACK_DRAGON_BREATH_AFTER = 15003,
    BLACK_DRAGON_LIGHTNING = 15004,

    // Combat Effect: 25000 ~ 
    COMBAT_COMPETE_START = 25000,
    COMBAT_COMPETE_PROGRESS = 250001,
    COMBAT_DAMAGE_TEXT = 250002,
    SIZE
}

public class EffectPoolManager : Singleton<EffectPoolManager>
{
    [System.Serializable]
    public class EffectPool
    {
        public EFFECT_POOL key;
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

    private Dictionary<EFFECT_POOL, EffectPool> effectPoolDictionary;
    [SerializeField] private EffectPool[] effectPoolArray;

    private void Start()
    {
        EffectPoolDictionary = new Dictionary<EFFECT_POOL, EffectPool>();

        foreach (EffectPool objectPool in EffectPoolArray)
        {
            objectPool.Initialize();
            EffectPoolDictionary.Add(objectPool.key, objectPool);
        }
    }

    public GameObject RequestObject(EFFECT_POOL key)
    {
        EffectPool objectPool = EffectPoolDictionary[key];

        if (objectPool != null)
        {
            GameObject requestObject;

            // ����� �� �ִ� ������Ʈ�� ���� ���
            if (objectPool.queue.Count > 0)
            {
                requestObject = objectPool.queue.Dequeue();
            }

            // ��� ������� ���
            else
            {
                requestObject = Instantiate(objectPool.value, transform);
            }
            requestObject.SetActive(true);

            return requestObject;
        }

        return null;
    }

    public GameObject RequestObject(EFFECT_POOL key, Vector3 position)
    {
        EffectPool objectPool = EffectPoolDictionary[key];

        if (objectPool != null)
        {
            GameObject requestObject;

            // ����� �� �ִ� ������Ʈ�� ���� ���
            if (objectPool.queue.Count > 0)
            {
                requestObject = objectPool.queue.Dequeue();
            }

            // ��� ������� ���
            else
            {
                requestObject = Instantiate(objectPool.value, transform);
            }

            requestObject.transform.position = position;
            requestObject.SetActive(true);

            return requestObject;
        }

        return null;
    }
    
    public void ReturnObject(EFFECT_POOL key, GameObject returnObject)
    {
        EffectPool objectPool = EffectPoolDictionary[key];

        if (objectPool != null)
        {
            returnObject.SetActive(false);
            objectPool.queue.Enqueue(returnObject);
            return;
        }

        return;
    }

    #region Property
    public Dictionary<EFFECT_POOL, EffectPool> EffectPoolDictionary
    {
        get { return effectPoolDictionary; }
        private set { effectPoolDictionary = value; }
    }
    public EffectPool[] EffectPoolArray
    {
        get { return effectPoolArray; }
        private set { effectPoolArray = value; }
    }
    #endregion
}
