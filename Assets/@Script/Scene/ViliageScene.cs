using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViliageScene : BaseScene
{
    [SerializeField] private Vector3 spawnPosition;

    protected override void Awake()
    {
        base.Awake();
        sceneType = SCENE_TYPE.VILIAGE;
    }

    public override void Initialize()
    {
        base.Initialize();

        if(Managers.DataManager.CurrentCharacter != null)
        {
            Managers.ResourceManager.InstantiatePrefab("Prefab_" + Managers.DataManager.CurrentCharacterData.CharacterClass, null,
                (GameObject targetObject) =>
                {
                    gameObject.transform.position = spawnPosition;
                    Managers.UIManager.OpenPanel(PANEL.UserPanel);
                });
        }
    }
}
