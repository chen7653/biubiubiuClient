using UnityEngine;
public abstract class CAvatar<T> : IAvatar where T : CAvatar<T>
{
    public int id;
    private int life;
    public int Life { get => life; set => life = value; }

    public virtual EMAvatarType AvatarType
    {
        get;
    }
    private GameObject mBodyRootGo;
    public GameObject BodyRootGo => mBodyRootGo;

    public virtual void InstantiateBodyRootGo(GameObject bodyRootGo)
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
        if (AvatarType == EMAvatarType.Character)
        {
            if (true)//life != 0)
            {
                life--;
                if (AvatarType == EMAvatarType.Character) StartGame();
            }
            else
            {
                //¹ÛÕ½?
                var Follow = CSceneManager.Instance.players.Find((player) => player.id != this.id);
                CSceneManager.Instance.SetCameraFollow(Follow);
            }
        }

    }
}