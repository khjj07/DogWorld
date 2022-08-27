using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;

public class LiveButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    protected Sequence ScaleSquence;
    public UnityEvent OnClick;
    public void Start()
    {
        ScaleSquence = DOTween.Sequence();
      
    }
    public void OnEnable()
    {
        ScaleSquence.Restart();
    }

    public void OnDestroy()
    {
        ScaleSquence.Kill();
    }

    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log(1);
        transform.DOKill();
        transform.DOScale(new Vector3(1, 1, 1), 0.1f).OnComplete(()=> { OnClick.Invoke(); });
    }

    public virtual void OnPointerEnter(PointerEventData pointerEventData)
    {
        transform.DOKill();
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f);

    }

    //Detect when Cursor leaves the GameObject
    public virtual void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.DOKill();
        transform.DOScale(new Vector3(1, 1, 1), 0.1f);
    }
}
