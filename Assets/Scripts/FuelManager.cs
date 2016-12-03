using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FuelManager : MonoBehaviour 
{


	static FuelManager s_instance;

	public static FuelManager Instance
	{
		get
		{
			return s_instance as FuelManager;
		}
	}

	void OnDestroy() 
	{
		s_instance = null;
	}


	void Awake()
	{
		
	}


	//Get Events
	private IEnumerator getEventsCoroutine;
	void Start () 
	{
		getEventsCoroutine = WaitAndGetEvents (4.0f);
		StartCoroutine (getEventsCoroutine);
	}

	public IEnumerator WaitAndGetEvents(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		FuelSDK.GetEvents (null);
	}

	//Get Missions
	private IEnumerator getEventsMissions;
	public IEnumerator WaitAndGetMissions(float waitTime, string missionID)
	{
		yield return new WaitForSeconds(waitTime);
		FuelSDK.GetMission (missionID);
	}


	
	void Update () 
	{
	
	}




	void OnEnable()
	{
		FuelSDK.broadcastFuelSDKIgniteLoaded += onFuelSDKIgniteLoaded;
		FuelSDK.broadcastFuelSDKIgniteEvents += onFuelSDKIgniteEvents;
		FuelSDK.broadcastFuelSDKIgniteMission += onFuelSDKIgniteMission;
	}

	void OnDisable()
	{
		FuelSDK.broadcastFuelSDKIgniteLoaded -= onFuelSDKIgniteLoaded;
		FuelSDK.broadcastFuelSDKIgniteEvents -= onFuelSDKIgniteEvents;
		FuelSDK.broadcastFuelSDKIgniteMission -= onFuelSDKIgniteMission;

	}


	void onFuelSDKIgniteLoaded (string message) 
	{
		Debug.Log ("REDUX LOG -------- onFuelSDKIgniteLoaded (Fuel)");


	}



	public enum IgniteEventType
	{
		none         	= -2,
		noactivity      = -1,
		leaderBoard 	= 0,
		mission      	= 1,
		quest        	= 2,
		offer        	= 3
	}

	void onFuelSDKIgniteEvents(Dictionary<string, object> data)
	{
		Debug.Log ("REDUX LOG -------- onFuelSDKIgniteEvents (Fuel)");


		object eventsObject;
		bool keyExists = data.TryGetValue("events", out eventsObject);

		if (eventsObject == null || keyExists == false) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected event list");
			return;
		}

		List<object> events = null;

		try{
			events = eventsObject as List<object>;

			if (events == null) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid event list data type: " + eventsObject.GetType ().Name);
				return;
			}
		}catch(Exception e){

			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid event list data type: " + eventsObject.GetType ().Name + " error message : " + e.Message);
			return;
		}

		//process events list
		Debug.Log ("REDUX LOG -------- onFuelSDKIgniteEvents (Fuel) : Process Event List");

		foreach (object eventObject in events) {
		
			Dictionary<string,object> eventDict =  (eventObject as Dictionary<string,object>);

			if( eventDict.ContainsKey( "id" ) ) {
				string Id = Convert.ToString( eventDict["id"] );

				Debug.Log ("    redux log ---- Event Id = " + Id);
			}
				
			if( eventDict.ContainsKey( "startTime" ) ) {
				var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				long t = Convert.ToInt64 (eventDict["startTime"]);
				DateTime StartTime = epoch.AddSeconds(t);
				Debug.Log ("    redux log ---- StartTime = " + StartTime);
			}
			if( eventDict.ContainsKey( "authorized" ) ) {
				bool Authorized = Convert.ToBoolean( eventDict["authorized"] );
				Debug.Log ("    redux log ---- Authorized = " + Authorized);
			}
			if( eventDict.ContainsKey( "achieved" ) ) {
				bool Achieved = Convert.ToBoolean( eventDict["achieved"] );
				Debug.Log ("    redux log ---- Achieved = " + Achieved);
			}
			if( eventDict.ContainsKey( "joined" ) ) {
				bool joined = Convert.ToBoolean( eventDict["joined"] );
				Debug.Log ("    redux log ---- joined = " + joined);
			}
			if( eventDict.ContainsKey( "eventId" ) ) {
				string EventId = Convert.ToString( eventDict["eventId"] );
				Debug.Log ("    redux log ---- EventId = " + EventId);
			}
			if( eventDict.ContainsKey( "state" ) ) {
				string State = Convert.ToString( eventDict["state"] );
				Debug.Log ("    redux log ---- State = " + State);
			}
			if( eventDict.ContainsKey( "score" ) ) {
				double Score = (float)Convert.ToDouble( eventDict["score"] );
				Debug.Log ("    redux log ---- Score = " + Score);
			}
			if( eventDict.ContainsKey( "type" ) ) {
				IgniteEventType Type = (IgniteEventType) Enum.Parse( typeof(IgniteEventType) , Convert.ToString( eventDict["type"] ) );
				Debug.Log ("    redux log ---- Type = " + Type);

				if (Type == IgniteEventType.mission) {
				
					string missionId = Convert.ToString( eventDict["id"] );

					getEventsMissions = WaitAndGetMissions (1.0f, missionId);
					StartCoroutine (getEventsMissions);

				}
			}
			if( eventDict.ContainsKey( "endTime" ) ) {
				var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				long t = Convert.ToInt64 (eventDict["endTime"]);
				DateTime EndTime = epoch.AddSeconds(t);
				Debug.Log ("    redux log ---- EndTime = " + EndTime);
			}
			if( eventDict.ContainsKey( "metadata" ) ) {
				Dictionary<string,object> eventMetadataDict = eventDict["metadata"] as Dictionary<string,object>;
			}


		
		}

	}



	void onFuelSDKIgniteMission(Dictionary<string, object> data)
	{
		Debug.Log ("REDUX LOG -------- onFuelSDKIgniteMission (Fuel)");


		object missionObject;
		bool keyExists = data.TryGetValue ("mission", out missionObject);

		if (missionObject == null || keyExists == false) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected mission data");
			return;
		}

		Dictionary<string, object> mission = null;

		try {
			mission = missionObject as Dictionary<string, object>;

			if (mission == null) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid mission data type: " + missionObject.GetType ().Name);
				return;
			}
		} catch (Exception e) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid mission data type: " + missionObject.GetType ().Name + " error message : " + e.Message);
			return;
		}


		if( mission.ContainsKey( "id" ) ) {
			string Id = Convert.ToString( mission["id"] );

			Debug.Log ("    redux log ---- Id = " + Id);

		}

		if( mission.ContainsKey( "progress" ) ) {
			float Progress = (float)Math.Round( Convert.ToDouble(mission["progress"]), 2);

			Debug.Log ("    redux log ---- Progress = " + Progress);

		}


	}


}
