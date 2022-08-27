using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using System.Linq;
using System;
public enum ObjectState
{
    Placed,
    Float,
    Fall
}

public class IngameItem : MonoBehaviour
{
    public Subject<ObjectState> StateStream = new Subject<ObjectState>();
    public ObjectState State;

    public void Start()
    {
        var renderer = GetComponent<SpriteRenderer>();
        GetComponent<BoxCollider>().size = new Vector3(renderer.size.x, renderer.size.y,0.1f);
        StateStream.Subscribe(x => { State = x;
            Debug.Log("State : " + State);
        });

        StateStream.Where(x => x.Equals(ObjectState.Float))
         .Subscribe(_ => {
             GetComponent<Rigidbody>().isKinematic = true;
             GetComponent<BoxCollider>().isTrigger = true;
         });

        StateStream.Where(x => x.Equals(ObjectState.Placed))
        .Subscribe(_ => {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<BoxCollider>().isTrigger = false;
        });

        //Fall
        StateStream.Where(x => x.Equals(ObjectState.Fall))
         .Subscribe(_ => {
             GetComponent<Rigidbody>().isKinematic = false;
             GetComponent<BoxCollider>().isTrigger = false;
         });
        StateStream.OnNext(State);

    }


    public void OnMouseEnter()
    {
        if (State.Equals(ObjectState.Placed) && !Cursor.instance.FollowTarget && !Cursor.instance.HoverTarget )
        {
            Cursor.instance.HoverTarget = this;
            this.OnMouseDownAsObservable()
                .Take(1)
                .Subscribe(_ => StateStream.OnNext(ObjectState.Float));

            this.OnMouseUpAsObservable()
                .Take(1)
                .Subscribe(_ => {
                    Cursor.instance.HoverTarget = null;
                    StateStream.OnNext(ObjectState.Fall);
                    var pos = transform.position;
                    transform.position = pos;
                });

        }
    }
    public void OnMouseExit()
    {
        if (!Input.GetMouseButton(0))
            Cursor.instance.HoverTarget = null;
    }

    public void OnCollisionStay(Collision collision)
    {
        if(State.Equals(ObjectState.Fall) && (collision.transform.CompareTag("Floor") || collision.transform.CompareTag("Item")))
            StateStream.OnNext(ObjectState.Placed);
    }
}
