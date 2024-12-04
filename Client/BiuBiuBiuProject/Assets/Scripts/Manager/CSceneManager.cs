using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSceneManager : MonoBehaviour
{
    public static CSceneManager Instance;

    public static CSceneManager CreateInstance()
    {
        GameObject go = new GameObject("CSceneManager");
        DontDestroyOnLoad(go);
        Instance = go.AddComponent<CSceneManager>();
        Instance.InitData();
        return Instance;
    }

    private void InitData()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
