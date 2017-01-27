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

		//FuelIgnite.Instance.StartGetEventsCorroutine ();

	}




	
	void Update () 
	{
	
	}





	void onFuelSDKIgniteLoaded (Dictionary<string, object> data) 
	{
		Debug.Log ("REDUX LOG -------- onFuelSDKIgniteLoaded (Fuel)");


	}

	//triggered by ui button
	public void RequestEventData() 
	{
		Debug.Log ("REDUX LOG -------- RequestEventData");

		FuelIgnite.Instance.RequestEventData ();

	}

	//triggered by ui button
	public void RequestMissionData() 
	{

		FuelIgnite.Instance.RequestAllMissionEventData ();

	}

	//triggered by ui button
	public void DisplayCurrentIgniteData () 
	{
		
		int numDisplayEvents = FuelIgnite.Instance.GetNumberOfActiveEvents ();
		ReduxGuiController.Instance.addLabelAndStringToWindow ("Num Events Loaded", numDisplayEvents.ToString());

		List<IgniteEvent> igniteEventList = FuelIgnite.Instance.GetActiveEventList;
		for (int i = 0; i < igniteEventList.Count; i++) {
			EventDebugPrint ("Filtered", igniteEventList[i]);
		}

		//List<IgniteEvent> igniteEvenSampletList = FuelIgnite.Instance.GetActiveEventList;
		//for (int i = 0; i < igniteEventList.Count; i++) {
		//	EventDebugPrint ("Filtered", igniteEventList[i]);
		//}

		List<IgniteSampleEvent> igniteSampleEventList = FuelIgnite.Instance.GetSampleEventList;
		for (int i = 0; i < igniteEventList.Count; i++) {
			SampleEventDebugPrint ("Samples", igniteSampleEventList[i]);
		}

			
	}

	//triggered by ui button
	public void DisplayCurrentMissionData () 
	{
		int numDisplayEvents = FuelIgnite.Instance.GetNumberOfActiveEvents ();

		ReduxGuiController.Instance.addLabelAndStringToWindow ("Num Events Loaded", numDisplayEvents.ToString());



		List<IgniteEvent> igniteEventList = FuelIgnite.Instance.GetActiveEventList;

		for (int i = 0; i < igniteEventList.Count; i++) {

			IgniteMission igniteMission = igniteEventList [i].activity as IgniteMission;
				
			MissionDebugPrint (igniteMission);
		}

	}


	void EventDebugPrint (string title, IgniteEvent igniteEvent) 
	{

		string label = title;
		ReduxGuiController.Instance.addLabelAndStringToWindow (label, "data");
		label = "Id";
		ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.Id);
		label = "Name";
		ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.Metadata.Name);
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

		//if (igniteEvent.ComingSoon == true) {
		//	label = "Starting In";
		//	ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.RemainingStartTimeShortString);

		//} else {
		//	label = "Ending In";
		//	ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteEvent.RemainingEndTimeLongString);
		//
		//}

	}

	void SampleEventDebugPrint (string title, IgniteSampleEvent igniteSampleEvent) 
	{

		string label = title;
		ReduxGuiController.Instance.addLabelAndStringToWindow (label, "data");
		label = "Id";
		ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteSampleEvent.Id);
		label = "Name";
		ReduxGuiController.Instance.addLabelAndStringToWindow (label, igniteSampleEvent.Metadata.Name);
	}

	void MissionDebugPrint (IgniteMission igniteMission)
	{
		Dictionary<string,IgniteMissionRuleData> SubMissions = igniteMission.Rules;

		if( SubMissions != null ) {
			foreach( IgniteMissionRuleData missionRule in SubMissions.Values ) {

				string label = "sub_" + missionRule.Id;
				ReduxGuiController.Instance.addLabelAndStringToWindow (label, missionRule.Progress.ToString());

			}
		}

	}



	void GetEventButtonInfo ()
	{
		/*
		Dictionary<string, object> eventButtonData = FuelIgnite.Instance.GetEventButtonData();

		if (eventButtonData.ContainsKey ("error")) {
			ReduxGuiController.Instance.addLabelAndStringToWindow ("error", "no events");

		} else {
		

			if (eventButtonData.ContainsKey ("EventName")) {
				
				ReduxGuiController.Instance.addLabelAndStringToWindow ("EventName", eventButtonData["EventName"].ToString());
			}
			if (eventButtonData.ContainsKey ("SpecialCharacterId")) {

				ReduxGuiController.Instance.addLabelAndStringToWindow ("SpecialCharacterId", eventButtonData["SpecialCharacterId"].ToString());
			}
			if (eventButtonData.ContainsKey ("EndTime")) {

				ReduxGuiController.Instance.addLabelAndStringToWindow ("SpecialCharacterId", eventButtonData["EndTime"].ToString());
			}
		}
		*/

			

	}
		







	
}
