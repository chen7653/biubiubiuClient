using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSceneManager : SingletonMono<CSceneManager>
{
    /// <summary>
    /// 出生点集合
    /// </summary>
    Transform[] StartPos;
    /// <summary>
    /// 玩家集合
    /// </summary>
    List<Player> players;

    string mapName;

    private void Awake()
    {
        InitData();
    }

    private void InitData()
    {
        players =  new List<Player>();
        mapName = "MapGrid_1";
    }


    internal void Init()
    {
        //地图加载
        AddressablesManager.Instance.LoadAssetAsync<GameObject>(mapName, MapCallBack);
    }

    private void MapCallBack(GameObject go)
    {
        go = Instantiate(go);
        Transform startPos = go.transform.Find("startPos");
        //出生点
        StartPos = startPos.GetComponentsInChildren<Transform>();

        //创角
        //创建地图后直接 new 先这样干
        Player Character = new Player();
        PlayerStartGame(Character);
        players.Add(Character);

    }


    void Start()
    {
        
    }

    internal void PlayerStartGame(IAvatar avatar)
    {
        if (avatar is Player) 
        {
            Player player = avatar as Player;
            if (player.BodyRootGo == null)
            {
                AddressablesManager.Instance.LoadAssetAsync<GameObject>(player.plauerName, player.InstantiateBodyRootGo);
            }
            else
            {
                RandomPos(player.BodyRootGo);
            }
        }

    }
    /// <summary>
    /// 放置到随机出生点
    /// </summary>
    /// <param name="go"></param>
    private void RandomPos(GameObject go)
    {
        go.transform.position = StartPos[UnityEngine.Random.Range(0, StartPos.Length)].position;
    }

    /// <summary>
    /// 设置摄像机跟随目标
    /// </summary>
    /// <param name="avatar"></param>
    internal void SetCameraFollow(IAvatar avatar)
    {

        CinemachineBrain cinemachine = Camera.main.GetComponent<CinemachineBrain>();
        cinemachine.ActiveVirtualCamera.Follow = avatar.BodyRootGo.transform;
    }
}
