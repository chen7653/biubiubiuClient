using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameManager : MonoBehaviour
{
    public static CGameManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        
    }


    public  bool Init(int  i)
    {
        //CProtoclManager.CreateInstance();
        //CServerTime.CreateInstance();
        //UIManager.CreateInstance();
        CSceneManager.CreateInstance();
        return true;
    }
    public static void CreateGameInstance()
    {
        //CProtoclManager.CreateInstance();
        //CServerTime.CreateInstance();
        //UIManager.CreateInstance();
        CSceneManager.CreateInstance();
    }
}
