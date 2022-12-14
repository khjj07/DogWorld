using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using DG.Tweening;
using System.Threading;
using System.Threading.Tasks;

public class SupplyBox : MonoBehaviour
{
    public int Number=5;
    public IngameItem ingameItemPrefab;
    public Transform Muzzle;
    public float AnimateDuration = 3f;
    private Sprite[] items;
    public void Start()
    {
        items = Resources.LoadAll<Sprite>("Item");
       
    }
    public void ItemOut()
    {
        bool full = Inventory.instance.Full();
        if (!full)
        {
            SoundManager.Instance.PlaySFXSound("box", 1);
            var instance = ItemManager.instance.MakeInstance(items[Random.Range(0, items.Length-1)]);
            instance.GetComponent<IngameItem>().State = ObjectState.Fall;
            instance.transform.position = Muzzle.position;
            var force = new Vector3(Random.Range(-1f, 1f) * 100, Random.Range(1f, 1.5f) * 300, Random.Range(-5f, -1f) * 100);
            instance.GetComponent<Rigidbody>().AddForce(force);
            transform.DOShakeScale(0.1f, 5f)
                .SetEase(Ease.OutBack)
                .OnComplete(() => {transform.DOScale(transform.localScale * 0.9f, 0.01f); });
            transform.DOShakeRotation(0.1f, 3f);
          
            bool success = Inventory.instance.Add(instance.GetComponent<SpriteRenderer>().sprite);
            if (success)
            {
                instance.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 1f)
                    .OnComplete(() => { Destroy(instance); });
            }
               
        }
       
     
    }

    public void TryItemOut()
    {
        if(Number > 0)
        {
            ItemOut();
            Number--;
            
        }
        if(Number == 0)
        {
            transform.DOScale(Vector3.zero, 0.1f)
                .OnComplete(() => { Destroy(gameObject); });
        }
    }



        private void OnMouseDown()
    {
        TryItemOut();
    }
}
