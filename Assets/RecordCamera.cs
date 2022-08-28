using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Moments;
using Moments.Encoder;

public class RecordCamera : MonoBehaviour
{
	Recorder m_Recorder;
	float m_Progress = 0f;
	string m_LastFile = "";
	bool m_IsSaving = false;
	// Start is called before the first frame update
	void Start()
    {
       
    }

	public void Record()
    {
		SoundManager.Instance.PlaySFXSound("capture", 1);
		m_Recorder = GetComponent<Recorder>();
		m_Recorder.Record();
		m_Recorder.OnPreProcessingDone = OnProcessingDone;
		m_Recorder.OnFileSaveProgress = OnFileSaveProgress;
		m_Recorder.OnFileSaved = OnFileSaved;
		StartCoroutine(Save());
	}

	public void Capture()
	{
		Camera.main.enabled = false;
		//ScreenCapture.CaptureScreenshot("sekainu_screenshot.png");
		
	}
	public IEnumerator Save()
    {

		yield return new WaitForSeconds(3f);
		m_Recorder.Save();
		m_Progress = 0f;
		
	}
	void OnProcessingDone()
	{
		m_IsSaving = true;
	}

	void OnFileSaveProgress(int id, float percent)
	{
		m_Progress = percent * 100f;
	}

	void OnFileSaved(int id, string filepath)
	{
		m_LastFile = filepath;
		m_IsSaving = false;
		m_Recorder.Record();
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
