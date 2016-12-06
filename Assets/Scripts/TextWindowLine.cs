using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class TextWindowLine : MonoBehaviour 
{

	private Text textAccess;

	void Start () 
	{
	
		textAccess = GetComponent<Text>();

	}
	
	void Update () 
	{
	
	}

	public void SetLineText (string lineText) 
	{
		if (textAccess != null) {
			textAccess.text = lineText;
		} else {
			Debug.Log ("textAccess == null");

		}

	}


}
