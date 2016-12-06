using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ReduxGuiController : MonoBehaviour 
{
	public GameObject StartLineProxy;


	private List <GameObject> TextLineObjects = null;

	public static ReduxGuiController Instance;

	private GameObject TextLinesContainer;
	private const int objectPoolSize = 20;


	void Awake () 
	{
		Instance = this;

		TextLineObjects = new List<GameObject>();
	}

	void Start () 
	{

		TextLinesContainer = GameObject.Find ("TextLineWindow");


		for (int t = 0; t < objectPoolSize; t++) 
		{
			GameObject _lineObj = Instantiate (Resources.Load ("TextWindowLine", typeof(GameObject))) as GameObject;
			//GameObject _tapObj = Instantiate (tapObjectPrefab) as GameObject;

			Debug.Log ("TextLinesContainer = " + TextLinesContainer);
			if (TextLinesContainer != null) {
				_lineObj.transform.parent = TextLinesContainer.transform;
			}

			_lineObj.name = "LineObj" + t.ToString ();


			_lineObj.transform.position = new Vector3(StartLineProxy.transform.position.x, StartLineProxy.transform.position.y - ((float)t * 30.0f), StartLineProxy.transform.position.z);

			TextWindowLine lineObjectScript = _lineObj.GetComponent<TextWindowLine> ();
			if (lineObjectScript != null) {
				lineObjectScript.SetLineText (_lineObj.name);
			} else {
				Debug.Log ("lineObjectScript == null");
			}


			TextLineObjects.Add (_lineObj);
		}

	}
	
	void Update () 
	{
	
	}
}
