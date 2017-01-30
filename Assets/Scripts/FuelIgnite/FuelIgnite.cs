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
	List<IgniteEvent> mIgniteSampleEventList = new List<IgniteEvent> ();

	Dictionary<string, IgniteEvent> mIgniteEventsDictionary = null;

	private bool mIgniteEventsRecieved = false;
	private bool mIgniteSampleEventsRecieved = false;

	public bool mIgniteLoaded = false;

	//private bool mCharacterDataLoaded = false;

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
		//We need to wait for this event before we can call getEvents with character unlock filters
		//EventDispatch.RegisterInterest("OnCharacterDataLoaded", this);
		//EventDispatch.RegisterInterest("OnStoreInitialised", this);

		StartGetEventsCorroutine ();

	}

	private void Event_OnCharacterDataLoaded(int numInSaveGame)
	{
		//mCharacterDataLoaded = true;

		StartGetEventsCorroutine ();

	}

	private void Event_OnStoreInitialised()
	{
	}

	private void ResetEventsRecieved()
	{
		mIgniteEventsRecieved = false;
		mIgniteSampleEventsRecieved = false;

	}


	void Update () 
	{
		//if (mIgniteEventsRecieved == true && mIgniteSampleEventsRecieved == true) {
		//	FactorInSampleEvents();
		//	CreateSortedEventList ();
		//	ResetEventsRecieved ();
		//}

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
