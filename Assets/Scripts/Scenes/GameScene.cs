using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    //GameScene가 꺼져 있어도 작동 시키기 위해 Awake로 변경
    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;
        //Managers.UI.ShowSceneUI<UI_Inven>();

        Dictionary<int, Data.Stat> Dict = Managers.Data.StatDict;
        //Temp_유니티쨩 2개 생성
        //for (int i = 0; i < 2; i++)
        //{
        //    Managers.Resources.Instantiate("unitychan");
        //}
        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);
        //Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(5);
    }

    public override void Clear()
    {

    }
}
