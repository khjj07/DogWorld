using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIState : GameState
{
    public override void OnStateEnter()
    {
        transform.position = new Vector3(960, 540, 0);
        //GetComponent<Canvas>().enabled = true;
        onStateEnableEvent.Invoke();
    }

    public override void OnStateExit()
    {
        transform.position = new Vector3(20000, 20000, 0);
       //GetComponent<Canvas>().enabled = false;
        onStateExitEvent.Invoke();
    }
}
