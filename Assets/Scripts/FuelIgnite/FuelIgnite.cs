using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FuelSDKIntegration.Structures;


//-----------------------------------------------------------------
/*
	  						IGNITE INIT
*/
//-----------------------------------------------------------------
public partial class FuelIgnite : MonoBehaviour
{

	List<IgniteEvent> mIgniteEventList = new List<IgniteEvent> ();

	List<IgniteSampleEvent> mIgniteSampleEventList = new List<IgniteSampleEvent> ();

	Dictionary<string, IgniteEvent> mIgniteEventsDictionary = null;


	public bool mIgniteLoaded = false;

	//private bool mCharacterDataLoaded = false;

	public static FuelIgnite Instance;
	void Awake()
	{
		Instance = this;

		mIgniteLoaded = false;

		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "FuelIgnite Awake!");

		Debug.Log ("REDUX LOG -------- FuelIgnite Awake!");
	}
	void OnDestroy() 
	{
		Instance = null;
	}

	void Start () 
	{
		//We need to wait for this event before we can call getEvents with character unlock filters
		//EventDispatch.RegisterInterest("OnCharacterDataLoaded", this);
		//EventDispatch.RegisterInterest("OnStoreInitialised", this);

		Event_OnCharacterDataLoaded (0);
	}

	private void Event_OnCharacterDataLoaded(int numInSaveGame)
	{
		//mCharacterDataLoaded = true;

		StartGetEventsCorroutine ();

	}

	private void Event_OnStoreInitialised()
	{
	}

	void Update () 
	{
	}

	void OnEnable()
	{
		FuelSDK.broadcastFuelSDKIgniteLoaded += onFuelSDKIgniteLoaded;


		FuelSDK.broadcastFuelSDKIgniteSampleEvents += onFuelSDKIgniteSampleEvents;
		FuelSDK.broadcastFuelSDKIgniteEvents += onFuelSDKIgniteEvents;
		FuelSDK.broadcastFuelSDKIgniteMission += onFuelSDKIgniteMission;
		FuelSDK.broadcastFuelSDKVirtualGoodList += onFuelSDKVirtualGoodList;

		FuelSDK.broadcastFuelSDKNotificationEnabled += onFuelSDKNotificationEnabled;
		FuelSDK.broadcastFuelSDKNotificationDisabled += onFuelSDKNotificationDisabled;

		//FuelSDK.broadcastFuelSDKLastRequestFailed += onFuelSDKLastRequestFailed;
	}

	void OnDisable()
	{
		FuelSDK.broadcastFuelSDKIgniteLoaded -= onFuelSDKIgniteLoaded;

		FuelSDK.broadcastFuelSDKIgniteSampleEvents -= onFuelSDKIgniteSampleEvents;
		FuelSDK.broadcastFuelSDKIgniteEvents -= onFuelSDKIgniteEvents;
		FuelSDK.broadcastFuelSDKIgniteMission -= onFuelSDKIgniteMission;
		FuelSDK.broadcastFuelSDKVirtualGoodList -= onFuelSDKVirtualGoodList;

		FuelSDK.broadcastFuelSDKNotificationEnabled -= onFuelSDKNotificationEnabled;
		FuelSDK.broadcastFuelSDKNotificationDisabled -= onFuelSDKNotificationDisabled;

		//FuelSDK.broadcastFuelSDKLastRequestFailed -= onFuelSDKLastRequestFailed;
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
