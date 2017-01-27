using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FuelSDKIntegration.Structures;


//-----------------------------------------------------------------
/*
	  						IGNITE EVENTS
*/
//-----------------------------------------------------------------
public partial class FuelIgnite : MonoBehaviour
{

	void onFuelSDKIgniteSampleEvents(Dictionary<string, object> data)
	{
		object eventsObject;
		bool keyExists = data.TryGetValue("events", out eventsObject);

		if (eventsObject == null || keyExists == false) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected event list");
			return;
		}

		List<object> sampleEventList = null;

		try{
			sampleEventList = eventsObject as List<object>;

			if (sampleEventList == null) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid event list data type: " + eventsObject.GetType ().Name);
				return;
			}
		}catch(Exception e){

			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid event list data type: " + eventsObject.GetType ().Name + " error message : " + e.Message);
			return;
		}


		foreach (object eventObj in sampleEventList) {

			IgniteSampleEvent igniteSampleEvent = new IgniteSampleEvent ();
			igniteSampleEvent.Create (eventObj as Dictionary<string,object>);

			mIgniteSampleEventList.Add (igniteSampleEvent);
		}
	}


	void onFuelSDKIgniteEvents(Dictionary<string, object> data)
	{
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "FuelIgnite : onFuelSDKIgniteEvents");

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

		//temp
		RequestAllMissionEventData ();
			
	}




	Dictionary<string,object> progressDictionary = new Dictionary<string, object>();


	public void ResetProgressDictionary() 
	{
		progressDictionary = new Dictionary<string, object>();

	}

	public void SetRingsCollectedProgress(int rings) 
	{
		Dictionary<string,int> ringsCollectedDict = new Dictionary<string,int>();
		ringsCollectedDict ["value"] = rings;
		progressDictionary["RingsCollected"] = ringsCollectedDict;
	}

	public void SetRedRingsCollectedProgress(int redRings) 
	{
		Dictionary<string,int> redRingsCollectedDict = new Dictionary<string,int>();
		redRingsCollectedDict ["value"] = redRings;
		progressDictionary["RedRingsCollected"] = redRingsCollectedDict;
	}

		
		
	public void SendProgress() 
	{
		ResetProgressDictionary ();

		//Rings Collected
		SetRingsCollectedProgress (100);

		//Red Rings Collected
		SetRedRingsCollectedProgress (100);

		//Distance Run
		Dictionary<string,int> distanceRunDict = new Dictionary<string,int>();
		distanceRunDict ["value"] = 500;
		progressDictionary["DistanceRunDict"] = distanceRunDict;

		//Score
		Dictionary<string,int> scoreDict = new Dictionary<string,int>();
		scoreDict ["value"] = 500;
		progressDictionary["ScoreDict"] = scoreDict;

		//Destroy Enemies
		Dictionary<string,int> destroyEnemiesDict = new Dictionary<string,int>();
		destroyEnemiesDict ["value"] = 500;
		progressDictionary["DestroyEnemiesDict"] = destroyEnemiesDict;

		//Destroy Crabs
		Dictionary<string,int> destroyCrabsDict = new Dictionary<string,int>();
		destroyCrabsDict ["value"] = 25;
		progressDictionary["DestroyCrabsDict"] = destroyCrabsDict;

		//Destroy Motor Bugs
		Dictionary<string,int> destroyMotorbugsDict = new Dictionary<string,int>();
		destroyMotorbugsDict ["value"] = 25;
		progressDictionary["DestroyMotorbugsDict"] = destroyMotorbugsDict;

		//Lane Changes
		Dictionary<string,int> laneChangesDict = new Dictionary<string,int>();
		laneChangesDict ["value"] = 25;
		progressDictionary["LaneChangesDict"] = laneChangesDict;

		//Dashes Used
		Dictionary<string,int> dashesUsedDict = new Dictionary<string,int>();
		dashesUsedDict ["value"] = 9;
		progressDictionary["DashesUsedDict"] = dashesUsedDict;

		//Jump Obstacle
		Dictionary<string,int> jumpObstacleDict = new Dictionary<string,int>();
		jumpObstacleDict ["value"] = 9;
		progressDictionary["JumpObstacleDict"] = jumpObstacleDict;

		//Jump Enemy
		Dictionary<string,int> jumpEnemyDict = new Dictionary<string,int>();
		jumpEnemyDict ["value"] = 9;
		progressDictionary["JumpEnemyDict"] = jumpEnemyDict;

		//Roll Under
		Dictionary<string,int> rollUnderDict = new Dictionary<string,int>();
		rollUnderDict ["value"] = 9;
		progressDictionary["RollUnderDict"] = rollUnderDict;

		//Roll Distance
		Dictionary<string,int> rollDistanceDict = new Dictionary<string,int>();
		rollDistanceDict ["value"] = 9;
		progressDictionary["RollDistanceDict"] = rollDistanceDict;

		List<object> ruleFilterTags = new List<object>();
		FuelSDK.SendProgress( progressDictionary, ruleFilterTags );


	}



	private List<object> GetEventFilterTags () {
		
		List<object> eventFilterTags = new List<object>();

		#if NOT_TESTBED
		//add character unlocks
		int characterTypeLength = Utils.GetEnumCount<Characters.Type>();

		for (int c = 0; c < characterTypeLength; c++) {

			if (Characters.CharacterUnlocked ((Characters.Type)c)) {

				string characterUnlockCohort = "unlocked_" + Characters.IDStrings [c];
				eventFilterTags.Add (characterUnlockCohort);

			} else {
			
				string characterLockCohort = "locked_" + Characters.IDStrings [c];
				eventFilterTags.Add (characterLockCohort);
			
			}

		}
		#endif



		string characterCohort = "unlocked_character_sonic";
		eventFilterTags.Add (characterCohort);

		characterCohort = "locked_character_tails";
		eventFilterTags.Add (characterCohort);


		return eventFilterTags;
	}

	private List<object> GetSampleEventFilterTags () {

		List<object> eventFilterTags = new List<object>();

		#if NOT_TESTBED
		//add character unlocks
		int characterTypeLength = Utils.GetEnumCount<Characters.Type>();

		for (int c = 0; c < characterTypeLength; c++) {

		if (Characters.CharacterUnlocked ((Characters.Type)c)) {

		string characterUnlockCohort = "unlocked_" + Characters.IDStrings [c];
		eventFilterTags.Add (characterUnlockCohort);

		} else {

		string characterLockCohort = "locked_" + Characters.IDStrings [c];
		eventFilterTags.Add (characterLockCohort);

		}

		}
		#endif



		//string characterCohort = "unlocked_character_sonic";
		//eventFilterTags.Add (characterCohort);

		//characterCohort = "locked_character_tails";
		//eventFilterTags.Add (characterCohort);

		//Query - will return just the locked events
		string characterCohort = "unlocked_character_tails";
		eventFilterTags.Add (characterCohort);

		characterCohort = "locked_character_knuckles";
		eventFilterTags.Add (characterCohort);

		return eventFilterTags;
	}




}