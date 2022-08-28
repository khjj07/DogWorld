using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiGuide : MonoBehaviour
{
    public List<Guide> GuideList = new List<Guide>();
    public int index=0;
    // Start is called before the first frame update
    public void CurrentGuideNext()
    {
        GuideList[index].NextGuide();
    }
    public void NextGuide()
    {
        index++;
    }
}
