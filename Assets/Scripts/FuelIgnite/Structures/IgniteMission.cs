using System;
using System.Collections.Generic;

namespace FuelSDKIntegration.Structures 
{
	public class IgniteMission : IgniteActivity 
	{
		public Dictionary<string,IgniteMissionRuleData> Rules { get; set; }
		public IgniteMissionMetadata Metadata { get; set; }
		//public IgniteMissionVisualData VisualData { get; set; }

		public IgniteMission() {
			this.Rules = new Dictionary<string, IgniteMissionRuleData>();
			this.Metadata = new IgniteMissionMetadata();
			//this.VisualData = new IgniteMissionVisualData( string.Empty );
		}

		public bool AllRulesCompleted {
			get {
				return RulesCompleted == Rules.Count;
			}
		}

		public int RulesCompleted {
			get {
				int rulesCompleted = 0;
				if( Rules != null ) {
					foreach( IgniteMissionRuleData missionRule in Rules.Values ) {
						if( missionRule.Progress >= 1 ) {
							rulesCompleted++;
						}
					}
				}
				return rulesCompleted;
			}
		}

		public override void Create( Dictionary<string,object> dataDict ) {
			base.Create( dataDict );

			if( dataDict.ContainsKey( "metadata" ) ) {
				Dictionary<string,object> missionMetadataDict = dataDict["metadata"] as Dictionary<string,object>;
				SetMetadata( missionMetadataDict );
			}
			if( dataDict.ContainsKey("rules") ) {
				this.Rules = new Dictionary<string, IgniteMissionRuleData>();
				List<object> rulesList = dataDict["rules"] as List<object>;
				foreach(object rule in rulesList ) {
					Dictionary<string,object> ruleDict = rule as Dictionary<string, object>;
					IgniteMissionRuleData ruleData = new IgniteMissionRuleData();
					ruleData.Create( ruleDict );
					this.Rules.Add( ruleData.Id, ruleData);
				}
			}
			//this.VisualData = new IgniteMissionVisualData( this.Id );
		}

		public override void SetMetadata( Dictionary<string,object> metadataDict ) {
			this.Metadata = new IgniteMissionMetadata();
			this.Metadata.Create( metadataDict );
		}

		/*
		public override bool GetVirtualGood () {
			if( !string.IsNullOrEmpty( this.Metadata.VirtualGood.Id ) ) {
				this.Metadata.VirtualGood.GetReward();
				return true;
			}
			return base.GetVirtualGood();
		}

		public override bool HaveVirtualGood () {
			if( !string.IsNullOrEmpty( this.Metadata.VirtualGood.Id ) ) {
				return true;
			}
			return base.HaveVirtualGood();
		}
		*/
		public override bool Completed()  {
			return base.Progress >= 1;
		}
	}
}
