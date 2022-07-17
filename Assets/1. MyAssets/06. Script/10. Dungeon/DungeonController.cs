using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    [SerializeField] private string dungeonName;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private MonsterSpawnController[] normalMonsterSpawnPoint;

    [SerializeField] private BossRoomController bossRoomController;

    private int spawnOrder;

    private void Awake()
    {
        spawnOrder = 0;

        if(normalMonsterSpawnPoint.Length > 0)
        {
            for(int i=0; i<normalMonsterSpawnPoint.Length; ++i)
            {
                normalMonsterSpawnPoint[i].OnSpawn += ActiveNextSpawnPoint;
            }

            normalMonsterSpawnPoint[0].gameObject.SetActive(true);
        }

        else
        {
            bossRoomController.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        UIManager.Instance.EntrancePanel.EntranceText.text = dungeonName;
        UIManager.Instance.EntrancePanel.gameObject.SetActive(true);
    }

    public void ActiveNextSpawnPoint()
    {
        ++spawnOrder;
        if (spawnOrder == normalMonsterSpawnPoint.Length-1)
        {
            bossRoomController.gameObject.SetActive(true);
        }

        else
        {
            normalMonsterSpawnPoint[spawnOrder].gameObject.SetActive(true);
        }
    }
}
