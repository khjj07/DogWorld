using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
public class MoveButtons : MonoBehaviour
{
    public CameraMove Cam;
    public LiveButton Left;
    public LiveButton Right;

    public void GotoLeft()
    {
        RoomFlowManager.instance.RoomFlow.OnNext(RoomFlowManager.instance.CurrentRoom.Left);
    }

    public void GotoRight()
    {
        RoomFlowManager.instance.RoomFlow.OnNext(RoomFlowManager.instance.CurrentRoom.Right);
    }

    void Start()
    {
        RoomFlowManager.instance.RoomFlow.Subscribe(room =>Left.gameObject.SetActive((bool)room.Left));
        RoomFlowManager.instance.RoomFlow.Subscribe(room => Right.gameObject.SetActive((bool)room.Right));
        RoomFlowManager.instance.RoomFlow.OnNext(RoomFlowManager.instance.CurrentRoom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
