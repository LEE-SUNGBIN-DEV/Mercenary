using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossRoomController : MonoBehaviour
{
    #region Event
    public static event UnityAction<float> onUpdateBossHPBar;
    #endregion

    [SerializeField] private SpawnData bossData;
    [SerializeField] private Transform bossSpawnPoint;
    [SerializeField] private GameObject[] bossRoomBarriers;
    [SerializeField] private Portal portal;
    [SerializeField] private GameObject entranceEffect;
    private Collider entranceCollider;

    private void OnEnable()
    {
        entranceCollider = GetComponent<Collider>();

        Enemy.onCurrentHitPointChanged -= UpdateBossHPBar;
        Enemy.onCurrentHitPointChanged += UpdateBossHPBar;

        Enemy.onDie -= CheckDungeonClear;
        Enemy.onDie += CheckDungeonClear;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Character>() != null)
        {
            StartBattle();
            entranceEffect.gameObject.SetActive(false);
        }
    }

    public void StartBattle()
    {
        entranceCollider.enabled = false;

        if (bossRoomBarriers.Length > 0)
        {
            for(int i=0; i<bossRoomBarriers.Length; ++i)
            {
                bossRoomBarriers[i].SetActive(true);
            }
        }

        for (int i = 0; i < bossData.spawnAmount; ++i)
        {
            Enemy monster = Managers.ObjectPoolManager.RequestObject(bossData.monster.key).GetComponent<Enemy>();
            monster.MonsterNavMeshAgent.Warp(bossSpawnPoint.position);
            monster.Target = Managers.DataManager.CurrentCharacter.transform;

            //Managers.UIManager.EntrancePanel.EntranceText.text = monster.GetComponent<Enemy>().MonsterName;
            Managers.UIManager.OpenPanel(PANEL.MapInformationPanel);
        }

        //Managers.UIManager.BossPanel.SetBossHPBar(1f);
        Managers.UIManager.OpenPanel(PANEL.MonsterInformationPanel);
    }

    public void UpdateBossHPBar(Enemy enemy)
    {
        if (enemy.MonsterName == bossData.monster.key)
        {
            float ratio = enemy.CurrentHitPoint / enemy.MaxHitPoint;
            if (onUpdateBossHPBar != null)
            {
                onUpdateBossHPBar(ratio);
            }
        }
    }

    public void CheckDungeonClear(Enemy monster)
    {
        if(monster.MonsterName == bossData.monster.key)
        {
            StartCoroutine(DungeonClear());
        }
    }

    IEnumerator DungeonClear()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1f;

        Enemy.onCurrentHitPointChanged -= UpdateBossHPBar;
        Enemy.onDie -= CheckDungeonClear;

        //Managers.UIManager.BossPanel.SetBossHPBar(1f);
        Managers.UIManager.ClosePanel(PANEL.MonsterInformationPanel);
        Managers.UIManager.RequestNotice("���� Ŭ����");

        portal.gameObject.SetActive(true);
    }
}
