using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class ItemManager : Singleton<ItemManager>
{
    public SupplyBox SupplyBoxPrefab;
    public Transform BoxPos;
    public IngameItem IngameItemPrefab;
    public GameObject MakeInstance(Sprite itemSprite)
    {
        if(itemSprite)
        {
            SoundManager.Instance.PlaySFXSound("box", 1);
            var instance = Instantiate(IngameItemPrefab.gameObject);
            instance.transform.localScale = new Vector3(4, 4, 1);
            instance.GetComponent<SpriteRenderer>().sprite = itemSprite;
            return instance;
        }
        return null;
    }
    public void MakeSupplyBox()
    {
        var instance = Instantiate(SupplyBoxPrefab.gameObject);
        instance.transform.position = BoxPos.position;
    }


}
