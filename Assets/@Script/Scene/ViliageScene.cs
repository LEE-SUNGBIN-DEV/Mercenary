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

        if(Managers.DataManager.CurrentCharacterData != null)
        {
            Managers.ResourceManager.InstantiatePrefab("Prefab_Player_Camera", null,
                (GameObject cameraObject) =>
                {
                    Managers.ResourceManager.InstantiatePrefab("Prefab_" + Managers.DataManager.CurrentCharacterData.CharacterClass, null,
                    (GameObject characterObject) =>
                    {
                        Character character = characterObject.GetComponent<Character>();
                        GameFunction.SetCharacterPosition(character, spawnPosition);
                        cameraObject.transform.position = spawnPosition;
                        Managers.UIManager.OpenPanel(PANEL.UserPanel);
                    });
                });
        }
    }

    public override void ExitScene()
    {
        base.ExitScene();
        Managers.UIManager.ClosePanel(PANEL.UserPanel);
    }
}
