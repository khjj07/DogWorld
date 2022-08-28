using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sister : MonoBehaviour
{
    public UIState SisterState;
    private void OnMouseDown()
    {
        if(!GameStateManager.instance.currentState.Equals(SisterState))
        {
            GameStateManager.instance.Next();
        }
    }
}
