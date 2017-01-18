using System;
using System.Collections.Generic;

namespace FuelSDKIntegration.Structures
{
	public class IgniteEventMetadata:Metadata 
	{
		//public string CascadeId { get; set; }
		//public string JoinedLeaderboard { get; set; }
		////public int JoinWindow { get; set; }
		//public int CascadeInterval { get; set; }
		//public int CascadeLength { get; set; }
		public string imageUrl { get; set; }
		public string ImageUrl { 
			get {
				if( !imageUrl.StartsWith("http") ) {
					return "https:"+imageUrl;
				}
				return imageUrl;
			}
		}

		public string imageMapUrl { get; set; }
		public string ImageMapUrl { 
			get {
				if( !imageMapUrl.StartsWith("http") ) {
					return "https:"+imageMapUrl;
				}
				return imageMapUrl;
			}
		}

		public IgniteEventMetadata() {
			this.Name = string.Empty;
			//this.CascadeId = string.Empty;
			//this.JoinedLeaderboard = string.Empty;
			//this.CascadeInterval = 0;
			//this.CascadeLength = 0;
			this.imageUrl = string.Empty;
			this.imageMapUrl = string.Empty;
		}

		public override void Create ( Dictionary<string,object> metadataDict ) {
			base.Create( metadataDict );

			/*
			if( metadataDict.ContainsKey( "cascadeId" ) ) {
				this.CascadeId = Convert.ToString( metadataDict["cascadeId"] );
			}

			if( metadataDict.ContainsKey( "joinedLeaderboard" ) ) {
				if (!String.IsNullOrEmpty (Convert.ToString (metadataDict ["joinedLeaderboard"]))) {
					this.JoinedLeaderboard = Convert.ToString (metadataDict ["joinedLeaderboard"]);
				} else {
					string joinedLB = GameSave.GetJoinedLeaderboard (this.CascadeId);
					this.JoinedLeaderboard = joinedLB;
				}

			}
			else {
				string joinedLB = GameSave.GetJoinedLeaderboard (this.CascadeId);
				this.JoinedLeaderboard = joinedLB;
			}

			if( metadataDict.ContainsKey( "cascadeInterval" ) ) {
				this.CascadeInterval = Convert.ToInt32( metadataDict["cascadeInterval"] );
			}

			if( metadataDict.ContainsKey( "cascadeLength" ) ) {
				this.CascadeLength = Convert.ToInt32( metadataDict["cascadeLength"] );
			}
			*/
			if( metadataDict.ContainsKey( "imageUrl" ) ) {
				this.imageUrl = Convert.ToString( metadataDict["imageUrl"] );
			}

			if( metadataDict.ContainsKey( "imageMapUrl" ) ) {
				this.imageMapUrl = Convert.ToString( metadataDict["imageMapUrl"] );
			}
		}
	}
}