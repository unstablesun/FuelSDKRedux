using System;
using System.Collections.Generic;

namespace FuelSDKIntegration.Structures
{
	public class IgniteMissionRuleMetadata:Metadata 
	{
		public string GameData { get; set; } 

		public override void Create ( Dictionary<string,object> metadataDict ) 
		{
			base.Create( metadataDict );

			if( metadataDict.ContainsKey( "gamedata" ) ) 
			{
				this.GameData = Convert.ToString( metadataDict["gamedata"] );
			}

		}
	}
}
