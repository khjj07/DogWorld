using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{
   
    public float MoveDuration = 5f;
    
    public void Goto(Room room)
    {
        var pos = room.transform.position;
        pos.z = transform.position.z;
        transform.DOMove(pos, MoveDuration);
        RoomFlowManager.instance.RoomFlow.OnNext(room);
    }
    void Start()
    {
        RoomFlowManager.instance.RoomFlow.Subscribe(room => Goto(room));
    }

}
