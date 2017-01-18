using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FuelSDKIntegration.Structures;


public class FuelIgnite : MonoBehaviour
{

	#region ____________REGIONS______________
	#endregion

	//static FuelManager s_instance;

	public static FuelIgnite Instance;


	void OnDestroy() 
	{
		Instance = null;
	}


	void Awake()
	{
		Instance = this;
		Debug.Log ("+====+ DAVES LOG: FuelIgnite Awake!");

	}

	void Start () 
	{

		if (UnityEngine.iOS.NotificationServices.deviceToken != null) {
			Debug.Log ("+====+ DAVES LOG: deviceToken exists!");
		} else {
			Debug.Log ("+====+ DAVES LOG: deviceToken exists!");
		}

	}


	void OnEnable()
	{
		FuelSDK.broadcastFuelSDKIgniteEvents += onFuelSDKIgniteEvents;
		FuelSDK.broadcastFuelSDKIgniteMission += onFuelSDKIgniteMission;
		FuelSDK.broadcastFuelSDKVirtualGoodList += onFuelSDKVirtualGoodList;

	}

	void OnDisable()
	{
		FuelSDK.broadcastFuelSDKIgniteEvents -= onFuelSDKIgniteEvents;
		FuelSDK.broadcastFuelSDKIgniteMission -= onFuelSDKIgniteMission;
		FuelSDK.broadcastFuelSDKVirtualGoodList -= onFuelSDKVirtualGoodList;

	}


	public void BootStrap()
	{
		ReduxGuiController.Instance.addTextToWindow ("FuelIgnite BootStrap");
	}

	public void RefreshMission1()
	{
	}
	public void RefreshMission2()
	{
	}
	public void RefreshMission3()
	{
	}
	public void RefreshMission4()
	{
	}

	void onFuelSDKIgniteEvents(Dictionary<string, object> data)
	{

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


		List<IgniteEvent> completeEventList = new List<IgniteEvent> ();


		foreach (object eventObj in eventList) {

			IgniteEvent igniteEvent = new IgniteEvent ();
			igniteEvent.Create (eventObj as Dictionary<string,object>);

			completeEventList.Add (igniteEvent);

			string label = "Id";
			ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.Id);
			label = "EventId";
			ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.EventId);
			//label = "State";
			//ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.State);
			//label = "Score";
			//ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.Score.ToString());
			//label = "StartTime";
			//ReduxGuiController.Instance.addLabelAndDateTimeToWindow (label, igniteEvent.StartTime);
			//label = "EndTime";
			//ReduxGuiController.Instance.addLabelAndDateTimeToWindow (label, igniteEvent.EndTime);

			label = "Special Character";
			ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.Metadata.SpecialCharacterId.ToString());

			if (igniteEvent.ComingSoon == true) {
				label = "Starting In";
				ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.RemainingStartTimeShortString);
			
			} else {
				label = "Ending In";
				ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.RemainingEndTimeShortString);

			}


			//FuelSDK.GetMission (igniteEvent.Id);


		}
			


	}



	void onFuelSDKIgniteMission(Dictionary<string, object> data)
	{
		object missionObject;
		bool keyExists = data.TryGetValue("mission", out missionObject);

		if (missionObject == null || keyExists == false) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected mission data");
			return;
		}

		Dictionary<string, object> missionDictionary = null;

		try{
			missionDictionary = missionObject as Dictionary<string, object>;

			if (missionDictionary == null) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid mission data type: " + missionObject.GetType ().Name);
				return;
			}
		}catch(Exception e){
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid mission data type: " + missionObject.GetType ().Name + " error message : " + e.Message);
			return;
		}


		IgniteMission igniteMission = new IgniteMission ();
		igniteMission.Create (missionDictionary);

		ReduxGuiController.Instance.addTextToWindow ("------------Mission Data-------------");

		string label = "Id";
		ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteMission.Id);

		//label = "Progress";
		//ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteMission.Progress.ToString());


		//Rules are effectively Sub Missions
		Dictionary<string,IgniteMissionRuleData> SubMissions = igniteMission.Rules;

		if( SubMissions != null ) {
			foreach( IgniteMissionRuleData missionRule in SubMissions.Values ) {

				label = "sub_" + missionRule.Id;
				ReduxGuiController.Instance.addLabelAndStringToWindow (label, missionRule.Progress.ToString());

			}
		}

	}
		





	void onFuelSDKVirtualGoodList(Dictionary<string, object> data)
	{

		object transactionIDObject;
		bool keyExists = data.TryGetValue("transactionID", out transactionIDObject);

		if (transactionIDObject == null || keyExists == false) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected transaction ID");
			return;
		}

		if (!(transactionIDObject is string)) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid transaction ID data type: " + transactionIDObject.GetType ().Name);
			return;
		}

		string transactionID = (string)transactionIDObject;


		object virtualGoodsObject;
		keyExists = data.TryGetValue("virtualGoods", out virtualGoodsObject);

		if (virtualGoodsObject == null || keyExists == false) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected virtual goods list");
			return;
		}

		List<object> virtualGoods = null;

		try{

			virtualGoods = virtualGoodsObject as List<object>;

			if (virtualGoods == null) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid virtual goods list data type: " + virtualGoodsObject.GetType ().Name);
				return;
			}

		}catch(Exception e){
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid virtual goods list data type: " + virtualGoodsObject.GetType ().Name + " error message : " + e.Message);
			return;
		}

		//OnPropellerSDKVirtualGoodList (transactionID, virtualGoods);

		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.INFO, "FUEL DEBUG LOG ::: FuelManager:: onFuelSDKVirtualGoodList");

	}


	public void SendProgress() 
	{
		Dictionary<string,object> progressDict = new Dictionary<string, object>();

		//Rings Collected
		Dictionary<string,int> ringsCollectedDict = new Dictionary<string,int>();
		ringsCollectedDict ["value"] = 100;
		progressDict["RingsCollected"] = ringsCollectedDict;

		//Rings Collected
		Dictionary<string,int> redRingsCollectedDict = new Dictionary<string,int>();
		redRingsCollectedDict ["value"] = 1;
		progressDict["RedRingsCollected"] = redRingsCollectedDict;

		List<object> tags = new List<object>();
		FuelSDK.SendProgress( progressDict, tags );

		ReduxGuiController.Instance.addTextToWindow ("_*!Progress Sent!*_");

	}


	//-----------------------------------------------------------------
	/*
	  							PUBLIC INTERFACE
	*/
	//-----------------------------------------------------------------

	public int GetNumberOfDisplayEvents()
	{
		return 0;
	}
		
}
