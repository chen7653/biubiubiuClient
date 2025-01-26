using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyComponent : NetworkBehaviour
{
    Player player;
    PlayMakerFSM InputFSM;
    //public float moveHorizontal;
    //public float moveVertical;
    //public bool moveJump;
    //public bool Attack;
    void Start()
    {
        
    }
    public void Init (Player player)
    {
        this.player = player;
        InputFSM = PlayMakerFSM.FindFsmOnGameObject(this.gameObject, "Inpnt");
        HandMovement();
    }
    void Update()
    {
        //HandMovement();
    }
    bool issetHand = true;
    void HandMovement()
    {
        //if (isLocalPlayer && issetHand)   //ÔÝÊ±×¢ÊÍ
        {
            InputFSM.FsmVariables.GetFsmBool("isLocalPlayer").Value = true;
            issetHand = false;
        }
    }

    public void OnDead()
    {
        this.player.OnDead();
    }
}
