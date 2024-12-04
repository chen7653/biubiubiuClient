using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;

//[ActionCategory(ActionCategory.Debug)]

public class ActionBase: FsmStateAction
{


    [Tooltip("Repeat every frame. NOTE: This operation will NOT be frame rate independent!")]
    public bool everyFrame;

    public override void Reset()
    {
        everyFrame = false;
    }

    public override void OnEnter()
    {
        if (!everyFrame)
            Finish();
    }

    // NOTE: very frame rate dependent!
    public override void OnUpdate()
    {

    }

#if UNITY_EDITOR
    public override string AutoName()
    {
        return ActionHelpers.AutoName(this,"");
    }
#endif
}

/*
  : ActionBase
{
    public override void OnEnter()
    {

        base.OnEnter();
    }
    // NOTE: very frame rate dependent!
    public override void OnUpdate()
    {

    }
}
 
 */



//变量 Variable
//int
//[RequiredField]
//[UIHint(UIHint.Variable)]
//public FsmInt intVariable;
//bool
//[RequiredField]
//[UIHint(UIHint.Variable)]
//public FsmBool boolVariable;

//过渡
//public FsmEvent mEvent;
//Fsm.Event(mEvent); 触发过渡