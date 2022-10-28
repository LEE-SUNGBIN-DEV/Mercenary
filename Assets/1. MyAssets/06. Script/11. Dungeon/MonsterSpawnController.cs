using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SpawnData
{
    public int spawnAmount;
    public MONSTER_POOL monster;
}

public class MonsterSpawnController : MonoBehaviour
{
    [SerializeField] private SpawnData[] spawnDatas;
    public UnityAction OnSpawn;

    private void SpawnMonster()
    {
        for (int i = 0; i < spawnDatas.Length; ++i)
        {
            for (int j = 0; j < spawnDatas[i].spawnAmount; ++j)
            {
                GameObject poolObject = MonsterPoolManager.Instance.RequestObject(spawnDatas[i].monster);
                Enemy monster = poolObject.GetComponent<Enemy>();
                Vector3 monsterPosition = transform.position + Random.insideUnitSphere * 5f;
                monster.MonsterNavMeshAgent.Warp(monsterPosition);
                monster.Target = GameManager.Instance.Player.transform;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SpawnMonster();
            OnSpawn();
            gameObject.SetActive(false);
        }
    }
}
