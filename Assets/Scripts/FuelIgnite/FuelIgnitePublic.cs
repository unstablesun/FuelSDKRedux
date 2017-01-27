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

	//--------------------------------------------------------------------
	/*
	 		Returns a sorted list of events
	 		The top element is the most relevant event
	*/
	//--------------------------------------------------------------------
	public List<IgniteEvent> GetActiveEventList {
		get {
			#if UNITY_EDITOR

			//mIgniteEventList = TestFuelSDK.GetTestEventList();


			#endif

			if( mIgniteEventList == null ||  mIgniteEventList.Count == 0 ) {
				return null;
			}
			return mIgniteEventList;
		}
	}

	public IgniteEvent GetActiveEvent(){
		#if UNITY_EDITOR

		//mIgniteEventList = TestFuelSDK.GetTestEventList();


		#endif
		if (mIgniteEventList.Count > 0) {
			return mIgniteEventList [0];
		}

		return null;
	}

	public int GetNumberOfActiveEvents()
	{
		if (mIgniteEventList == null) {
			return 0;
		}

		return mIgniteEventList.Count;
	}


	public List<IgniteSampleEvent> GetSampleEventList {
		get {
			#if UNITY_EDITOR

			//mIgniteEventList = TestFuelSDK.GetTestEventList();


			#endif

			if( mIgniteSampleEventList == null ||  mIgniteSampleEventList.Count == 0 ) {
				return null;
			}
			return mIgniteSampleEventList;
		}
	}






	public void GetEventsWithTags()
	{
		Debug.Log ("REDUX LOG -------- GetEventsWithTags");

		Debug.Log ("GetEventsWithTags");

		List<object> eventFiltertags = GetEventFilterTags();
		FuelSDK.GetEvents (eventFiltertags);

		List<object> sampleEventFiltertags = GetSampleEventFilterTags ();
		FuelSDK.GetSampleEvents (sampleEventFiltertags);
	}



	//Get Events Coroutine
	private IEnumerator getEventsCoroutine;
	public void StartGetEventsCorroutine()
	{
		getEventsCoroutine = WaitAndGetEvents (1.0f);
		StartCoroutine (getEventsCoroutine);
	}

	public IEnumerator WaitAndGetEvents(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		GetEventsWithTags ();
	}








	//--------------------------------------------------------------------
	/*
	 		Request Server (or cache) ignite data
	*/
	//--------------------------------------------------------------------

	public void RequestEventData()
	{
		Debug.Log ("REDUX LOG -------- RequestEventData");
		//WaitAndGetEvents (0f);

		StartGetEventsCorroutine ();
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