using System;
using System.Collections.Generic;

namespace FuelSDKIntegration.Structures
{
	public class IgniteMissionMetadata:IgniteActivityMetadata 
	{

		//TODO: virtual good information for the overall mission comes in here

		public IgniteMissionMetadata() {
		}

		public override void Create ( Dictionary<string,object> metadataDict ) {
			base.Create( metadataDict );

		}
	}
}
