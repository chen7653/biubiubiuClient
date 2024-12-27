using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSceneManager : SingletonMono<CSceneManager>
{
    /// <summary>
    /// �����㼯��
    /// </summary>
    Transform[] StartPos;
    /// <summary>
    /// ��Ҽ���
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
        //��ͼ����
        AddressablesManager.Instance.LoadAssetAsync<GameObject>(mapName, MapCallBack);
    }

    private void MapCallBack(GameObject go)
    {
        go = Instantiate(go);
        Transform startPos = go.transform.Find("startPos");
        //������
        StartPos = startPos.GetComponentsInChildren<Transform>();

        //����
        //������ͼ��ֱ�� new ��������
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
    /// ���õ����������
    /// </summary>
    /// <param name="go"></param>
    private void RandomPos(GameObject go)
    {
        go.transform.position = StartPos[UnityEngine.Random.Range(0, StartPos.Length)].position;
    }

    /// <summary>
    /// �������������Ŀ��
    /// </summary>
    /// <param name="avatar"></param>
    internal void SetCameraFollow(IAvatar avatar)
    {

        CinemachineBrain cinemachine = Camera.main.GetComponent<CinemachineBrain>();
        cinemachine.ActiveVirtualCamera.Follow = avatar.BodyRootGo.transform;
    }
}
