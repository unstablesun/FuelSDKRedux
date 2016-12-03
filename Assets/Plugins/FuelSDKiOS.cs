﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using FuelSDKSimpleJSON;

#if UNITY_IOS

public class FuelSDKiOS : FuelSDKPlatform {

	[DllImport ("__Internal")]
	private static extern void iOSInitialize(string key, string secret, bool gameHasLogin, bool gameHasInvite, bool gameHasShare);
	[DllImport ("__Internal")]
	private static extern void iOSSetLanguageCode(string languageCode);
	[DllImport ("__Internal")]
	private static extern bool iOSLaunch();
	[DllImport ("__Internal")]
	private static extern bool iOSSubmitMatchResult(string delimitedMatchInfo);
	[DllImport ("__Internal")]
	private static extern void iOSSyncChallengeCounts();
	[DllImport ("__Internal")]
	private static extern void iOSSyncTournamentInfo();
	[DllImport ("__Internal")]
	private static extern bool iOSSyncVirtualGoods();
	[DllImport ("__Internal")]
	private static extern bool iOSAcknowledgeVirtualGoods(string transactionId, string acknowledgementTokensJSONString, bool consumed);
	[DllImport ("__Internal")]
	private static extern bool iOSEnableNotification(FuelSDK.NotificationType notificationType);
	[DllImport ("__Internal")]
	private static extern bool iOSDisableNotification(FuelSDK.NotificationType notificationType);
	[DllImport ("__Internal")]
	private static extern bool iOSIsNotificationEnabled(FuelSDK.NotificationType notificationType);
	[DllImport ("__Internal")]
	private static extern bool iOSSetLocalNotificationMessage(string key, string message);
	[DllImport ("__Internal")]
	private static extern bool iOSSetLocalNotificationActive(string key, bool active);
	[DllImport ("__Internal")]
	private static extern bool iOSIsLocalNotificationActive(string key);
	[DllImport ("__Internal")]
	private static extern bool iOSSdkSocialLoginCompleted(string loginInfo);
	[DllImport ("__Internal")]
	private static extern bool iOSSdkSocialInviteCompleted();
	[DllImport ("__Internal")]
	private static extern bool iOSSdkSocialShareCompleted();
	[DllImport ("__Internal")]
	private static extern void iOSCancelAllNotifications();
	[DllImport ("__Internal")]
	private static extern void iOSRestoreAllLocalNotifications();
	[DllImport ("__Internal")]
	private static extern void iOSSetNotificationToken();
	[DllImport ("__Internal")]
	private static extern void iOSInitializeCompete();
	[DllImport ("__Internal")]
	private static extern void iOSInitializeCompeteUI();
	[DllImport ("__Internal")]
	private static extern void iOSInitializeIgnite();
	[DllImport ("__Internal")]
	private static extern void iOSInitializeIgniteUI();
	[DllImport ("__Internal")]
	private static extern void iOSInitializeDynamics ();
	[DllImport ("__Internal")]
	private static extern bool iOSSetUserConditions (string userConditions);
	[DllImport ("__Internal")]
	private static extern bool iOSSyncUserValues ();
	[DllImport ("__Internal")]
	private static extern void iOSSetOrientationuiCompete (string orientation);
	[DllImport ("__Internal")]
	private static extern void iOSSetOrientationuiIgnite (string orientation);
	[DllImport ("__Internal")]
	private static extern bool iOSExecMethod (string method, string parameters);
	[DllImport ("__Internal")]
	private static extern void iOSSendProgress (string progress, string ruleTags);
	[DllImport ("__Internal")]
	private static extern bool iOSIgniteGetEvent (string tags);
	[DllImport ("__Internal")]
	private static extern bool iOSIgniteGetSampleEvent (string tags);
	[DllImport ("__Internal")]
	private static extern bool iOSIgniteJoinEvent (string eventID);
	[DllImport ("__Internal")]
	private static extern bool iOSIgniteGetLeaderBoard (string boardID);
	[DllImport ("__Internal")]
	private static extern bool iOSIgniteGetMission (string missionID);
	[DllImport ("__Internal")]
	private static extern bool iOSIgniteGetQuest (string questID);
	[DllImport ("__Internal")]
	private static extern bool iOSIgniteGetOffer (string offerID);
	[DllImport ("__Internal")]
	private static extern bool iOSIgniteAcceptOffer (string offerID);
	[DllImport ("__Internal")]
	private static extern bool iOSGetLocalizationFile();

