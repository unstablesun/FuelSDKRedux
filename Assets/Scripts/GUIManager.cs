using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {

	static GUIManager s_instance;

	public static GUIManager Instance
	{
		get
		{
			return s_instance as GUIManager;
		}
	}

	void OnDestroy() 
	{
		s_instance = null;
	}


	void Awake()
	{

	}

	void Start () 
	{

	}

	void Update () 
	{

	}




	void OnEnable()
	{
		FuelSDK.broadcastFuelSDKIgniteLoaded += onFuelSDKIgniteLoaded;
	}

	void OnDisable()
	{
		FuelSDK.broadcastFuelSDKIgniteLoaded -= onFuelSDKIgniteLoaded;

	}


	void onFuelSDKIgniteLoaded (Dictionary<string, object> data) 
	{
		Debug.Log ("REDUX LOG -------- onFuelSDKIgniteLoaded(GUI)");
	}


}
