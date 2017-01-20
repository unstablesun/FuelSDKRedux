using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FuelSDKIntegration.Structures;


//-----------------------------------------------------------------
/*
	  						PUBLIC INTERFACE
*/
//-----------------------------------------------------------------
public partial class FuelIgnite : MonoBehaviour
{

	public List<IgniteEvent> GetActiveEventList {
		get {
			if( mIgniteEventList == null ||  mIgniteEventList.Count == 0 ) {
				return null;
			}
			return mIgniteEventList;
		}
	}

	public int GetNumberOfActiveEvents()
	{
		if (mIgniteEventList == null) {
			return 0;
		}

		return mIgniteEventList.Count;
	}



	public void GetEventsWithTags()
	{
		Debug.Log ("GetEventsWithTags");
		List<object> tags = GetEventTags();
		FuelSDK.GetEvents (tags);
	}







	//Get Events Coroutine
	private IEnumerator getEventsCoroutine;
	public void StartGetEventsCorroutine()
	{
		getEventsCoroutine = WaitAndGetEvents (4.0f);
		StartCoroutine (getEventsCoroutine);
	}

	public IEnumerator WaitAndGetEvents(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		GetEventsWithTags ();
	}




	public void RequestMissionEventData(string MissionId)
	{
		FuelSDK.GetMission(MissionId);
	}

	public bool RequestAllMissionEventData()
	{
		if( mIgniteEventsDictionary != null ) {
			foreach( IgniteEvent igniteEvent in mIgniteEventsDictionary.Values ) {

				RequestMissionEventData (igniteEvent.Id);
			}
			return true;
		}

		return false;

	}
		

}