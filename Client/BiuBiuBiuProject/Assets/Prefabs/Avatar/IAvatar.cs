using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAvatar
{
    int Life { get; set; }
    GameObject BodyRootGo { get; }
    void OnDead();
    void Attack();
    void BeAttack();

}

public enum EMAvatarType
{
    /// <summary>
    /// 其他玩家
    /// </summary>
    Player = 0,
    /// <summary>
    /// 主角
    /// </summary>
    Character = 1,
}