using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Title : MonoBehaviour
{
    public Image[] images;
    // Start is called before the first frame update
    public void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        foreach(var i in images)
        {
            var pos = i.transform.position;
            i.transform.position = new Vector3(0, -1000, 0);
            i.transform.DOMove(pos, 2f).SetEase(Ease.OutExpo);
        }
    }
}
