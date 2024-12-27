using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CAvatar<Player>
{
    public string plauerName = "Player_1";

    public override EMAvatarType AvatarType => EMAvatarType.Character;

    public override void StartGame()
    {
        CSceneManager.Instance.PlayerStartGame(this);
    }

}
