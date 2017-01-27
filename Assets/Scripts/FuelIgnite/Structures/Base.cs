using UnityEngine;
using System;
using FuelSDKIntegration.IgniteLocalization;

namespace FuelSDKIntegration.Structures 
{
	public class Metadata 
	{
		
		private string name;
		public string Name { 
			get {
				//return HemanLocalization.Instance.LocalizeText(name);

				return name;
			}
			set {
				name = value;
			}
		}

		public virtual void Create ( System.Collections.Generic.Dictionary<string,object> metadataDict ) {
			if( metadataDict.ContainsKey( "name" ) ) {
				this.Name = Convert.ToString( metadataDict["name"]);
			}
		}
	}

	public class VisualData 
	{
		protected string Id = "";

		public VisualData( string id ) {
			this.Id = id;
		}

		public void SetKeyEventID ( string eventid ) {
			this.Id = eventid;
		}

		public int GetIntValue( string pref ) {
			return PlayerPrefs.GetInt(pref+Id,0);
		}

		public void SetIntValue( string pref, int value ) {
			PlayerPrefs.SetInt( pref+Id , value );
		}

		public bool GetBoolValue( string pref ) {
			return (PlayerPrefs.GetInt(pref+Id,0) == 1);
		}

		public void SetBoolValue( string pref, bool value ) {
			PlayerPrefs.SetInt( pref+Id , (value)?1:0 );
		}

		public string GetStringValue( string pref ) {
			return PlayerPrefs.GetString(pref+Id,"");
		}

		public void SetStringValue( string pref, string value ) {
			PlayerPrefs.SetString( pref+Id , value );
		}

		public float GetFloatValue( string pref ) {
			return PlayerPrefs.GetFloat(pref+Id,0f);
		}

		public void SetFloatValue( string pref, float value ) {
			PlayerPrefs.SetFloat( pref+Id , value );
		}
	}

}
