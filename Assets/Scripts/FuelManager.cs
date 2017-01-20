using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FuelSDKIntegration.Structures;

public class FuelManager : MonoBehaviour 
{

	public static FuelManager Instance;


	void OnDestroy() 
	{
		Instance = null;
	}


	void Awake()
	{
		Instance = this;


	}


	void Start () 
	{

		FuelIgnite.Instance.StartGetEventsCorroutine ();

	}




	
	void Update () 
	{
	
	}





	void onFuelSDKIgniteLoaded (Dictionary<string, object> data) 
	{
		Debug.Log ("REDUX LOG -------- onFuelSDKIgniteLoaded (Fuel)");


	}

	//triggered by ui button
	public void RequestMissionData() 
	{

		FuelIgnite.Instance.RequestAllMissionEventData ();

	}

	//triggered by ui button
	public void GetCurrentIgniteList () 
	{
		
		int numDisplayEvents = FuelIgnite.Instance.GetNumberOfActiveEvents ();

		ReduxGuiController.Instance.addLabelAndStringToWindow ("Num Events Loaded", numDisplayEvents.ToString());


	}


	void EventDebugPrint (IgniteEvent igniteEvent) 
	{



		string label = "Id";
		ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.Id);
		label = "EventId";
		ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.EventId);
		label = "State";
		ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.State);
		label = "Score";
		ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.Score.ToString());
		//label = "StartTime";
		//ReduxGuiController.Instance.addLabelAndDateTimeToWindow (label, igniteEvent.StartTime);
		//label = "EndTime";
		//ReduxGuiController.Instance.addLabelAndDateTimeToWindow (label, igniteEvent.EndTime);

		//label = "Special Character";
		//ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.Metadata.SpecialCharacterId.ToString());

		if (igniteEvent.ComingSoon == true) {
			label = "Starting In";
			ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.RemainingStartTimeShortString);

		} else {
			label = "Ending In";
			ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.RemainingEndTimeLongString);

		}


		//FuelSDK.GetMission (igniteEvent.Id);




		/*
		//Rules are effectively Sub Missions
		Dictionary<string,IgniteMissionRuleData> SubMissions = igniteMission.Rules;

		if( SubMissions != null ) {
			foreach( IgniteMissionRuleData missionRule in SubMissions.Values ) {

				//label = "sub_" + missionRule.Id;
				//ReduxGuiController.Instance.addLabelAndStringToWindow (label, missionRule.Progress.ToString());

			}
		}
		*/

	}







	
}
