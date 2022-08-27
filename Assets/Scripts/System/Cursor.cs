using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using System;
using System.Threading;
using System.Threading.Tasks;

public class Cursor : Singleton<Cursor>
{
    public IngameItem FollowTarget;
    public IngameItem HoverTarget;
    public float DragDistinct;
    public void SetTarget(IngameItem newItem)
    {
        FollowTarget = newItem;
        
     

        var followStream = this.UpdateAsObservable()
            .TakeUntil(newItem.OnMouseDownAsObservable())
            .Subscribe(_ => FollowTarget.transform.position = transform.position)
            .AddTo(newItem);


        FollowTarget.StateStream.OnNext(ObjectState.Float);

        newItem.OnMouseDownAsObservable()
        .Take(1)
        .Select(x=>transform.position)
        .Subscribe(x =>
        {
            var pos = x;
            transform.position = pos;
            FollowTarget.transform.position = pos;
            FollowTarget.StateStream.OnNext(ObjectState.Fall);
            FollowTarget = null;
        });

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
    public async void TryAddToInventory(IngameItem item)
    {
        Inventory.instance.Add(item.GetComponent<SpriteRenderer>().sprite);
    }
    public void Start()
    {
        this.UpdateAsObservable()
            .Subscribe(_ => MousePositioning())
            .AddTo(gameObject);


       this.ObserveEveryValueChanged(x => transform.position)
          .Where(_ => HoverTarget && Input.GetMouseButton(0))
          .Subscribe(x => HoverTarget.transform.position = x)
          .AddTo(gameObject);

        var clickStream = this.UpdateAsObservable()
               .Where(_ => Input.GetMouseButtonDown(0));

        clickStream.Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(300)))
               .Where(x => HoverTarget && x.Count >= 2)
               .Select(x=>HoverTarget)
               .Subscribe(_=>Task.Run(()=>TryAddToInventory(HoverTarget)));
    }
}
