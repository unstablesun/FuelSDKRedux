using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FuelSDKSimpleJSON;

#if UNITY_EDITOR

public class FuelSDKEditor : FuelSDKPlatform {

	// -- Fuel intefaces
	public override void Initialize(string key, string secret, bool gameHasLogin, bool gameHasInvite, bool gameHasShare) {}
	
	public override void SetNotificationToken (string notificationToken) {}
	
	public override bool EnableNotification (FuelSDK.NotificationType notificationType) {
		return false;
	}
	
	public override bool DisableNotification (FuelSDK.NotificationType notificationType) {
		return false;
	}
	
	public override bool IsNotificationEnabled (FuelSDK.NotificationType notificationType) {
		return false;
	}
	
	public override void SetLanguageCode (string langCode) {}
	
	public override bool SetNotificationIcon (int iconResId) {
		return false;
	}
	
	public override bool SetNotificationIcon (string iconName) {
		return false;
	}

	public override bool SetLocalNotificationMessage (FuelSDK.LocalNotificationType localNotificationType, string message)
	{
		return false;
	}

	public override bool SetLocalNotificationActive(FuelSDK.LocalNotificationType localNotificationType, bool active)
	{
		return false;
	}

	public override bool IsLocalNotificationActive(FuelSDK.LocalNotificationType localNotificationType)
	{
		return false;
	}

	public override bool SyncVirtualGoods () {
		return false;
	}
	
	public override bool AcknowledgeVirtualGoods(string transactionId, string acknowledgementTokensJSONString, bool consumed) {
		return false;
	}
	
	public override bool SdkSocialLoginCompleted (Dictionary<string, string> loginData) {
		return false;
	}
	
	public override bool SdkSocialInviteCompleted () {
		return false;
	}
	
	public override bool SdkSocialShareCompleted () {
		return false;
	}
	
	// -- Compete intefaces
	public override void InitializeCompete() {}
	
	public override bool SubmitMatchResult (string matchResultJSONString) {
		return false;
	}
	
	public override void SyncChallengeCounts () {}
	
	public override void SyncTournamentInfo () {}
	
	// -- Competeui interfaces
	public override void InitializeCompeteUI() {}
	
	public override void SetOrientationUICompete (FuelSDK.ContentOrientation orientation) {}
	
	public override bool Launch () {
		return false;
	}
	
	// -- Ingnite intefaces
	public override void InitializeIgnite() {}
	
	public override bool ExecMethod (string method, string parameters) {
		return false;
	}
	
	public override void SendProgress (string progress, string ruleTags){}
	
	public override bool GetEvents (string eventTags) {
		return false;
	}

	public override bool GetSampleEvents (string eventTags) {
		return false;
	}
	
	public override bool GetLeaderBoard (string boardID) {
		return false;
	}
	
	public override bool JoinEvent (string eventID) {
		return false;
	}
	
	public override bool GetMission (string missionID) {
		return false;
	}
	
	public override bool GetQuest (string questID) {
		return false;
	}

	public override bool GetOffer (string offerID) {
		return false;
	}

	public override bool AcceptOffer (string offerID) {
		return false;
	}
	
	// -- Ingniteui intefaces
	public override void InitializeIgniteUI() {}
	
	public override  void SetOrientationUIIgnite (FuelSDK.ContentOrientation orientation) {}
	
	//--Dynamics methods
	public override void InitializeDynamics () {}
	
	public override bool SetUserConditions (string userConditions) {
		return false;
	}
	
	public override bool SyncUserValues () {
		return false;
	}

	//-Localization methods
	public override bool GetLocalizationFile ()
	{
		return false;
	}
	
	public override void OnPause() {}
	
	public override void OnResume() {}
	
	public override void OnQuit() {}
	
	public override void CancelAllNotifications() {}
	
	public override void RestoreAllLocalNotifications() {}
	
	public override void InitializeGCM(string googleProjectNumber) {}

	public override bool RequestUpdateUserInfo(string userData) {
		return false;
	}

	public override bool RequestUserAvatars () {
		return false;
	}

	public override bool RequestUserInfo () {
		return false;
	}


}

#endif 
