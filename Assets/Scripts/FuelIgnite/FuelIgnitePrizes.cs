using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FuelSDKIntegration.Structures;


//-----------------------------------------------------------------
/*
	  				IGNITE PRIZES (virtual goods)
*/
//-----------------------------------------------------------------
public partial class FuelIgnite : MonoBehaviour
{

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

		//temp comment due to warning
		//string transactionID = (string)transactionIDObject;


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



	void onFuelSDKVirtualGoodRollback(Dictionary<string, object> data)
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


	}

	void onFuelSDKVirtualGoodConsumeSuccess(Dictionary<string, object> data)
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
	}



}