	//user info
	[DllImport ("__Internal")]
	private static extern bool iOSrequestUpdateUserInfo (string userInfo);
	[DllImport ("__Internal")]
	private static extern bool iOSrequestUserAvatars();
	[DllImport ("__Internal")]
	private static extern bool iOSrequestUserInfo();


	public FuelSDKiOS() {
		
	}

	#region Fuel intefaces
	
	public override void Initialize (string key, string secret, bool gameHasLogin, bool gameHasInvite, bool gameHasShare) {
		iOSInitialize(key, secret, gameHasLogin, gameHasInvite, gameHasShare);
	}
	
	public override void SetNotificationToken (string notificationToken) {
		iOSSetNotificationToken();
	}
	
	public override bool EnableNotification (FuelSDK.NotificationType notificationType) {
		return iOSEnableNotification(notificationType);
	}
	
	public override bool DisableNotification (FuelSDK.NotificationType notificationType) {
		return iOSDisableNotification(notificationType);
	}
	
	public override bool IsNotificationEnabled (FuelSDK.NotificationType notificationType) {
		return iOSIsNotificationEnabled(notificationType);
	}

	public override bool SetLocalNotificationMessage (FuelSDK.LocalNotificationType localNotificationType, string message)
	{
		return iOSSetLocalNotificationMessage (localNotificationType.ToString (), message);
	}

	public override bool SetLocalNotificationActive(FuelSDK.LocalNotificationType localNotificationType, bool active)
	{
		return iOSSetLocalNotificationActive (localNotificationType.ToString (), active);
	}

	public override bool IsLocalNotificationActive(FuelSDK.LocalNotificationType localNotificationType)
	{
		return iOSIsLocalNotificationActive (localNotificationType.ToString ());
	}

	public override void SetLanguageCode (string langCode) {
		iOSSetLanguageCode(langCode);
	}
	
	public override bool SetNotificationIcon (int iconResId) {
		// unused by iOS
		return false;
	}
	
	public override bool SetNotificationIcon (string iconName) {
		// unused by iOS
		return false;
	}
	
	public override bool SyncVirtualGoods () {
		return iOSSyncVirtualGoods();
	}
	
	public override bool AcknowledgeVirtualGoods( string transactionId, string acknowledgementTokensJSONString, bool consumed) {
		return iOSAcknowledgeVirtualGoods(transactionId, acknowledgementTokensJSONString, consumed);
	}
	
	public override bool SdkSocialLoginCompleted (Dictionary<string, string> loginData) {
		
		string urlEncodedString = null;
		
		if (loginData != null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			
			bool first = true;
			
			foreach ( KeyValuePair<string,string> entry in loginData )
			{
				if (first)
				{
					first = false;
				}
				else
				{
					stringBuilder.Append( "&" );
				}
				
				stringBuilder.Append( WWW.EscapeURL (entry.Key) );
				stringBuilder.Append( "=" );
				stringBuilder.Append( WWW.EscapeURL (entry.Value) );
			}
			
			urlEncodedString = stringBuilder.ToString();
		}
		
		return iOSSdkSocialLoginCompleted( urlEncodedString );
	}
	
	public override bool SdkSocialInviteCompleted () {
		return iOSSdkSocialInviteCompleted();
	}
	
	public override bool SdkSocialShareCompleted () {
		return iOSSdkSocialShareCompleted();
	}

	public override bool RequestUpdateUserInfo (string userInfo) {
		return iOSrequestUpdateUserInfo (userInfo);
	}

	public override bool RequestUserAvatars () {
		return iOSrequestUserAvatars ();
	}

