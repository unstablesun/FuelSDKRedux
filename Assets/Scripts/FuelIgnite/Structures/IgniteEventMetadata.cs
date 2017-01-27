using System;
using System.Collections.Generic;

namespace FuelSDKIntegration.Structures
{
	public class IgniteEventMetadata:Metadata 
	{
		public string EventCharacterName { get; set; }
		public string PlayCharacterName { get; set; }
		public bool PlayCharacterEnabled { get; set; }
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
			this.EventCharacterName = string.Empty;
			this.PlayCharacterName = string.Empty;
			this.PlayCharacterEnabled = false;
			this.imageUrl = string.Empty;
			this.imageMapUrl = string.Empty;
		}

		public override void Create ( Dictionary<string,object> metadataDict ) {
			base.Create( metadataDict );

			if( metadataDict.ContainsKey( "eventCharacterName" ) ) {
				this.EventCharacterName = Convert.ToString( metadataDict["eventCharacterName"] );
			}
				
			if( metadataDict.ContainsKey( "playCharacterName" ) ) {
				this.PlayCharacterName = Convert.ToString( metadataDict["playCharacterName"] );
			}

			if( metadataDict.ContainsKey( "playCharacterEnabled" ) ) {
				this.PlayCharacterEnabled = Convert.ToBoolean( metadataDict["playCharacterEnabled"] );
			}

			if( metadataDict.ContainsKey( "imageUrl" ) ) {
				this.imageUrl = Convert.ToString( metadataDict["imageUrl"] );
			}

			if( metadataDict.ContainsKey( "imageMapUrl" ) ) {
				this.imageMapUrl = Convert.ToString( metadataDict["imageMapUrl"] );
			}
		}
	}
}