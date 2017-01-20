using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FuelSDKIntegration.Structures;


public partial class FuelIgnite : MonoBehaviour
{
	//-----------------------------------------------------------------
	/*
	  							IGNITE INIT
	*/
	//-----------------------------------------------------------------


	List<IgniteEvent> mIgniteEventList = new List<IgniteEvent> ();

	Dictionary<string, IgniteEvent> mIgniteEventsDictionary = null;


	public bool mIgniteLoaded = false;


	public static FuelIgnite Instance;
	void Awake()
	{
		Instance = this;

		mIgniteLoaded = false;

		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "FuelIgnite Awake!");
	}
	void OnDestroy() 
	{
		Instance = null;
	}

	void Start () 
	{
		GetEventsWithTags ();
	}


	void OnEnable()
	{
		FuelSDK.broadcastFuelSDKIgniteLoaded += onFuelSDKIgniteLoaded;

		FuelSDK.broadcastFuelSDKIgniteEvents += onFuelSDKIgniteEvents;
		FuelSDK.broadcastFuelSDKIgniteMission += onFuelSDKIgniteMission;
		FuelSDK.broadcastFuelSDKVirtualGoodList += onFuelSDKVirtualGoodList;

		FuelSDK.broadcastFuelSDKNotificationEnabled += onFuelSDKNotificationEnabled;
		FuelSDK.broadcastFuelSDKNotificationDisabled += onFuelSDKNotificationDisabled;

		FuelSDK.broadcastFuelSDKLastRequestFailed += onFuelSDKLastRequestFailed;
	}

	void OnDisable()
	{
		FuelSDK.broadcastFuelSDKIgniteLoaded -= onFuelSDKIgniteLoaded;

		FuelSDK.broadcastFuelSDKIgniteEvents -= onFuelSDKIgniteEvents;
		FuelSDK.broadcastFuelSDKIgniteMission -= onFuelSDKIgniteMission;
		FuelSDK.broadcastFuelSDKVirtualGoodList -= onFuelSDKVirtualGoodList;

		FuelSDK.broadcastFuelSDKNotificationEnabled -= onFuelSDKNotificationEnabled;
		FuelSDK.broadcastFuelSDKNotificationDisabled -= onFuelSDKNotificationDisabled;

		FuelSDK.broadcastFuelSDKLastRequestFailed -= onFuelSDKLastRequestFailed;
	}

	void onFuelSDKIgniteLoaded (Dictionary<string, object> data) 
	{
		Debug.Log ("onFuelSDKIgniteLoaded");

		mIgniteLoaded = true;
	}

	void onFuelSDKLastRequestFailed (string errorMessage) 
	{
		Debug.Log ("onFuelSDKLastRequestFailed error = " + errorMessage);


	}
		
}
