using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FuelSDKIntegration.Structures;


public partial class FuelIgnite : MonoBehaviour
{
	//-----------------------------------------------------------------
	/*
	  							IGNITE DYNAMICS
	*/
	//-----------------------------------------------------------------









	//------------------------------------------------------------------------------
	/*
	  									UserValues
	*/
	//------------------------------------------------------------------------------
	void onFuelSDKUserValues(Dictionary<string, object> data)
	{

		object conditionsObject;
		bool keyExists = data.TryGetValue("dynamicConditions", out conditionsObject);

		if (conditionsObject == null || keyExists == false) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected dynamic conditions");
			return;
		}

		Dictionary<string, object> conditions = null;

		try{
			conditions = conditionsObject as Dictionary<string, object>;

			if (conditions == null) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid conditions data type: " + conditionsObject.GetType ().Name);
				return;
			}
		}catch(Exception e){
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid conditions data type: " + conditionsObject.GetType ().Name + " error message : " + e.Message);
			return;
		}

		object variablesObject;
		keyExists = data.TryGetValue("variables", out variablesObject);

		if (variablesObject == null || keyExists == false) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected dynamic variables");
			return;
		}

		Dictionary<string, object> variables = null;

		try{
			variables = variablesObject as Dictionary<string, object>;

			if (variables == null) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid variables data type: " + variablesObject.GetType ().Name);
				return;
			}
		}catch(Exception e){
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid variables data type: " + variablesObject.GetType ().Name + " error message : " + e.Message);
			return;
		}

	}

}