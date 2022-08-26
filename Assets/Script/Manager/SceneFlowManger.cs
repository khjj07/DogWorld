using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

public class SceneFlowManger : Singleton<SceneFlowManger>
{
    public Subject<int> SceneNumber = new Subject<int>();
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        SceneNumber.Subscribe(x => SceneManager.LoadScene(x));
    }
    public void ChangeScene(int x)
    {
        SceneNumber.OnNext(x);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
