using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class RoomFlowManager : Singleton<RoomFlowManager>
{
    public Room CurrentRoom;
    public Subject<Room> RoomFlow = new Subject<Room>();
    void Start()
    {
        SoundManager.Instance.PlayBGMSound(1);
        RoomFlow.Subscribe(room => CurrentRoom = room);
    }
    public void SetRoom(Room room)
    {
        RoomFlow.OnNext(room);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
