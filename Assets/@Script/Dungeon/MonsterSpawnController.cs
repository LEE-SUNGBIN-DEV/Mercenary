using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SpawnData
{
    public int spawnAmount;
    public ObjectPool monster;
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
                GameObject poolObject = Managers.ObjectPoolManager.RequestObject(spawnDatas[i].monster.key);
                Enemy monster = poolObject.GetComponent<Enemy>();
                Vector3 monsterPosition = transform.position + Random.insideUnitSphere * 5f;
                monster.MonsterNavMeshAgent.Warp(monsterPosition);
                monster.Target = Managers.DataManager.CurrentCharacter.transform;
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
