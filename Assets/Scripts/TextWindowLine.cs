using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class TextWindowLine : MonoBehaviour 
{

	public Text textAccess;


	private int _lineIndex = 0;
	public int lineIndex 
	{
		get {return _lineIndex; } 
		set {_lineIndex = value; }
	}


	void Start () 
	{
	}
	
	void Update () 
	{
	}

	public void SetLineText (string lineText) 
	{
		if (textAccess != null) {
			//Debug.Log ("textAccess = " + lineText);
			textAccess.text = lineText;
		} else {
			Debug.Log ("textAccess = null");

		}

	}

	public void SetFormattedLineText (string lineText) 
	{
		if (textAccess != null) {
			Debug.Log ("textAccess =" + lineText);
			textAccess.text = lineText + "<color=#ff00ffff>" + lineText + "</Color>" + "...eol";
		} else {
			Debug.Log ("textAccess = null");

		}

	}




	//We are <b>absolutely <i>definitely</i> not</b> amused


}
