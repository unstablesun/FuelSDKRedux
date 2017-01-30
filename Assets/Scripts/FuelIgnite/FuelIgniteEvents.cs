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

			IgniteEvent igniteSampleEvent = new IgniteEvent ();
			igniteSampleEvent.Create (eventObj as Dictionary<string,object>);

			mIgniteSampleEventList.Add (igniteSampleEvent);
		}

		mIgniteSampleEventsRecieved = true;
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



		foreach (object eventObj in eventList) {

			IgniteEvent igniteEvent = new IgniteEvent ();
			igniteEvent.Create (eventObj as Dictionary<string,object>);

			mIgniteEventsDictionary [igniteEvent.Id] = igniteEvent;

		}
			
		mIgniteEventsRecieved = true;
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

		//add character unlocks
		#if REDUX_TESTBED

		//test filter A
		string characterCohort = "unlocked_character_sonic";
		eventFilterTags.Add (characterCohort);
		characterCohort = "unlocked_character_tails";
		eventFilterTags.Add (characterCohort);
		characterCohort = "locked_character_knuckles";
		eventFilterTags.Add (characterCohort);
		characterCohort = "locked_character_amy";
		eventFilterTags.Add (characterCohort);
		characterCohort = "locked_character_shadow";
		eventFilterTags.Add (characterCohort);
		characterCohort = "locked_character_blaze";
		eventFilterTags.Add (characterCohort);

		#else
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



		return eventFilterTags;
	}

	private List<object> GetSampleEventFilterTags () {

		List<object> eventFilterTags = new List<object>();

		//example: for any sequence 1-unlocked 2-locked add another sequence 2-unlocked 3-locked (if 3 is locked)
		//this will query for the next locked event

		#if REDUX_TESTBED

		//test filter A
		string characterCohort = "unlocked_character_sonic";
		eventFilterTags.Add (characterCohort);
		characterCohort = "unlocked_character_tails";
		eventFilterTags.Add (characterCohort);
		characterCohort = "locked_character_knuckles";
		eventFilterTags.Add (characterCohort);
		characterCohort = "locked_character_amy";
		eventFilterTags.Add (characterCohort);
		characterCohort = "locked_character_shadow";
		eventFilterTags.Add (characterCohort);
		characterCohort = "locked_character_blaze";
		eventFilterTags.Add (characterCohort);

		characterCohort = "unlocked_character_knuckles";
		eventFilterTags.Add (characterCohort);


		#else

		int characterCount = Utils.GetEnumCount<Characters.Type>();
		bool[]	character_unlockState = new bool[characterCount];

		for (int c = 0; c < characterCount; c++) {

			if (Characters.CharacterUnlocked ((Characters.Type)c)) {
				character_unlockState [c] = true;
			} else {
				character_unlockState [c] = false;
			}
		}

		//add initial character unlocks
		for (int c = 0; c < characterCount; c++) {

			if (character_unlockState[c] == true) {

				string characterUnlockCohort = "unlocked_" + Characters.IDStrings [c];
				eventFilterTags.Add (characterUnlockCohort);

			} else {

				string characterLockCohort = "locked_" + Characters.IDStrings [c];
				eventFilterTags.Add (characterLockCohort);

			}
		}

		//add additional character locks
		for (int c = 0; c < characterCount - 3; c++) {

			if (character_unlockState[c] == true && character_unlockState[c+1] == false) { 

				for (int k = c; k < characterCount - 3; k++) {

					if (character_unlockState [c + 2] == false) {
					
						//item is locked see if there is a 


					} else {
					
					}	

				}
				//string characterUnlockCohort = "unlocked_" + Characters.IDStrings [c+1];
				//eventFilterTags.Add (characterUnlockCohort);

			}
		}

		#endif


		return eventFilterTags;
	}



	private void FactorInSampleEvents()
	{
		Debug.Log ("REDUX LOG - FactorInSampleEvents");

		for (int e = 0; e < mIgniteSampleEventList.Count; e++) {  

			IgniteEvent igniteEvent = mIgniteEventsDictionary[mIgniteSampleEventList[e].Id];

			if (igniteEvent == null) {
			
				//sample event is not in list so add it
				IgniteEvent igniteSampleEvent = mIgniteSampleEventList[e];

				Debug.Log ("REDUX LOG - igniteSampleEvent.EventLocked = true");
				igniteSampleEvent.EventLocked = true;
				mIgniteEventsDictionary.Add (igniteSampleEvent.Id, igniteSampleEvent);
				break;//just add 1 sample event? yes for now
			}
		}

	}

	private void CreateSortedEventList()
	{
		//Find first Character event
		mIgniteEventList = new List<IgniteEvent> ();

		foreach(IgniteEvent e in mIgniteEventsDictionary.Values){
			if(e.IsCharacterEvent == true) {
				mIgniteEventList.Add( e );
			}
		}

		foreach(IgniteEvent e in mIgniteEventsDictionary.Values){
			if(e.IsCharacterEvent == false && e.Active == true) {
				mIgniteEventList.Add( e );
			}
		}

		foreach(IgniteEvent e in mIgniteEventsDictionary.Values){
			if(e.IsCharacterEvent == false && e.Active == true) {
				mIgniteEventList.Add( e );
			}
		}

		foreach(IgniteEvent e in mIgniteEventsDictionary.Values){
			if(e.IsCharacterEvent == false && e.Ended == true) {
				mIgniteEventList.Add( e );
			}
		}

		foreach(IgniteEvent e in mIgniteEventsDictionary.Values){
			if(e.IsCharacterEvent == false && e.ComingSoon == true) {
				mIgniteEventList.Add( e );
			}
		}

	}
		
		


}