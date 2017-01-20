using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FuelSDKIntegration.Structures;


public partial class FuelIgnite : MonoBehaviour
{
	//-----------------------------------------------------------------
	/*
	  							IGNITE EVENTS
	*/
	//-----------------------------------------------------------------

	void onFuelSDKIgniteEvents(Dictionary<string, object> data)
	{
		Debug.Log ("onFuelSDKIgniteEvents");

		mIgniteEventsDictionary = new Dictionary<string, IgniteEvent> ();

		object eventsObject;
		bool keyExists = data.TryGetValue("events", out eventsObject);

		if (eventsObject == null || keyExists == false) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected event list");
			return;
		}

		List<object> eventList = null;

		try{
			eventList = eventsObject as List<object>;

			if (eventList == null) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid event list data type: " + eventsObject.GetType ().Name);
				return;
			}
		}catch(Exception e){

			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid event list data type: " + eventsObject.GetType ().Name + " error message : " + e.Message);
			return;
		}


		mIgniteEventList = new List<IgniteEvent> ();


		foreach (object eventObj in eventList) {

			IgniteEvent igniteEvent = new IgniteEvent ();
			igniteEvent.Create (eventObj as Dictionary<string,object>);

			mIgniteEventList.Add (igniteEvent);
			mIgniteEventsDictionary [igniteEvent.Id] = igniteEvent;

		}
			
	}


	public void SendProgress() 
	{
		Dictionary<string,object> progressDict = new Dictionary<string, object>();

		//Rings Collected
		Dictionary<string,int> ringsCollectedDict = new Dictionary<string,int>();
		ringsCollectedDict ["value"] = 100;
		progressDict["RingsCollected"] = ringsCollectedDict;

		//Red Rings Collected
		Dictionary<string,int> redRingsCollectedDict = new Dictionary<string,int>();
		redRingsCollectedDict ["value"] = 1;
		progressDict["RedRingsCollected"] = redRingsCollectedDict;

		List<object> tags = new List<object>();
		FuelSDK.SendProgress( progressDict, tags );

		ReduxGuiController.Instance.addTextToWindow ("_*!Progress Sent!*_");

	}



	private List<object> GetEventTags () {
		List<object> tags = new List<object>();

		tags.Add("UserLevel5");

		return tags;
	}



}