	public override bool RequestUserInfo (){
		return iOSrequestUserInfo();
	}


	#endregion

	#region Compete intefaces
	
	public override void InitializeCompete()
	{
		iOSInitializeCompete ();
	}
	
	public override bool SubmitMatchResult (string matchResultJSONString) {
		return iOSSubmitMatchResult (matchResultJSONString);
	}
	
	public override void SyncChallengeCounts () {
		iOSSyncChallengeCounts();
	}
	
	public override void SyncTournamentInfo () {
		iOSSyncTournamentInfo();
	}
	#endregion
	
	#region Competeui intefaces
	public override void InitializeCompeteUI()
	{
		iOSInitializeCompeteUI();
	}

	public override void SetOrientationUICompete (FuelSDK.ContentOrientation orientation) {
		iOSSetOrientationuiCompete (orientation.ToString ());
	}
	
	public override bool Launch ()
	{
		return iOSLaunch();
	}
	#endregion
	
	#region Ignite intefaces
	
	public override void InitializeIgnite ()
	{
		iOSInitializeIgnite ();
	}
	
	
	public override bool ExecMethod (string method, string parameters) {
		return iOSExecMethod(method, parameters);
	}
	
	public override void SendProgress (string progress, string tags) {
		iOSSendProgress(progress , tags);
	}
	
	public override bool GetEvents (string eventTags) {
		return iOSIgniteGetEvent( eventTags );
	}

	public override bool GetSampleEvents (string eventTags) {
		return iOSIgniteGetSampleEvent( eventTags );
	}

	public override bool JoinEvent (string eventID) {
		return iOSIgniteJoinEvent(eventID);
	}
	

	public override bool GetLeaderBoard (string boardID) {
		return iOSIgniteGetLeaderBoard(boardID);
	}
	
	public override bool GetMission (string missionID) {
		return iOSIgniteGetMission(missionID);
	}
	
	public override bool GetQuest (string questID) {
		return iOSIgniteGetQuest(questID);

	}

	public override bool GetOffer (string offerID) {
		return iOSIgniteGetOffer(offerID);
	}

	public override bool AcceptOffer (string offerID) {
		return iOSIgniteAcceptOffer(offerID);
	}

	#endregion
	
	#region Ingniteui intefaces
	public override void InitializeIgniteUI () {
		iOSInitializeIgniteUI ();
	}
	
	public override  void SetOrientationUIIgnite (FuelSDK.ContentOrientation orientation) {
		iOSSetOrientationuiIgnite(orientation.ToString());
	}
	
	#endregion
	
	#region Dynamics intefaces
	
	public override void InitializeDynamics () {
		iOSInitializeDynamics ();
	}
	
	public override bool SetUserConditions (string userConditions) {
		return iOSSetUserConditions (userConditions);
	}
	
	public override bool SyncUserValues () {
		return iOSSyncUserValues ();
	}
	#endregion


	#region Localization intefaces
	public override bool GetLocalizationFile ()
	{
		return iOSGetLocalizationFile ();
	}


	#endregion


	/// <summary>
	/// Clears all received notifications (local and remote) from the notification center
	/// and unschedules any set local notifications that haven't fired yet. Unscheduled 
	/// local notifications are cached in order to be rescheduled via the
	/// RestoreAllLocalNotifications() call
	/// </summary>
	public override void CancelAllNotifications ()
	{
		if (!Application.isEditor) {
			iOSCancelAllNotifications();
		}
	}

	/// <summary>
	/// Restores cancelled Propeller SDK local notifications
	/// </summary>
	public override void RestoreAllLocalNotifications ()
	{
		if (!Application.isEditor) {
			iOSRestoreAllLocalNotifications();
		}
	}


	public override void OnPause ()
	{
		// nused by iOS
	}

	public override void OnQuit ()
	{
		// unused by iOS
	}

	public override void OnResume ()
	{
		// unused by iOS
	}

	public override void InitializeGCM (string googleProjectNumber)
	{
		// unused by iOS
	}	
}
#endif
