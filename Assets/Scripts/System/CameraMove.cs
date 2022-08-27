using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{
   
    public float MoveDuration = 1f;
    public Ease moveEase;
    public void Goto(Room room)
    {
        var pos = room.transform.position;
        pos.z = transform.position.z;
        transform.DOMove(pos, MoveDuration)
            .SetEase(moveEase);
    }
    void Start()
    {
        RoomFlowManager.instance.RoomFlow.Subscribe(room => Goto(room));
    }

}
