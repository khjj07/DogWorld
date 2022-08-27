using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Item : LiveButton
{
    public Sprite ItemSprite;

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        ItemManager.instance.MakeInstance(ItemSprite);
        ItemSprite = null;
        GetComponent<Image>().sprite = ItemSprite;
    }

    public void Initialize()
    {
        GetComponent<Image>().sprite = ItemSprite;
    }
    public void Initialize(Sprite newSprite)
    {
        ItemSprite = newSprite;
        GetComponent<Image>().sprite = ItemSprite;
    }

    private void Start()
    {
        Initialize();
    }

}
