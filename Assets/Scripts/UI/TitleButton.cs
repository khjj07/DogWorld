using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class TitleButton : LiveButton,IPointerEnterHandler,IPointerExitHandler, IPointerClickHandler
{
    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        base.OnPointerClick(pointerEventData);
    }
    public override void OnPointerEnter(PointerEventData pointerEventData)
    {
        base.OnPointerEnter(pointerEventData);
        GetComponent<Image>().DOColor(new Color(1,1,0.7f,1),0.1f);

    }

    //Detect when Cursor leaves the GameObject
    public override void OnPointerExit(PointerEventData pointerEventData)
    {
        base.OnPointerExit(pointerEventData);
        GetComponent<Image>().DOColor(Color.white, 0.1f);
    }
}
