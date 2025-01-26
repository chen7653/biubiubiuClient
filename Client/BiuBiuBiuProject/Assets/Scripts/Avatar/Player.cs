using UnityEngine;

public class Player : CAvatar<Player>
{
    public string plauerName = "Player_1";
    public PlayerBodyComponent PlayerBody;
    public override EMAvatarType AvatarType => EMAvatarType.Character;

    public override void StartGame()
    {
        CSceneManager.Instance.PlayerStartGame(this);
    }

    public override void InstantiateBodyRootGo(GameObject bodyRootGo)
    {
        base.InstantiateBodyRootGo(bodyRootGo);
        PlayerBody = BodyRootGo.GetComponent<PlayerBodyComponent>();
        PlayerBody.Init(this);
    }
}
