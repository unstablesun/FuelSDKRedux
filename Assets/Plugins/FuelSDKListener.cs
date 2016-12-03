using UnityEngine;
using System.Collections.Generic;

public class FuelSDKListener
{
	public virtual void OnVirtualGoodList (string transactionID, List<object> virtualGoods) {}
	
	public virtual void OnVirtualGoodRollback (string transactionID) {}

	public virtual void OnVirtualGoodConsumeSuccess (string transactionID) {}
	
	public virtual void OnNotificationEnabled (FuelSDK.NotificationType notificationType) {}

	public virtual void OnNotificationDisabled (FuelSDK.NotificationType notificationType) {}

	public virtual void OnSocialLogin (bool allowCache) {}
	
	public virtual void OnSocialInvite (Dictionary<string, string> data) {}
	
	public virtual void OnSocialShare (Dictionary<string, string> data) {}
	
	public virtual void OnImplicitLaunch (FuelSDK.ApplicationState applicationState) {}

	public virtual void OnUserValues (Dictionary<string, string> conditions, Dictionary<string, string> variables) {}

	public virtual void OnCompeteChallengeCount (int count) {}
	
	public virtual void OnCompeteTournamentInfo (Dictionary<string, string> tournamentInfo) {}

	public virtual void OnCompeteUICompletedWithExit () {}
	
	public virtual void OnCompeteUICompletedWithMatch (Dictionary<string, object> matchInfo) {}

	public virtual void OnCompeteUIFailed (string reason) {}

	public virtual void OnIgniteEvents (List<object> events) {}

	public virtual void OnIgniteSampleEvents (List<object> events) {}

	public virtual void OnIgniteLeaderBoard (Dictionary<string,object> leaderBoard) {}

	public virtual void OnIgniteMission (Dictionary<string,object> mission) {}

	public virtual void OnIgniteQuest (Dictionary<string,object> data) {}

	public virtual void OnIgniteOffer (Dictionary<string, object> offer) {}

	public virtual void OnIgniteAcceptOffer (string offerID, bool accepted) {}

	public virtual void OnIgniteJoinEvent (string eventID, bool joinStatus) {}

	public virtual void OnLocalizationData (string filePath) {}

	public virtual void OnReceiveData (Dictionary<string, object> data) {}

	public virtual void OnUserInfo(Dictionary<string,object> data) {}

	public virtual void OnUpdatedUserInfo(Dictionary<string,object> data) {}

	public virtual void OnUserAvatars(Dictionary<string,object> avatars) {}

	public virtual void OnIgniteEngineLoaded(Dictionary<string, object> data) {}

	public virtual void OnDynamicsEngineLoaded(Dictionary<string, object> data) {}

	public virtual void OnServerAPIStatusUpdated(Dictionary<string, object> data) {}

}
