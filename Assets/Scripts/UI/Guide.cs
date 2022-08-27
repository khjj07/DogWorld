using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using System.Threading.Tasks;
using System.Threading;
public class Guide : MonoBehaviour
{
    public List<string> GuideMessages;
    public TextMeshProUGUI Text;
    public int Index=0;
    public float TypeDuration = 0.1f;
    public bool IsPlaying = false;
    private Coroutine TypeRoutine;
    public IEnumerator Print(string message)
    {
        Debug.Log(11111);
        if (!IsPlaying)
        {
            IsPlaying = true;
            var type = "";
            for (int i = 0; i < message.Length; i++)
            {
                type = type + message[i];
                Text.text = type;
                yield return new WaitForSeconds(TypeDuration);
            }
            IsPlaying = false;
            Index +=1;
        }
        else
        {
            Text.text = message;
            IsPlaying = false;
            Index +=1;
            StopCoroutine(TypeRoutine);
        }
        
    }
    // Start is called before the first frame update
    public void NextGuide()
    {
        if(Index<GuideMessages.Count)
            TypeRoutine = StartCoroutine(Print(GuideMessages[Index]));
    }
}
