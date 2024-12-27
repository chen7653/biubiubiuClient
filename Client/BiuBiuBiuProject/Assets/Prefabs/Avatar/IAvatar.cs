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
    /// �������
    /// </summary>
    Player = 0,
    /// <summary>
    /// ����
    /// </summary>
    Character = 1,
}