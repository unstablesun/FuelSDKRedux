namespace FuelSDKIntegration.Structures 
{
	public class IgniteActivityFactory  
	{
		public static IgniteActivity GetActivity(IgniteEventType type)
		{
			IgniteActivity activity = null;
			switch( type ) {
				case IgniteEventType.leaderBoard:
					//activity = new IgniteLeaderBoard();
					break;
				case IgniteEventType.mission:
					activity = new IgniteMission();
					break;
				case IgniteEventType.quest:
					//activity = new IgniteQuest();
					break;
				case IgniteEventType.offer:
					//activity = new IgniteOffer();
					break;
				default:
					break;
			}

			return activity;
		}
	}

}
