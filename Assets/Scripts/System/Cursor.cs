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
        }else if(Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Furniture"))
        {
            transform.position = hit.point + new Vector3(0f, 0f, -4f);
        }
    }
    public void TryAddToInventory(IngameItem item)
    {
        bool success = Inventory.instance.Add(item.transform.GetComponent<SpriteRenderer>().sprite);
        if (success)
            Destroy(item.gameObject);
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
                .Select(x=>HoverTarget)
               .Where(_ => HoverTarget && Input.GetMouseButtonDown(1))
               .Subscribe(x => TryAddToInventory(x))
               .AddTo(gameObject);

    }
}
