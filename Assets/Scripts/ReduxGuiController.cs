using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ReduxGuiController : MonoBehaviour 
{


	private List <GameObject> TextLineObjects = null;

	public static ReduxGuiController Instance;

	private GameObject TextLinesContainer;
	private const int objectPoolSize = 4;


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
			GameObject _lineObj = Instantiate (Resources.Load ("TextWindowLine1", typeof(GameObject))) as GameObject;
			//GameObject _tapObj = Instantiate (tapObjectPrefab) as GameObject;

			Debug.Log ("TextLinesContainer = " + TextLinesContainer);
			if (TextLinesContainer != null) {
				_lineObj.transform.parent = TextLinesContainer.transform;
			} else {
			
			}

			_lineObj.name = "LineObj" + t.ToString ();

			/*
			Vector3 vRadius = Zenith.transform.position - CenterPoint.transform.position;
			_tapObj.transform.position = new Vector3(Zenith.transform.position.x, Zenith.transform.position.y - vRadius.y * 2.0f, Zenith.transform.position.z);

			TapObject tapObjectScript = _tapObj.GetComponent<TapObject> ();
			tapObjectScript.SetCenterPoint (new Vector3(CenterPoint.transform.position.x, CenterPoint.transform.position.y, CenterPoint.transform.position.z));
			tapObjectScript.SetZenithPoint (new Vector3(Zenith.transform.position.x, Zenith.transform.position.y, Zenith.transform.position.z));
			*/

			TextLineObjects.Add (_lineObj);
		}

	}
	
	void Update () 
	{
	
	}
}
