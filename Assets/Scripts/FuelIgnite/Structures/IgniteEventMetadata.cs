using System;
using System.Collections.Generic;

namespace FuelSDKIntegration.Structures
{
	public class IgniteEventMetadata:Metadata 
	{
		public int SpecialCharacterId { get; set; }
		public int UseCharaterId { get; set; }
		public int SpecialOp{ get; set; }
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
			this.SpecialOp = 0;
			this.SpecialCharacterId = 0;
			this.UseCharaterId = 0;
			this.imageUrl = string.Empty;
			this.imageMapUrl = string.Empty;
		}

		public override void Create ( Dictionary<string,object> metadataDict ) {
			base.Create( metadataDict );

			if( metadataDict.ContainsKey( "specialCharacterId" ) ) {
				this.SpecialCharacterId = Convert.ToInt32( metadataDict["specialCharacterId"] );
			}
				
			if( metadataDict.ContainsKey( "useCharacterId" ) ) {
				this.UseCharaterId = Convert.ToInt32( metadataDict["useCharacterId"] );
			}

			if( metadataDict.ContainsKey( "specialOp" ) ) {
				this.SpecialOp = Convert.ToInt32( metadataDict["specialOp"] );
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