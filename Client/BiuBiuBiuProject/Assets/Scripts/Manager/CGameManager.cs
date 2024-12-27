using UnityEngine;

public class CGameManager : SingletonMono<CGameManager>
{
    void Start()
    {
        Init();
    }


    public void Init()
    {
        //CProtoclManager.CreateInstance();
        //CServerTime.CreateInstance();
        //UIManager.CreateInstance();
        CSceneManager.Instance.Init();
    }
    public static void CreateGameInstance()
    {

    }

}
