using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ReduxGuiController : MonoBehaviour 
{
	public GameObject StartLineProxy;
	public GameObject TopLineProxy;
	public List <GameObject> ListTextLineObjects;
	public List <GameObject> ListMissionButtonObjects;
	public static ReduxGuiController Instance;
	private GameObject TextLinesContainer;
	private int CurrentLineIndex = 0;

	void Awake () 
	{
		Instance = this;

	}

	void Start () 
	{
		RectTransform rt = (RectTransform)StartLineProxy.transform;
		float dy = rt.rect.height;

		int objCount = ListTextLineObjects.Count;

		for (int t = 0; t < objCount; t++) 
		{
			GameObject _lineObj = ListTextLineObjects [t];

			TextWindowLine lineObjectScript = _lineObj.GetComponent<TextWindowLine> ();
			if (lineObjectScript != null) {
				lineObjectScript.SetLineText (".");

				lineObjectScript.lineIndex = t;
			} else {
				Debug.Log ("lineObjectScript == null");
			}				
		}
			
		QueryLineObjectsClear ();
	}
	
	void Update () 
	{
	
	}

	public void QueryLineObjectsClear() 
	{
		foreach(GameObject tObj in ListTextLineObjects)
		{
			TextWindowLine lineObjectScript = tObj.GetComponent<TextWindowLine> ();
			lineObjectScript.SetLineText (".");
		}
		CurrentLineIndex = 0;
	}
		
	public void onGetEventsButtonClick () 
	{
		FuelManager.Instance.StartGetEventsCorroutine ();
	}
		
	public void addTextToWindow (string text) 
	{
		QueryLineObjectsAddTextLine (text);
	}
	public void addLabelAndStringToWindow (string label, string data) 
	{
		string combine = label + " : " + data;
		QueryLineObjectsAddTextLine (combine);
	}
	public void addLabelAndDateTimeToWindow (string label, DateTime time) 
	{
		string combine = label + " : " + time.ToLocalTime();
		QueryLineObjectsAddTextLine (combine);
	}

	public void QueryLineObjectsAddTextLine(string text) 
	{
		foreach(GameObject tObj in ListTextLineObjects)
		{
			TextWindowLine lineObjectScript = tObj.GetComponent<TextWindowLine> ();

			int index = lineObjectScript.lineIndex;
			if (index == CurrentLineIndex) {
				lineObjectScript.SetLineText (text);

				CurrentLineIndex++;
				break;
			}
		}
	}

}
