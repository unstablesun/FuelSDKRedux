using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FuelSDKIntegration.Structures;


//-----------------------------------------------------------------
/*
	  					IGNITE MISSIONS
*/
//-----------------------------------------------------------------
public partial class FuelIgnite : MonoBehaviour
{

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


		//IgniteMission igniteMission = new IgniteMission ();
		//igniteMission.Create (missionDictionary);

		IgniteEvent igniteEvent = mIgniteEventsDictionary[missionDictionary["id"].ToString()];
		if (igniteEvent != null) {
			igniteEvent.LoadActivityData (missionDictionary);

		} else {
		
		}



	}



}