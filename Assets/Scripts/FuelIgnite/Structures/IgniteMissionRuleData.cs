using System;
using System.Collections.Generic;

namespace FuelSDKIntegration.Structures
{
	public class IgniteMissionRuleData 
	{
		public enum MissionRuleType 
		{
			none = -1,
			incremental = 0,
			spot       	= 1,
		}

		public string Id { get; set; }
		public int Score { get; set; }
		public int Target { get; set; }
		public bool Achieved { get; set; }
		public string Variable { get; set; }
		public MissionRuleType Kind { get; set; }
		public IgniteMissionRuleMetadata Metadata { get; set; }
		//public MissionRuleVisualData VisualData { get; set; }

		public IgniteMissionRuleData() {
		
			this.Id = string.Empty;
			this.Score = -1;
			this.Target = -1;
			this.Achieved = false;
			this.Variable = string.Empty;
			this.Kind = MissionRuleType.none;
			this.Metadata = new IgniteMissionRuleMetadata ();
			//this.VisualData = new MissionRuleVisualData (this.Id);
		}

		public float Progress {
			get {
				double progressValue = (Target > 0)?Math.Round( (double)Score/(double)Target , 3 ):0;
				if( progressValue > 1 ) {
					progressValue = 1;
				}
				return (float)progressValue;
			}
		}

		public bool Complete {
			get {
				return Progress == 1.0f;
			}
		}

		public void Create ( Dictionary<string,object> ruleDict ) {
			if( ruleDict.ContainsKey("id") ) {
				this.Id = Convert.ToString( ruleDict["id"] );
			}
			if( ruleDict.ContainsKey("score") ) {
				this.Score = Convert.ToInt32( ruleDict["score"] );
			}
			if( ruleDict.ContainsKey("target") ) {
				this.Target = Convert.ToInt32( ruleDict["target"] );
			}
			if( ruleDict.ContainsKey("achieved") ) {
				this.Achieved = Convert.ToBoolean( ruleDict["achieved"] );
			}
			if( ruleDict.ContainsKey("variable") ) {
				this.Variable = Convert.ToString( ruleDict["variable"] );
			}
			if( ruleDict.ContainsKey("kind") ) {
				this.Kind = (MissionRuleType) Enum.Parse( typeof(MissionRuleType) , Convert.ToString( ruleDict["kind"] ) );
			}
			if( ruleDict.ContainsKey( "metadata" ) ) {
				Dictionary<string,object> ruleMetadataDict = ruleDict["metadata"] as Dictionary<string,object>;
				this.Metadata = new IgniteMissionRuleMetadata();
				this.Metadata.Create( ruleMetadataDict );
			}
			//this.VisualData = new MissionRuleVisualData( this.Id );
		}
	}
}
