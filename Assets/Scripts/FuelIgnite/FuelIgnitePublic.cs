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
			#if NOT_TESTBED

			mIgniteEventList = TestFuelSDK.GetTestEventList();


			#endif

			if( mIgniteEventList == null ||  mIgniteEventList.Count == 0 ) {
				return null;
			}
			return mIgniteEventList;
		}
	}

	public IgniteEvent GetActiveEvent(){
		#if NOT_TESTBED

		mIgniteEventList = TestFuelSDK.GetTestEventList();


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








	public void GetEventsWithTags()
	{
		ResetEventsRecieved ();

		List<object> eventFiltertags = GetEventFilterTags();
		FuelSDK.GetEvents (eventFiltertags);

		List<object> sampleEventFiltertags = GetSampleEventFilterTags();
		FuelSDK.GetSampleEvents (sampleEventFiltertags);

		StartCreateEventListCoroutine ();
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




	//Create Event List Coroutine
	private IEnumerator createEventListCoroutine;
	public void StartCreateEventListCoroutine()
	{
		createEventListCoroutine = createEventList ();
		StartCoroutine (createEventListCoroutine);
	}

	public IEnumerator createEventList()
	{
		while( mIgniteEventsRecieved == false ){
			yield return null;
		}

		while( mIgniteSampleEventsRecieved == false ){
			yield return null;
		}

		FactorInSampleEvents();
		CreateSortedEventList ();
		ResetEventsRecieved ();
	}





	//--------------------------------------------------------------------
	/*
	 		Request Server (or cache) ignite data
	*/
	//--------------------------------------------------------------------

	public void RequestEventData()
	{
		WaitAndGetEvents (0f);
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