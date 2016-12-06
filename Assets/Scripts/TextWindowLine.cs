using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class TextWindowLine : MonoBehaviour 
{

	public Text textAccess;

	void Start () 
	{
	

	}
	
	void Update () 
	{
	
	}

	public void SetLineText (string lineText) 
	{
		if (textAccess != null) {
			Debug.Log ("textAccess =" + lineText);
			textAccess.text = lineText;
		} else {
			Debug.Log ("textAccess = null");

		}

	}


}
