using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : ActionBase
{

    [RequiredField]
    [UIHint(UIHint.Variable)]
    public FsmInt CurJumpCount;
    
    [RequiredField]
    [UIHint(UIHint.Variable)]
    public FsmInt MapJumpCount;

    [UIHint(UIHint.Variable)]
    public FsmBool JumpInpnt;

    public FsmEvent mJumpEvent;

    public override void OnEnter()
    {

        base.OnEnter();
    }
    // NOTE: very frame rate dependent!
    public override void OnUpdate()
    {
        if (JumpInpnt.Value && CurJumpCount.Value < MapJumpCount.Value)
        {
            Fsm.Event(mJumpEvent);
            Finish();
        }
    }
}
