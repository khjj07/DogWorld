using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Title : MonoBehaviour
{
    public Image[] images;
    public Image title;
    // Start is called before the first frame update
    public void Start()
    {
        Initialize();
        SoundManager.Instance.PlayBGMSound(1);
    }
    public void Initialize()
    {
        title.color = Color.white;
        foreach (var i in images)
        {
            var pos = i.transform.position;
            i.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -1000, 0);
            i.GetComponent<RectTransform>().DOMove(pos, 0.1f).SetEase(Ease.OutExpo);
        }
        title.DOColor(Color.yellow, 4f);
    }
}
