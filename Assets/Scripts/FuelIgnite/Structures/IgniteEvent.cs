using UnityEngine;
using System;
using System.Collections.Generic;
using FuelSDKIntegration.Utils;

namespace FuelSDKIntegration.Structures
{
	public enum IgniteEventType
	{
		none         	= -2,
		noactivity      = -1,
		leaderBoard 	= 0,
		mission      	= 1,
		quest        	= 2,
		offer        	= 3
	}

	public class IgniteEvent 
	{
		public string Id { get; set; }
		public DateTime StartTime { get; set; }
		public bool Authorized { get; set; }
		public bool Achieved { get; set; }
		private bool joined { get; set; }
		public string EventId { get; set; }
		public string State { get; set; }
		public float Score { get; set; }
		public IgniteEventType Type { get; set; }
		public DateTime EndTime { get; set; }
		public IgniteActivityInterface activity;
		public IgniteEventMetadata Metadata { get; set; }
		public IgniteMissionMetadata TypeMetadata { get; set; }
		//public IgniteEventVisualData VisualData { get; set; }

		//derived
		public bool EventLocked { get; set; }
		public string EventLockedNeedCaracter { get; set; }

		public IgniteEvent() {
			this.Id = string.Empty;
			this.StartTime = DateTime.MinValue;
			this.Authorized = false;
			this.Achieved = false;
			this.joined = false;
			this.EventId = string.Empty;
			this.State = string.Empty;
			this.Score = 0f;
			this.Type = IgniteEventType.none;
			this.EndTime = DateTime.MinValue;
			this.activity = null;
			this.Metadata = new IgniteEventMetadata();
			//this.VisualData = new IgniteEventVisualData( string.Empty );

			//derived
			this.EventLocked = false;
			this.EventLockedNeedCaracter = string.Empty;;
		}



		public void Create ( Dictionary<string,object> eventDict ) {
			if( eventDict.ContainsKey( "id" ) ) {
				this.Id = Convert.ToString( eventDict["id"] );
			}
			if( eventDict.ContainsKey( "startTime" ) ) {
				var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				long t = Convert.ToInt64 (eventDict["startTime"]);
				this.StartTime = epoch.AddSeconds(t);
			}
			if( eventDict.ContainsKey( "authorized" ) ) {
				this.Authorized = Convert.ToBoolean( eventDict["authorized"] );
			}
			if( eventDict.ContainsKey( "achieved" ) ) {
				this.Achieved = Convert.ToBoolean( eventDict["achieved"] );
			}
			if( eventDict.ContainsKey( "joined" ) ) {
				this.joined = Convert.ToBoolean( eventDict["joined"] );
			}
			if( eventDict.ContainsKey( "eventId" ) ) {
				this.EventId = Convert.ToString( eventDict["eventId"] );
			}
			if( eventDict.ContainsKey( "state" ) ) {
				this.State = Convert.ToString( eventDict["state"] );
			}
			if( eventDict.ContainsKey( "score" ) ) {
				this.Score = (float)Convert.ToDouble( eventDict["score"] );
			}
			if( eventDict.ContainsKey( "type" ) ) {
				this.Type = (IgniteEventType) Enum.Parse( typeof(IgniteEventType) , Convert.ToString( eventDict["type"] ) );
			}
			if( eventDict.ContainsKey( "endTime" ) ) {
				var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				long t = Convert.ToInt64 (eventDict["endTime"]);
				this.EndTime = epoch.AddSeconds(t);
			}
			if( eventDict.ContainsKey( "metadata" ) ) {
				Dictionary<string,object> eventMetadataDict = eventDict["metadata"] as Dictionary<string,object>;
				this.Metadata = new IgniteEventMetadata();
				this.Metadata.Create( eventMetadataDict );
			}

			this.activity = IgniteActivityFactory.GetActivity (this.Type);
			if( eventDict.ContainsKey( "typemetadata" ) ) {
				Dictionary<string,object> activityMetadataDict = eventDict["typemetadata"] as Dictionary<string,object>;
				this.activity.SetMetadata( activityMetadataDict );

				//add at this level as well
				this.TypeMetadata = new IgniteMissionMetadata();
				this.TypeMetadata.Create( activityMetadataDict );

			}
				
			//this.VisualData = new IgniteEventVisualData( this.Id );
		}

		public void LoadActivityData( Dictionary<string,object> dataDict ) {
			if( this.activity != null ) {
				this.activity.Create( dataDict );
			}
		}

		/*

		public bool GetVirtualGood() {
			if( this.activity != null ) {
				return this.activity.GetVirtualGood();
			}
			return false;
		}

		public bool HaveVirtualGood {
			get {
				if( this.activity != null ) {
					return this.activity.HaveVirtualGood();
				}
				return false;
			}
		}

		public bool HasParticipated {
			get {
				if( this.activity != null ) {
					return this.activity.HasParticipated();
				}
				return this.Score == -1;
			}
		}
		*/

		public bool Active {
			get {
				if( State != "active" ) {
					return false;
				}
				//if( VisualData.VirtualGoodCollected ) {
				//	return false;
				//}
				if( TimeUtility.TimeIsInThePast(StartTime) && TimeUtility.TimeIsInTheFuture(EndTime) ) {
					return true;
				}
				return false;
			}
		}

		public bool ComingSoon {
			get {
				if(TimeUtility.TimeIsInTheFuture(StartTime)) {
					return true;
				}
				return false;
			}
		}

		public bool Ended {
			get {
				if( TimeUtility.TimeIsInThePast( EndTime ) /* && !VisualData.VirtualGoodCollected */ ) {
					return true;
				}

				return false;
			}
		}

		public string RemainingEndTimeLongString {
			get {
				if( Completed &&  Ended ) {
					return "";
				}
				return TimeUtility.RemainingTimeString( this.EndTime, TimeUtility.TimeStringType.Long );
			}
		}

		public string RemainingEndTimeShortString {
			get {
				if( Completed && Ended ) {
					return "";
				}
				return TimeUtility.RemainingTimeString( this.EndTime, TimeUtility.TimeStringType.HoursMinutesSeconds );
			}
		}

		public string RemainingStartTimeShortString {
			get {
				if( Completed || Ended || Active ) {
					return "";
				}
				return TimeUtility.RemainingTimeString( this.StartTime );
			}
		}



		public bool Completed {
			get {
				if( activity != null ) {
					return activity.Completed();
				}
				return false;
			}
		}

		public bool IsCharacterEvent {
			get {
				if (Metadata.EventCharacterName != null && Metadata.EventCharacterName != string.Empty) {
					return true;
				}

				return false;
			}
		}

		public string EventCharacterName {
			get {
				return Metadata.EventCharacterName;
			}
		}

		public string PlayCharacterName {
			get {
				return Metadata.PlayCharacterName;
			}
		}

		public bool PlayCharacterEnabled {
			get {
				return Metadata.PlayCharacterEnabled;
			}
		}

		public string EventDescription {
			get {
				return TypeMetadata.Name;
			}
		}


	}

}
