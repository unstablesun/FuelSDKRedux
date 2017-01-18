using UnityEngine;
using System.Collections;
using System;

namespace FuelSDKIntegration.Utils
{
	public static class TimeUtility {

		public enum TimeStringType 
		{
			Short = 0,
			Long = 1,
			HoursMinutesSeconds = 3
		}

		public static long UnixTimeNow()
		{
			return (long) (DateTime.UtcNow.Subtract (new DateTime (1970, 1, 1))).TotalSeconds;
		}

		public static DateTime FromUnixTime(long unixTime)
		{
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return epoch.AddSeconds(unixTime);
		}

		public static bool TimeIsInThePast( DateTime time ) 
		{
			if (time.CompareTo (DateTime.UtcNow) <= 0) 
			{
				return true;
			}
			return false;
		}

		public static bool TimeIsInTheFuture( DateTime time ) 
		{
			if (time.CompareTo (DateTime.UtcNow) >= 0) 
			{
				return true;
			}
			return false;
		}

		public static string RemainingTimeString ( DateTime time , TimeStringType type = TimeStringType.Short ) 
		{
			int days=0, hours=0, minutes=0, seconds=0;
			if (TimeIsInTheFuture (time)) 
			{
				TimeSpan remainingTime = TimeSpan.FromSeconds(time.Subtract( DateTime.UtcNow ).TotalSeconds);
				days = remainingTime.Days;
				hours = remainingTime.Hours;
				minutes = remainingTime.Minutes;
				seconds = remainingTime.Seconds;
			}
			return GetTimeString( days, hours, minutes, seconds, type );
		}
			
		public static string ElapsedTimeString ( DateTime time , TimeStringType type = TimeStringType.Short ) 
		{
			int days=0, hours=0, minutes=0, seconds=0;
			if (TimeIsInThePast (time)) 
			{
				TimeSpan elapsedTime = TimeSpan.FromSeconds(DateTime.UtcNow.Subtract( time ).TotalSeconds);
				days = elapsedTime.Days;
				hours = elapsedTime.Hours;
				minutes = elapsedTime.Minutes;
				seconds = elapsedTime.Seconds;
			}
			return GetTimeString( days, hours, minutes, seconds, type );
		}

		private static string GetTimeString( int days=0, int hours=0, int minutes=0, int seconds=0, TimeStringType type = TimeStringType.Short ) 
		{
			string result = "";
			switch ( type ) {
			case TimeStringType.Short:
				if( days > 0 ) 
				{
					result = ((days < 10) ? "0" + days.ToString () : days.ToString ())+ "d "+
						((hours < 10) ? "0" + hours.ToString () : hours.ToString ()) + "h";
				}
				else if( hours > 0 ) 
				{
					result = ((hours < 10) ? "0" + hours.ToString () : hours.ToString ())+ "h "+
						((minutes < 10) ? "0" + minutes.ToString () : minutes.ToString ()) + "m";
				}
				else if( minutes > 0 ) 
				{
					result = ((minutes < 10) ? "0" + minutes.ToString () : minutes.ToString ())+ "m "+
						((seconds < 10) ? "0" + seconds.ToString () : seconds.ToString ()) + "s";
				}
				else {
					result =  ((seconds < 10) ? "0" + seconds.ToString () : seconds.ToString ()) + "s";
				}
				break;
			case TimeStringType.Long:
				if( days > 0 ) 
				{
					result = ((days < 10) ? "0" + days.ToString () : days.ToString ())+ "d "+
						((hours < 10) ? "0" + hours.ToString () : hours.ToString ()) + "h"+
						((minutes < 10) ? "0" + minutes.ToString () : minutes.ToString ()) + "m";
				}
				else if( hours > 0 ) 
				{
					result = ((hours < 10) ? "0" + hours.ToString () : hours.ToString ())+ "h "+
						((minutes < 10) ? "0" + minutes.ToString () : minutes.ToString ()) + "m "+
						((seconds < 10) ? "0" + seconds.ToString () : seconds.ToString ()) + "s";
				}
				else if( minutes > 0 ) 
				{
					result = ((minutes < 10) ? "0" + minutes.ToString () : minutes.ToString ())+ "m "+
						((seconds < 10) ? "0" + seconds.ToString () : seconds.ToString ()) + "s";
				}
				else {
					result =  ((seconds < 10) ? "0" + seconds.ToString () : seconds.ToString ()) + "s";
				}
				break;
			case TimeStringType.HoursMinutesSeconds:
				hours = (days > 0 )?days*hours:hours;
				result = ((hours < 10) ? "0" + hours.ToString () : (hours).ToString ())+ ":"+
					((minutes < 10) ? "0" + minutes.ToString () : minutes.ToString ())+ ":"+
					((seconds < 10) ? "0" + seconds.ToString () : seconds.ToString ());
				break;
			}
			return result;
		}
	}
}