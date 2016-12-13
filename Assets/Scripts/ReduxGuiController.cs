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

	//private List <GameObject> TextLineObjects = null;

	public static ReduxGuiController Instance;

	private GameObject TextLinesContainer;
	private const int objectPoolSize = 11;//22

	private int CurrentLineIndex = 0;

	void Awake () 
	{
		Instance = this;

		//TextLineObjects = new List<GameObject>();
	}

	void Start () 
	{
		//TextLinesContainer = GameObject.Find ("FuelPanel");

		RectTransform rt = (RectTransform)StartLineProxy.transform;
		float dy = rt.rect.height;

		for (int t = 0; t < objectPoolSize; t++) 
		{
			//GameObject _lineObj = Instantiate (Resources.Load ("TextLineObj", typeof(GameObject))) as GameObject;
			//GameObject _lineObj = Instantiate (TopLineProxy);

			//Debug.Log ("TextLinesContainer = " + TextLinesContainer);
			//if (TextLinesContainer != null) {
			//	_lineObj.transform.parent = TextLinesContainer.transform;
			//}

			//_lineObj.name = "LineObj" + t.ToString ();

			//_lineObj.transform.position = new Vector3(StartLineProxy.transform.position.x, StartLineProxy.transform.position.y - ((float)t * dy), StartLineProxy.transform.position.z);

			//_lineObj.gameObject.
			//_lineObj.transform.localScale = new Vector3(2, 1, 1);

			GameObject _lineObj = ListTextLineObjects [t];

			TextWindowLine lineObjectScript = _lineObj.GetComponent<TextWindowLine> ();
			if (lineObjectScript != null) {
				lineObjectScript.SetLineText (".");

				lineObjectScript.lineIndex = t;
			} else {
				Debug.Log ("lineObjectScript == null");
			}
				
			//TextLineObjects.Add (_lineObj);
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
