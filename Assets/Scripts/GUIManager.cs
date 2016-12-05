using UnityEngine;
using System.Collections;

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


	void onFuelSDKIgniteLoaded (string message) 
	{
		Debug.Log ("REDUX LOG -------- onFuelSDKIgniteLoaded(GUI)");
	}


}
