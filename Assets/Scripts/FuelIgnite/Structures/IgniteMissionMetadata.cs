using System;
using System.Collections.Generic;

namespace FuelSDKIntegration.Structures
{
	public class IgniteMissionMetadata:IgniteActivityMetadata 
	{
		public string GameData { get; set; }

		public IgniteMissionMetadata() {
		}

		public override void Create ( Dictionary<string,object> metadataDict ) {
			base.Create( metadataDict );

			if( metadataDict.ContainsKey( "gamedata" ) ) {
				this.GameData = Convert.ToString( metadataDict["gamedata"] );
			}
		}
	}
}
