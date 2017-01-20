using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FuelSDKIntegration.Structures;


public partial class FuelIgnite : MonoBehaviour
{
	//-----------------------------------------------------------------
	/*
	  							IGNITE MISC
	*/
	//-----------------------------------------------------------------



	public enum NotificationType
	{
		none 	= 0x0,
		all 	= 0x3,
		push 	= 1 << 0,
		local 	= 1 << 1
	}
	void onFuelSDKNotificationEnabled(Dictionary<string, object> data)
	{
		object notificationTypeObject;
		bool keyExists = data.TryGetValue("notificationType", out notificationTypeObject);

		if (notificationTypeObject == null || keyExists == false) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected notification type");
			return;
		}

		if (!(notificationTypeObject is long)) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid notification type data type: " + notificationTypeObject.GetType ().Name);
			return;
		}

		int notificationTypeValue = (int)((long)notificationTypeObject);

		if (!Enum.IsDefined (typeof (NotificationType), notificationTypeValue)) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "unsuppported notification type value: " + notificationTypeValue.ToString ());
			return;
		}

	}

	void onFuelSDKNotificationDisabled(Dictionary<string, object> data)
	{

		object notificationTypeObject;
		bool keyExists = data.TryGetValue ("notificationType", out notificationTypeObject);

		if (notificationTypeObject == null || keyExists == false) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected notification type");
			return;
		}

		if (!(notificationTypeObject is long)) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid notification type data type: " + notificationTypeObject.GetType ().Name);
			return;
		}

		int notificationTypeValue = (int)((long)notificationTypeObject);

		if (!Enum.IsDefined (typeof(NotificationType), notificationTypeValue)) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "unsuppported notification type value: " + notificationTypeValue.ToString ());
			return;
		}
	}

}