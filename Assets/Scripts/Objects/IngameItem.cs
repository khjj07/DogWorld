using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using System.Linq;
public enum ObjectState
{
    Picked,
    Placed,
    Float,
    Fall
}

public class IngameItem: MonoBehaviour
{
    public Subject<ObjectState> StateStream = new Subject<ObjectState>();
    public ObjectState State;
    public Dummy DummyChild;

    private void Start()
    {
        StateStream.Subscribe(x => State = x);
        StateStream.Where(x => DummyChild && x.Equals(ObjectState.Placed))
            .Subscribe(_ => Destroy(DummyChild.gameObject));

        StateStream.Where(x => DummyChild && x.Equals(ObjectState.Picked))
         .Subscribe(_ => Destroy(DummyChild.gameObject));

        //Float
        StateStream.Where(x => DummyChild && x.Equals(ObjectState.Float))
         .Subscribe(_ => Destroy(DummyChild.gameObject));
        StateStream.Where(x => x.Equals(ObjectState.Float))
         .Subscribe(_ => GetComponent<Rigidbody>().isKinematic=true);

        //Fall
        StateStream.Where(x => x.Equals(ObjectState.Fall))
        .Subscribe(_ => GetComponent<Rigidbody>().isKinematic = false);

    }


    public void OnMouseEnter()
    {
        if (State.Equals(ObjectState.Placed))
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
                    pos.z = pos.z-1.2f;
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
        if(State.Equals(ObjectState.Fall) && collision.transform.CompareTag("Floor"))
            StateStream.OnNext(ObjectState.Placed);
    }
}
