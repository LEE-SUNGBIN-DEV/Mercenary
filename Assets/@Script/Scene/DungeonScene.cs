using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DungeonScene : BaseScene
{
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private MonsterSpawnController[] normalMonsterSpawnPoint;
    [SerializeField] private BossRoomController bossRoomController;

    private int spawnOrder;

    protected override void Awake()
    {
        base.Awake();
        sceneType = SCENE_TYPE.DUNGEON;
    }

    public override void Initialize()
    {
        base.Initialize();
        Managers.UIManager.OpenPanel(PANEL.UserPanel);

        //Managers.UIManager.EntrancePanel.EntranceText.text = sceneName;
        //Managers.UIManager.EntrancePanel.gameObject.SetActive(true);

        spawnOrder = 0;
        if (normalMonsterSpawnPoint.Length > 0)
        {
            for (int i = 0; i < normalMonsterSpawnPoint.Length; ++i)
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
