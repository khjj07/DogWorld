using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIState : GameState
{
    public override void OnStateEnter()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        //GetComponent<Canvas>().enabled = true;
        onStateEnableEvent.Invoke();
    }

    public override void OnStateExit()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector3(200000, 200000, 0);
        //GetComponent<Canvas>().enabled = false;
        onStateExitEvent.Invoke();
    }
}
