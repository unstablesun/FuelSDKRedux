using System;
using System.Collections.Generic;

namespace FuelSDKIntegration.Structures 
{
	public interface IgniteActivityInterface 
	{
		void Create ( System.Collections.Generic.Dictionary<string,object> dataDict );
		void SetMetadata( Dictionary<string,object> metadataDict );
		bool GetVirtualGood();
		bool HaveVirtualGood();
		bool HasParticipated();
		bool Completed();
	}

	public class IgniteActivity : IgniteActivityInterface 
	{
		public string Id { get; set; }
		public float Progress { get; set; }
		private DateTime lastUpdate { get; set; }

		public IgniteActivity() {
			this.Id = string.Empty;
			this.Progress = 0f;
			this.lastUpdate = DateTime.MinValue;
		}

		public virtual void Create ( System.Collections.Generic.Dictionary<string,object> dataDict ) {
			if( dataDict.ContainsKey( "id" ) ) {
				this.Id = Convert.ToString( dataDict["id"] );
			}
			if( dataDict.ContainsKey( "progress" ) ) {
				this.Progress = (float)Math.Round( Convert.ToDouble(dataDict["progress"]), 2);
			}
			lastUpdate = DateTime.UtcNow;
		}

		public DateTime LastUpdate {
			get{
				if( lastUpdate == DateTime.MinValue ) {
					return DateTime.UtcNow;
				}
				return lastUpdate;
			}
		}

		public virtual void SetMetadata( Dictionary<string,object> metadataDict ) {
		}

		public virtual bool GetVirtualGood () {
			return false;
		}

		public virtual bool HaveVirtualGood () {
			return true;
		}

		public virtual bool HasParticipated() {
			return true;
		}

		public virtual bool Completed () {
			return false;
		}

	}


	public class IgniteActivityMetadata : Metadata {
		//public VirtualGoodData VirtualGood { get; set; }

		public IgniteActivityMetadata() {
			//this.VirtualGood = new VirtualGoodData();
		}

		public override void Create ( Dictionary<string,object> metadataDict ) {
			base.Create( metadataDict );

			if( metadataDict.ContainsKey( "virtualGood" ) ) {
				//Dictionary<string,object> virtualGoodDict = metadataDict["virtualGood"] as Dictionary<string,object>;
				////this.VirtualGood = new VirtualGoodData();
				////this.VirtualGood.Create( virtualGoodDict );
			}
		}
	}

}
