using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Item : LiveButton
{
    public Sprite ItemSprite;
    public Room InnerRoom;
    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        if(!RoomFlowManager.instance.CurrentRoom.Equals(InnerRoom))
        {
            var instance = ItemManager.instance.MakeInstance(ItemSprite);
            Cursor.instance.SetTarget(instance.GetComponent<IngameItem>());
            ItemSprite = null;
            GetComponent<Image>().sprite = ItemSprite;
            GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
        }
     
    }

    public void Initialize()
    {
        GetComponent<Image>().sprite = ItemSprite;
        if (!ItemSprite)
            GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
        else
            GetComponent<Image>().color = new Vector4(1, 1, 1, 1);

    }
    public void Initialize(Sprite newSprite)
    {
        ItemSprite = newSprite;
        GetComponent<Image>().sprite = ItemSprite;
        if (!ItemSprite)
            GetComponent<Image>().color=new Vector4(1,1,1,0);
        else
            GetComponent<Image>().color=new Vector4(1, 1, 1, 1);


    }

    private void Start()
    {
        Initialize();
    }

}
