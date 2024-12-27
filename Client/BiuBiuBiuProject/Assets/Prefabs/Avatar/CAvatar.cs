using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public abstract class CAvatar<T> : IAvatar where T : CAvatar<T>
{
    private int life;
    public int Life { get => life; set => life = value; }

    public virtual EMAvatarType AvatarType
    {
        get;
    }
    private GameObject mBodyRootGo;
    public GameObject BodyRootGo => mBodyRootGo;

    public void InstantiateBodyRootGo(GameObject bodyRootGo)
    {
        mBodyRootGo = GameObject.Instantiate(bodyRootGo);

        CSceneManager.Instance.PlayerStartGame(this);
        CSceneManager.Instance.SetCameraFollow(this);
    }
    public abstract void StartGame();

    public void Attack()
    {
    }

    public void BeAttack()
    {
    }

    public void OnDead()
    {
        if (life != 0)
        {
            life--;
            if (AvatarType == EMAvatarType.Character) StartGame();
        }
    }
}