using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using System;


public class Cursor : Singleton<Cursor>
{
    public IngameObject FollowTarget;
    public IngameObject HoverTarget;
    public Dummy DummyChild;
    public float DragDistinct;
    private void SetTarget(IngameObject newObject)
    {
        var child = Instantiate(DummyChild);
        FollowTarget = newObject;
        FollowTarget.DummyChild = child;
        child.transform.parent = FollowTarget.transform;

        //Dummy
        FollowTarget.StateStream.OnNext(ObjectState.Picked);
        this.UpdateAsObservable()
            .Subscribe(_ => FollowTarget.transform.position = transform.position)
            .AddTo(child);

        this.UpdateAsObservable()
           .Where(_=>Input.GetMouseButtonDown(0))
           .Subscribe(_ => {
               FollowTarget.StateStream.OnNext(ObjectState.Placed);
               FollowTarget = null;})
           .AddTo(child);
    }
    public void MousePositioning()
    {
        RaycastHit hit;
        Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && (hit.collider.CompareTag("Floor")|| hit.collider.CompareTag("Wall")))
        {
            transform.position =  hit.point;
        }
    }
    public void Start()
    {
        this.UpdateAsObservable()
            .Subscribe(_ => MousePositioning())
            .AddTo(gameObject);

      
        this.ObserveEveryValueChanged(x=>transform.position)
          .Where(_=>HoverTarget&&Input.GetMouseButton(0))
          .Subscribe(x => HoverTarget.transform.position=x)
           .AddTo(gameObject);
    }
   
}
