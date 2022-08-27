using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;

public class LiveButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private Sequence ScaleSquence;
    public UnityEvent OnClick;
    private void Start()
    {
        ScaleSquence = DOTween.Sequence();
    }
    void OnEnable()
    {
        ScaleSquence.Restart();
    }
    void OnDestroy()
    {
        ScaleSquence.Kill();
    }

    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {
        transform.DOKill();
        transform.DOScale(new Vector3(1, 1, 1), 0.1f).OnComplete(()=> { OnClick.Invoke(); });
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {

        transform.DOKill();
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f);

    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.DOKill();
        transform.DOScale(new Vector3(1, 1, 1), 0.1f);
    }
}
