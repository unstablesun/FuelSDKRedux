using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FuelSDKSimpleJSON;


public class FuelSDK : MonoBehaviour {

	#region ===================================== data enums =====================================

	public enum ContentOrientation
	{
		landscape,
		portrait,
		auto
	}

	public enum NotificationType
	{
		none 	= 0x0,
		all 	= 0x3,
		push 	= 1 << 0,
		local 	= 1 << 1
	}

	public enum ApplicationState
	{
		none,
		starting,
		running,
		resuming
	}

	private enum DataReceiverAction
	{
		none,
		fuelSDKVirtualGoodList,
		fuelSDKVirtualGoodRollback,
		fuelSDKVirtualGoodConsumeSuccess,
		fuelSDKNotificationEnabled,
		fuelSDKNotificationDisabled,
		fuelSDKSocialLoginRequest,
		fuelSDKSocialInviteRequest,
		fuelSDKSocialShareRequest,
		fuelSDKImplicitLaunchRequest,
		fuelSDKUserValues,
		fuelSDKCompeteChallengeCount,
		fuelSDKCompeteTournamentInfo,
		fuelSDKCompeteUICompletedExit,
		fuelSDKCompeteUICompletedMatch,
		fuelSDKCompeteUICompletedFail,
		fuelSDKIgniteEvents,
		fuelSDKIgniteSampleEvents,
		fuelSDKIgniteLeaderBoard,
		fuelSDKIgniteMission,
		fuelSDKIgniteQuest,
		fuelSDKIgniteOffer,
		fuelSDKIgniteAcceptOffer,
		fuelSDKIgniteJoinEvent,
		fuelSDKLocalizationLoaded,
		fuelSDKReceiveData,
		fuelSDKUserInfo,
		fuelSDKUserUpdated,
		fuelSDKUserAvatars,
		fuelSDKIgniteLoaded,
		fuelSDKDynamicsLoaded,
		fuelSDKServerAPIStatusUpdated
	}

	public enum LocalNotificationType
	{
		none,
		competeTournamentStartNow,
		competeTournamentStartWarn,
		competeTournamentEndNow,
		competeTournamentEndWarn,
		igniteEventMissionStartNow,
		igniteEventMissionEndWarn,
		igniteEventLeaderboardStartNow,
		igniteEventLeaderboardEndWarn,
		igniteEventOfferStartNow,
		igniteEventOfferEndWarn,
		igniteEventDefaultStartNow,
		igniteEventDefaultEndWarn
	}

	#endregion

	#region ===================================== Editor Variables =====================================

	public string GameKey;
	public string GameSecret;
	public ContentOrientation Orientation = ContentOrientation.landscape;
	public bool iOSGameHandleLogin;
	public bool iOSGameHandleInvite;
	public bool iOSGameHandleShare;
	public string AndroidNotificationIcon = "notify_icon";
	public string AndroidGCMSenderID;
	public bool AndroidGameHandleLogin;
	public bool AndroidGameHandleInvite;
	public bool AndroidGameHandleShare;
	public bool IgniteEnabled;
	public bool CompeteEnabled;
	public bool DynamicsEnabled;

	#endregion

	#region ===================================== Static Local Variables =====================================

	public static bool Initialized {
		get{ 
			return m_bInitialized;
		}
	}

	private static FuelSDKPlatform platformSelected;
	private static bool m_bInitialized;
	//private static FuelSDKListener m_listener;

	public static bool IsIgniteEnabled {
		get {
			return m_IgniteEnabled;
		}
	}

	private static bool m_IgniteEnabled;

	public static bool IsCompeteEnabled {
		get {
			return m_CompeteEnabled;
		}
	}

	private static bool m_CompeteEnabled;

	#endregion

	#region ===================================== MonoBehaviour =====================================

	void Start() {
		if (!m_bInitialized) {
			GameObject.DontDestroyOnLoad (gameObject);

			if (!Application.isEditor) {
				bool gameHandleLogin = false;
				bool gameHandleInvite = false;
				bool gameHandleShare = false;

				#if UNITY_IOS
				gameHandleLogin = iOSGameHandleLogin;
				gameHandleInvite = iOSGameHandleInvite;
				gameHandleShare = iOSGameHandleShare;
				#elif UNITY_ANDROID
				gameHandleLogin = AndroidGameHandleLogin;
				gameHandleInvite = AndroidGameHandleInvite;
				gameHandleShare = AndroidGameHandleShare;				
				#endif

				GetFuelSDKPlatform().Initialize(GameKey, GameSecret, gameHandleLogin, gameHandleInvite, gameHandleShare);

				if(CompeteEnabled) {
					InitializeCompete();
				}

				if(IgniteEnabled) {
					InitializeIgnite();
				}

				if(DynamicsEnabled) {
					InitializeDynamics();
				}

				GetFuelSDKPlatform().SetNotificationIcon(AndroidNotificationIcon);

				GetFuelSDKPlatform().InitializeGCM(AndroidGCMSenderID);

			}

			m_bInitialized = true;
			m_IgniteEnabled = IgniteEnabled;
			m_CompeteEnabled = CompeteEnabled;
		} else {
			GameObject.Destroy (gameObject);	
		}

		if (CompeteEnabled) {
			InitializeCompeteUI();
			SetOrientationUICompete (Orientation);
		}

		if (IgniteEnabled) {
			InitializeIgniteUI();
			SetOrientationUIIgnite (Orientation);
		}
	}

	private void OnApplicationPause (bool paused)
	{
		if (!Application.isEditor) {
			if (paused) {
				GetFuelSDKPlatform().OnPause();
			} else {
				GetFuelSDKPlatform().OnResume();
			}
		}

	}

	private void OnApplicationQuit ()
	{
		if (!Application.isEditor) {
			GetFuelSDKPlatform().OnQuit(); //Only for Android
		}
	}

	#endregion

	#region ===================================== Static Methods =====================================

	static FuelSDK(){
		m_bInitialized = false;
	}

	private static FuelSDKPlatform GetFuelSDKPlatform ()
	{
		if (platformSelected == null) {
			platformSelected = FuelSDKPlatform.setupPlatform ();
		}

		return platformSelected;
	}

	#endregion
	#region ===================================== User Update =====================================

	/// <summary>
	/// User update Call.
	/// </summary>
	/// <returns><c>true</c>, if the call is correct, <c>false</c> otherwise.</returns>
	/// <param name="data">User Data.</param>
	public static bool RequestUpdateUserInfo (Dictionary<string,object> data)
	{
		JSONClass userInfoJSON = FuelSDKCommon.toJSONClass(data);
		string userInfoString = null;
		if (userInfoJSON == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.WARN, "request update user info with undefined data");
		} else {
			userInfoString = userInfoJSON.ToString (); 
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "request update user with data: " + userInfoString);
		}

		bool succeeded = false;

		if (!Application.isEditor) {
			succeeded = GetFuelSDKPlatform().RequestUpdateUserInfo (userInfoString);
		}

		return succeeded;
	}

	/// <summary>
	/// Request user avatar.
	/// </summary>
	/// <returns><c>true</c>, if the request is correct, <c>false</c> otherwise.</returns>
	public static bool RequestUserAvatars ()
	{
		bool succeeded = false;

		if (!Application.isEditor) {
			succeeded = GetFuelSDKPlatform().RequestUserAvatars();
		}

		return succeeded;
	}

	/// <summary>
	/// Request user profile info.
	/// </summary>
	/// <returns><c>true</c>, if the request is correct, <c>false</c> otherwise.</returns>
	public static bool RequestUserInfo ()
	{
		bool succeeded = false;

		if (!Application.isEditor) {
			succeeded = GetFuelSDKPlatform().RequestUserInfo();
		}

		return succeeded;
	}

	#endregion

	#region ===================================== Notifications =====================================

	public static void SetNotificationToken (string notificationToken) {
		if (notificationToken == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.WARN, "setting an undefined notification token");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "setting notification token: " + notificationToken);
		}

		if (!Application.isEditor) {
			GetFuelSDKPlatform().SetNotificationToken(notificationToken);
		}
	}

	public static void EnableNotification (NotificationType notificationType)
	{
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "enabling notification type: " + notificationType.ToString ());

		if (!Application.isEditor) {
			GetFuelSDKPlatform().EnableNotification(notificationType);
		}
	}

	public static void DisableNotification (NotificationType notificationType)
	{
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "disabling notification type: " + notificationType.ToString ());

		if (!Application.isEditor) {
			GetFuelSDKPlatform().DisableNotification(notificationType);
		}
	}

	public static bool IsNotificationEnabled (NotificationType notificationType)
	{
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "validating if notification type '" + notificationType.ToString () + "' is enabled");

		bool succeed = false;

		if (!Application.isEditor) {
			succeed = GetFuelSDKPlatform().IsNotificationEnabled(notificationType);
		}

		if (succeed) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "notification type '" + notificationType.ToString () + "' is enabled");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "notification type '" + notificationType.ToString () + "' is disabled");
		}

		return succeed;
	}

	public static bool SetLocalNotificationMessage(LocalNotificationType localNotificationType, string message)
	{
		if (localNotificationType == LocalNotificationType.none)
		{
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "attempting to set local notification message for undefined type");
			return false;
		}

		if (String.IsNullOrEmpty (message))
		{
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "setting local notification message for type '" + localNotificationType.ToString () + "', with null or empty message");
		}
		else
		{
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "setting local notification message for type '" + localNotificationType.ToString () + "', with message '" + message + "'");
		}

		return GetFuelSDKPlatform ().SetLocalNotificationMessage (localNotificationType, message);
	}

	public static bool SetLocalNotificationActive(LocalNotificationType localNotificationType, bool active)
	{
		if (localNotificationType == LocalNotificationType.none)
		{
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "attempting to set local notification active state for undefined type");
			return false;
		}

		if (active)
		{
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "setting local notification active for type '" + localNotificationType.ToString () + "'");
		}
		else
		{
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "setting local notification inactive for type '" + localNotificationType.ToString () + "'");
		}

		return GetFuelSDKPlatform ().SetLocalNotificationActive (localNotificationType, active);
	}

	public static bool IsLocalNotificationActive(LocalNotificationType localNotificationType)
	{
		if (localNotificationType == LocalNotificationType.none)
		{
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "attempting to validate local notification active state for undefined type");
			return false;
		}

		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "validating local notification active state for type '" + localNotificationType.ToString () + "'");

		return GetFuelSDKPlatform ().IsLocalNotificationActive (localNotificationType);
	}

	#endregion

	#region ===================================== Language =====================================

	/// <summary>
	/// Sets the language code for the Propeller SDK online content. Must be compliant to ISO 639-1.
	/// If the language code is not supported, then the content will default to English (en)
	/// </summary>
	/// <param name="languageCode">
	/// Two character string language code to set the Fuel SDK online content to.
	/// </param>
	public static void SetLanguageCode (string languageCode)
	{
		if (languageCode == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.WARN, "setting an undefined language code");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "setting language code: " + languageCode);
		}

		if (!Application.isEditor) {
			GetFuelSDKPlatform().SetLanguageCode(languageCode);
		}
	}

	#endregion

	#region ===================================== Virtual Goods =====================================

	/// <summary>
	/// Begins an asynchronous operation to request the virtual goods from Propeller.
	/// </summary>
	public static void SyncVirtualGoods ()
	{
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "syncing virtual goods");

		if (!Application.isEditor) {
			GetFuelSDKPlatform().SyncVirtualGoods();
		}
	}

	/// <summary>
	/// Begins an asynchronous operation to acknowledge all of the received virtual goods from Fuel.
	/// </summary>
	/// <param name="transactionId">
	/// The transaction ID being acknowledged
	/// </param>
	/// <param name="consumed">
	/// Flags whether or not the virutal good were consumed
	/// </param>
	/// <returns>
	/// True if the request to acknowledge the virtual goods succeeded, false otherwise.
	/// </returns>
	public static bool AcknowledgeVirtualGoods (string transactionId, bool consumed)
	{
		return AcknowledgeVirtualGoods (transactionId, null, consumed);
	}
	/// <summary>
	/// Begins an asynchronous operation to acknowledge a specified subset of received virtual goods
	/// from Fuel.
	/// </summary>
	/// <param name="transactionId">
	/// The transaction ID being acknowledged
	/// </param>
	/// <param name="acknowledgementTokens">
	/// The tokens of the virtual goods used to acknowledge their consumption with the Fuel Powered
	/// virtual goods transaction server.
	/// <param name="consumed">
	/// Flags whether or not the virutal good were consumed
	/// </param>
	/// <returns>
	/// True if the request to acknowledge the virtual goods succeeded, false otherwise.
	/// </returns>
	public static bool AcknowledgeVirtualGoods (string transactionId, List<object> acknowledgementTokens, bool consumed)
	{
		if (transactionId == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.WARN, "acknowledging virtual goods with an undefined transaction ID");
			return false;
		}

		if (consumed) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "acknowledging virtual goods for transaction '" + transactionId + "' as consumed");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "acknowledging virtual goods for transaction '" + transactionId + "' as not consumed");
		}

		string acknowledgementTokensJSONString = FuelSDKCommon.Serialize(acknowledgementTokens);

		if (acknowledgementTokensJSONString == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "acknowledging all received virtual goods");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "acknowledging the specified list of virtual goods: " + acknowledgementTokensJSONString);
		}

		if (!Application.isEditor) {
			return GetFuelSDKPlatform().AcknowledgeVirtualGoods(transactionId, acknowledgementTokensJSONString, consumed);
		}

		return false;
	}

	#endregion

	#region ===================================== Login =====================================

	/// <summary>
	/// Begins an asynchronous operation to indicate login info is complete.
	/// </summary>
	/// <param name="loginInfo">Login info.</param>
	public static void SdkSocialLoginCompleted (Dictionary<string, string> loginInfo)
	{
		if (loginInfo == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "social login completed unsuccessfully");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "social login completed with login info: " + FuelSDKCommon.Serialize(loginInfo));
		}

		if (!Application.isEditor) {
			GetFuelSDKPlatform().SdkSocialLoginCompleted(loginInfo);
		}
	}

	#endregion

	#region ===================================== Social =====================================

	/// <summary>
	/// Begins an asynchronous operation to indicate invite info is complete.
	/// </summary>
	public static void SdkSocialInviteCompleted ()
	{
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "social invite completed");

		if (!Application.isEditor) {
			GetFuelSDKPlatform().SdkSocialInviteCompleted();
		}
	}

	/// <summary>
	/// Begins an asynchronous operation to indicate sharing info is complete.
	/// </summary>
	public static void SdkSocialShareCompleted ()
	{
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "social share completed");

		if (!Application.isEditor) {
			GetFuelSDKPlatform().SdkSocialShareCompleted();
		}
	}


	#endregion

	#region ===================================== Compete =====================================

	/// <summary>
	/// Initializes compete.
	/// </summary>
	public static void InitializeCompete() {
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "initializing the compete product");

		if (!Application.isEditor) {
			GetFuelSDKPlatform().InitializeCompete ();
		}
	}

	/// <summary>
	/// Submits the results of the match. You must stuff the match result into a dictionary that will be parsed and passed to the SDK API servers..
	/// Current parameters:
	/// 	matchID
	/// 	tournamentID
	/// 	score
	/// </summary>
	/// <returns>
	/// True if the match results were submitted.
	/// </returns>
	/// <param name="matchResult">
	/// A dictionary filled with values the SDK needs to use to properly pass the result to the API servers. Examples: score, tournamentID, matchID, etc.
	/// </param>
	public static bool SubmitMatchResult (Dictionary<string, object> matchResult) {
		if (matchResult == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.WARN, "submitting undefined match result");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "submitting match result: " + FuelSDKCommon.Serialize(matchResult));
		}

		bool succeeded = false;

		if (!Application.isEditor) {
			JSONClass matchResultJSON = FuelSDKCommon.toJSONClass (matchResult);

			if (matchResultJSON == null) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "unable to coerce match result dictionary into a JSON class");
			} else {
				succeeded = GetFuelSDKPlatform().SubmitMatchResult(matchResultJSON.ToString ());
			}
		}

		if (succeeded) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "match result was submitted");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "match result was not submitted");
		}

		return succeeded;
	}

	/// <summary>
	/// Begins an asynchronous operation to request the player's challenge counts from Propeller.
	/// </summary>
	public static void SyncChallengeCounts ()
	{
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "synching challenge counts");

		if (!Application.isEditor) {
			GetFuelSDKPlatform().SyncChallengeCounts();
		}
	}

	/// <summary>
	/// Begins an asynchronous operation to request the tournament information from Propeller.
	/// </summary>
	public static void SyncTournamentInfo ()
	{
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "synching tournament info");

		if (!Application.isEditor) {
			GetFuelSDKPlatform().SyncTournamentInfo();
		}
	}

	//Compete UI

	public static void InitializeCompeteUI() {
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "initializing the compete UI product");

		if (!Application.isEditor) {
			GetFuelSDKPlatform().InitializeCompeteUI ();
		}
	}

	/// <summary>
	/// Sets the orientationui compete.
	/// </summary>
	/// <param name="orientation">Orientation.</param>
	public static void SetOrientationUICompete(ContentOrientation orientation) {
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "setting the compete UI orientation: " + orientation.ToString ());

		if (!Application.isEditor) {
			GetFuelSDKPlatform().SetOrientationUICompete (orientation);
		}
	}

	/// <summary>
	/// Launch the SDK with the provided listener to handle callbacks.
	/// </summary>
	/// <param name='listener'>
	/// A class that subclasses the PropellerSDKListener abstract class that will receive various callbacks.
	/// </param>
	public static bool Launch ()
	{
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "launching the compete UI");

		bool succeeded = false;

		if (!Application.isEditor) {
			succeeded = GetFuelSDKPlatform().Launch();
		}

		return succeeded;
	}

	#endregion

	#region ===================================== Ignite =====================================

	/// <summary>
	/// Initializes ignite.
	/// </summary>
	public static void InitializeIgnite() {
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "initializing the ignite product");

		GetFuelSDKPlatform().InitializeIgnite ();
	}

	/// <summary>
	/// Execs the method.
	/// </summary>
	/// <returns><c>true</c>, if method was execed, <c>false</c> otherwise.</returns>
	/// <param name="method">Method.</param>
	/// <param name="parameters">Parameters.</param>
	public static bool ExecMethod (string method, List<object> parameters) {
		string parametersString = FuelSDKCommon.Serialize(parameters);

		if (method == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.WARN, "executing undefined method");
		} else {
			if (parameters == null) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "executing method '" + method + "' with undefined parameters");
			} else {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "executing method '" + method + "' with parameters: " + parametersString);
			}
		}

		bool succeeded = false; 

		if (!Application.isEditor) {
			succeeded = GetFuelSDKPlatform().ExecMethod (method, parametersString);
		}

		return succeeded;
	}

	/// <summary>
	/// Sends the progress.
	/// </summary>
	/// <param name="progress">Progress.</param>
	/// <param name="tags">Tags.</param>
	public static void SendProgress (Dictionary<string, object> progress, List<object> tags) {
		string progressString = FuelSDKCommon.Serialize(progress);
		string tagsString = FuelSDKCommon.Serialize(tags);

		if (progress == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.WARN, "sending undefined progress");
		} else {
			if (tags == null) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "sending progress '" + progressString + "' with undefined tags");
			} else {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "sending progress '" + progressString + "' with tags: " + tagsString);
			}
		}

		if (progressString == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "unable to serialize the progress dictionary");
			return;
		}

		if (!Application.isEditor) {
			GetFuelSDKPlatform().SendProgress (progressString, tagsString);
		}
	}

	/// <summary>
	/// Gets the events.
	/// </summary>
	/// <returns><c>true</c>, if events was gotten, <c>false</c> otherwise.</returns>
	public static bool GetEvents (List<object> eventTags) {
		string eventTagsString = FuelSDKCommon.Serialize(eventTags);

		if (eventTags == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "getting events with undefined tags");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "getting events with tags: " + eventTagsString);
		}

		bool succeeded = false;

		if (!Application.isEditor) {
			succeeded =  GetFuelSDKPlatform().GetEvents (eventTagsString);
		}

		return succeeded;
	}

	/// <summary>
	/// Gets the sample events.
	/// </summary>
	/// <returns><c>true</c>, if events was gotten, <c>false</c> otherwise.</returns>
	public static bool GetSampleEvents (List<object> eventTags) {
		string eventTagsString = FuelSDKCommon.Serialize(eventTags);

		if (eventTags == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "getting events with undefined tags");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "getting events with tags: " + eventTagsString);
		}

		bool succeeded = false;

		if (!Application.isEditor) {
			succeeded =  GetFuelSDKPlatform().GetSampleEvents (eventTagsString);
		}

		return succeeded;
	}

	/// <summary>
	/// Joins an event.
	/// </summary>
	/// <returns><c>true</c>, if join request was sent through, <c>false</c> otherwise.</returns>
	/// <param name="eventID">Event ID.</param>
	public static bool JoinEvent (string eventID) {
		if (eventID == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.WARN, "joining with undefined event ID");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "joining with event ID: " + eventID);
		}

		bool succeeded = false;

		if (!Application.isEditor) {
			succeeded = GetFuelSDKPlatform().JoinEvent (eventID);
		}

		return succeeded;
	}

	/// <summary>
	/// Gets the leader board.
	/// </summary>
	/// <returns><c>true</c>, if leader board was gotten, <c>false</c> otherwise.</returns>
	/// <param name="boardID">Board I.</param>
	public static bool GetLeaderBoard (string boardID) {
		if (boardID == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.WARN, "getting leaderboard with undefined leaderboard ID");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "getting leaderboard with leaderboard ID: " + boardID);
		}

		bool succeeded = false;

		if (!Application.isEditor) {
			succeeded = GetFuelSDKPlatform().GetLeaderBoard (boardID);
		}

		return succeeded;
	}

	/// <summary>
	/// Gets the mission.
	/// </summary>
	/// <returns><c>true</c>, if mission was gotten, <c>false</c> otherwise.</returns>
	/// <param name="missionID">Mission I.</param>
	public static bool GetMission (string missionID) {
		if (missionID == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.WARN, "getting mission with undefined mission ID");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "getting mission with mission ID: " + missionID);
		}

		bool succeeded = false;

		if (!Application.isEditor) {
			succeeded = GetFuelSDKPlatform().GetMission (missionID);
		}

		return succeeded;
	}

	/// <summary>
	/// Gets the quest.
	/// </summary>
	/// <returns><c>true</c>, if quest was gotten, <c>false</c> otherwise.</returns>
	/// <param name="questID">Quest I.</param>
	public static bool GetQuest (string questID) {
		if (questID == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.WARN, "getting quest with undefined quest ID");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "getting quest with quest ID: " + questID);
		}

		bool succeeded = false;

		if (!Application.isEditor) {
			succeeded = GetFuelSDKPlatform().GetQuest (questID);
		}

		return succeeded;
	}

	/// <summary>
	/// Gets the offer.
	/// </summary>
	/// <returns><c>true</c>, if offer was gotten, <c>false</c> otherwise.</returns>
	/// <param name="offerID">Offer ID</param>
	public static bool GetOffer (string offerID) {
		if (offerID == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.WARN, "getting offer with undefined offer ID");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "getting offer with offer ID: " + offerID);
		}

		bool succeeded = false;

		if (!Application.isEditor) {
			succeeded = GetFuelSDKPlatform().GetOffer (offerID);
		}

		return succeeded;
	}

	/// <summary>
	/// Accepts the offer.
	/// </summary>
	/// <returns><c>true</c>, if offer was accepted, <c>false</c> otherwise.</returns>
	/// <param name="offerID">Offer ID</param>
	public static bool AcceptOffer (string offerID) {
		if (offerID == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.WARN, "accepting offer with undefined offer ID");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "accepting offer with offer ID: " + offerID);
		}

		bool succeeded = false;

		if (!Application.isEditor) {
			succeeded = GetFuelSDKPlatform().AcceptOffer (offerID);
		}

		return succeeded;
	}

	//Ingnite UI

	/// <summary>
	/// Initializes the ingite U.
	/// </summary>
	public static void InitializeIgniteUI () {
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "initializing the ignite UI product");

		if (!Application.isEditor) {
			GetFuelSDKPlatform().InitializeIgniteUI ();
		}
	}

	/// <summary>
	/// Sets the orientationui ignite.
	/// </summary>
	/// <param name="orientation">Orientation.</param>
	public static void SetOrientationUIIgnite (ContentOrientation orientation) {
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "setting the ignite UI orientation: " + orientation.ToString ());

		if (!Application.isEditor) {
			GetFuelSDKPlatform().SetOrientationUIIgnite (orientation);
		}
	}

	//Dynamics

	/// <summary>
	/// Initializes dynamics.
	/// </summary>
	public static void InitializeDynamics () {
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "initializing the dynamics product");

		if (!Application.isEditor) {
			GetFuelSDKPlatform().InitializeDynamics ();
		}
	}

	/// <summary>
	/// FUEL DYNAMICS - SetUserConditions
	/// 
	/// </summary>
	/// <returns>
	/// True if the call to the SDK succeeded.
	/// </returns>
	/// <param name="conditions">
	/// A dictionary filled with values the SDK needs to use to properly pass the result to the Propeller servers. Examples: score, tournamentID, matchID, etc.
	/// </param>
	public static bool SetUserConditions (Dictionary<string, object> conditions)
	{
		string userConditions = FuelSDKCommon.Serialize(conditions);

		if (conditions == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.WARN, "setting undefined user conditions");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "setting user conditions: " + userConditions);
		}

		bool succeeded = false;

		if (!Application.isEditor) {
			succeeded = GetFuelSDKPlatform().SetUserConditions(userConditions);
		}

		return succeeded;
	}

	/// <summary>
	/// Begins an asynchronous operation to request the user values from Propeller.
	/// </summary>
	/// <returns><c>true</c>, if user values was synced, <c>false</c> otherwise.</returns>
	public static bool SyncUserValues ()
	{
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "synching user values");

		bool succeeded = false;

		if (!Application.isEditor) {
			succeeded = GetFuelSDKPlatform().SyncUserValues();
		}

		return succeeded;
	}

	#endregion

	public static void GetLocalizationFile() {
		GetFuelSDKPlatform().GetLocalizationFile();
	}

	#region ===================================== Callbacks =====================================

	/// <summary>
	/// Sets the listener.
	/// </summary>
	/// <param name="listener">Listener.</param>
	/// 
	/*
	public static void setListener(FuelSDKListener listener)
	{
		if (listener == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "setting undefined listener");
		} else {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "setting listener");
		}

		m_listener = listener;
	}
	*/
	/// <summary>
	/// Clears all received notifications from the notification centre and unschedule any set
	/// local notifications that haven't fired yet. iOS only. Unused by Android.
	/// </summary>
	public static void CancelAllNotifications()
	{
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "clearing all notifications");

		if (!Application.isEditor) {
			GetFuelSDKPlatform().CancelAllNotifications ();
		}
	}

	/// <summary>
	/// Restores cancelled Propeller SDK local notifications
	/// </summary>
	public static void RestoreAllLocalNotifications ()
	{
		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "restoring all local notifications");

		if (!Application.isEditor) {
			GetFuelSDKPlatform().RestoreAllLocalNotifications();
		}
	}




	/*
	private enum DataReceiverAction
	{
		none,
		fuelSDKSocialInviteRequest,
		fuelSDKSocialShareRequest,
		fuelSDKImplicitLaunchRequest,
		fuelSDKUserValues,
		fuelSDKIgniteEvents,
		fuelSDKIgniteSampleEvents,
		fuelSDKIgniteLeaderBoard,
		fuelSDKIgniteQuest,
		fuelSDKIgniteOffer,
		fuelSDKIgniteAcceptOffer,
		fuelSDKIgniteJoinEvent,
		fuelSDKLocalizationLoaded,
		fuelSDKReceiveData,
		fuelSDKUserInfo,
		fuelSDKUserUpdated,
		fuelSDKUserAvatars,
	}
	*/

	//Events


	//fuelSDKSocialLoginRequest
	public delegate void protofuelSDKSocialLoginRequest(Dictionary<string, object> data);
	public static event protofuelSDKSocialLoginRequest broadcastFuelSDKSocialLoginRequest;

	//fuelSDKUserValues
	public delegate void protofuelSDKUserValues(Dictionary<string, object> data);
	public static event protofuelSDKUserValues broadcastFuelSDKUserValues;


	//fuelSDKNotificationDisabled
	public delegate void protofuelSDKNotificationDisabled(Dictionary<string, object> data);
	public static event protofuelSDKNotificationDisabled broadcastFuelSDKNotificationDisabled;


	//fuelSDKNotificationEnabled
	public delegate void protofuelSDKNotificationEnabled(Dictionary<string, object> data);
	public static event protofuelSDKNotificationEnabled broadcastFuelSDKNotificationEnabled;

	//fuelSDKVirtualGoodConsumeSuccess,
	public delegate void protofuelSDKVirtualGoodConsumeSuccess(Dictionary<string, object> data);
	public static event protofuelSDKVirtualGoodConsumeSuccess broadcastFuelSDKVirtualGoodConsumeSuccess;


	//fuelSDKVirtualGoodRollback
	public delegate void protofuelSDKVirtualGoodRollback(Dictionary<string, object> data);
	public static event protofuelSDKVirtualGoodRollback broadcastFuelSDKVirtualGoodRollback;

	//fuelSDKVirtualGoodList
	public delegate void protofuelSDKVirtualGoodList(Dictionary<string, object> data);
	public static event protofuelSDKVirtualGoodList broadcastFuelSDKVirtualGoodList;

	//fuelSDKServerAPIStatusUpdated
	public delegate void protofuelSDKServerAPIStatusUpdated(Dictionary<string, object> data);
	public static event protofuelSDKServerAPIStatusUpdated broadcastFuelSDKServerAPIStatusUpdated;

	//fuelSDKDynamicsLoaded
	public delegate void protofuelSDKDynamicsLoaded(Dictionary<string, object> data);
	public static event protofuelSDKDynamicsLoaded broadcastFuelSDKDynamicsLoaded;

	//fuelSDKIgniteLoaded
	public delegate void protofuelSDKIgniteLoaded(Dictionary<string, object> data);
	public static event protofuelSDKIgniteLoaded broadcastFuelSDKIgniteLoaded;

	//fuelSDKIgniteSampleEvents
	public delegate void protofuelSDKIgniteSampleEvents(Dictionary<string, object> data);
	public static event protofuelSDKIgniteSampleEvents broadcastFuelSDKIgniteSampleEvents;

	//fuelSDKIgniteEvents
	public delegate void protofuelSDKIgniteEvents(Dictionary<string, object> data);
	public static event protofuelSDKIgniteEvents broadcastFuelSDKIgniteEvents;

	//fuelSDKIgniteMission
	public delegate void protofuelSDKIgniteMission(Dictionary<string, object> data);
	public static event protofuelSDKIgniteMission broadcastFuelSDKIgniteMission;

	//fuelSDKUICompetedExit
	public delegate void protofuelSDKUICompetedExit();
	public static event protofuelSDKUICompetedExit broadcastFuelSDKUICompetedExit;

	//fuelSDKUICompetedFail
	public delegate void protofuelSDKUICompetedFail(string reason);
	public static event protofuelSDKUICompetedFail broadcastFuelSDKUICompetedFail;

	//fuelSDKUICompetedMatch
	public delegate void protofuelSDKUICompetedMatch(Dictionary<string, object> data);
	public static event protofuelSDKUICompetedMatch broadcastFuelSDKUICompetedMatch;

	//fuelSDKCompeteChallengeCount
	public delegate void protofuelSDKCompeteChallengeCount(Dictionary<string, object> data);
	public static event protofuelSDKCompeteChallengeCount broadcastFuelSDKCompeteChallengeCount;

	//fuelSDKCompeteTournamentInfo
	public delegate void protofuelSDKCompeteTournamentInfo(Dictionary<string, string> data);
	public static event protofuelSDKCompeteTournamentInfo broadcastFuelSDKCompeteTournamentInfo;


	/// <summary>
	/// Data Receiver
	/// </summary>
	/// <param name="message">data</param>
	private void DataReceiver (string message)
	{
		//if (m_listener == null) {
		//	FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "Fuel SDK listener has not been set");
		//	return;
		//}

		Debug.Log ("FUEL DEBUG LOG ::: DataReceiver message = " + message);



		if (message == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "received undefined message");
			return;
		}


		object messageObject = FuelSDKCommon.Deserialize (message);

		if (messageObject == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "message could not be deserialized");
			return;
		}

		Dictionary<string, object> messageDictionary = null;

		try{

			messageDictionary = messageObject as Dictionary<string, object>;

			if (messageDictionary == null) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, " message data type: " + messageObject.GetType ().Name);
				return;
			}

		}catch(Exception e) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, " message data type: " + messageObject.GetType ().Name + " error message : " + e.Message);
			return;
		}

		object actionObject;
		bool keyExists = messageDictionary.TryGetValue ("action", out actionObject);

		if (actionObject == null || keyExists == false) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "received undefined action for message: " + message);
			return;
		}

		if (!(actionObject is string)) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid action data type: " + actionObject.GetType ().Name);
			return;
		}

		string action = (string)actionObject;
		DataReceiverAction dataReceiverAction = DataReceiverAction.none;

		if (!FuelSDKCommon.TryParseEnum<DataReceiverAction> (action, out dataReceiverAction)) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "unsupported action: " + action);
			return;
		}

		object dataObject;
		keyExists = messageDictionary.TryGetValue ("data", out dataObject);

		if (dataObject == null || keyExists == false) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "no specific data in the response object for action: " + action);
			return;
		}

		Dictionary<string, object> data = null;

		try{

			data = dataObject as Dictionary<string, object>;

			if (data == null) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid data data type" + dataObject.GetType ().Name);
				return;
			}

		}catch(Exception e){
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid data data type" + dataObject.GetType ().Name + " error message : " + e.Message);
			return;
		}

		string dataString = FuelSDKCommon.Serialize (data);

		if (dataString == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "data could not be serialized");
			return;
		}

		FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG, "received '" + action + "': " + dataString);


		Debug.Log ("FUEL DEBUG LOG ::: DataReceiver Switch * dataReceiverAction = " + dataReceiverAction);

		switch (dataReceiverAction) {
		case DataReceiverAction.none:
			{
				// noop
				break;
			}
		case DataReceiverAction.fuelSDKVirtualGoodList:
			{
				/*
				object transactionIDObject;
				keyExists = data.TryGetValue("transactionID", out transactionIDObject);

				if (transactionIDObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected transaction ID");
					break;
				}

				if (!(transactionIDObject is string)) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid transaction ID data type: " + transactionIDObject.GetType ().Name);
					break;
				}

				string transactionID = (string)transactionIDObject;


				object virtualGoodsObject;
				keyExists = data.TryGetValue("virtualGoods", out virtualGoodsObject);

				if (virtualGoodsObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected virtual goods list");
					break;
				}

				List<object> virtualGoods = null;

				try{

					virtualGoods = virtualGoodsObject as List<object>;

					if (virtualGoods == null) {
						FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid virtual goods list data type: " + virtualGoodsObject.GetType ().Name);
						break;
					}

				}catch(Exception e){
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid virtual goods list data type: " + virtualGoodsObject.GetType ().Name + " error message : " + e.Message);
					break;
				}
				*/

				if (broadcastFuelSDKVirtualGoodList != null) {
					broadcastFuelSDKVirtualGoodList (data);
				}

				//m_listener.OnVirtualGoodList (transactionID, virtualGoods);
				break;
			}
		case DataReceiverAction.fuelSDKVirtualGoodRollback:
			{
				/*
				object transactionIDObject;
				keyExists = data.TryGetValue("transactionID", out transactionIDObject);

				if (transactionIDObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected transaction ID");
					break;
				}

				if (!(transactionIDObject is string)) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid transaction ID data type: " + transactionIDObject.GetType ().Name);
					break;
				}
				*/

				if (broadcastFuelSDKVirtualGoodRollback != null) {
					broadcastFuelSDKVirtualGoodRollback (data);
				}

				//m_listener.OnVirtualGoodRollback ((string)transactionIDObject);
				break;
			}
		case DataReceiverAction.fuelSDKVirtualGoodConsumeSuccess:
			{
				/*
				object transactionIDObject;
				keyExists = data.TryGetValue("transactionID", out transactionIDObject);

				if (transactionIDObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected transaction ID");
					break;
				}

				if (!(transactionIDObject is string)) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid transaction ID data type: " + transactionIDObject.GetType ().Name);
					break;
				}
				*/

				if (broadcastFuelSDKVirtualGoodConsumeSuccess != null) {
					broadcastFuelSDKVirtualGoodConsumeSuccess (data);
				}

				//m_listener.OnVirtualGoodConsumeSuccess ((string)transactionIDObject);
				break;	
			}
		case DataReceiverAction.fuelSDKNotificationEnabled:
			{
				/*
				object notificationTypeObject;
				keyExists = data.TryGetValue("notificationType", out notificationTypeObject);

				if (notificationTypeObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected notification type");
					break;
				}

				if (!(notificationTypeObject is long)) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid notification type data type: " + notificationTypeObject.GetType ().Name);
					break;
				}

				int notificationTypeValue = (int)((long)notificationTypeObject);

				if (!Enum.IsDefined (typeof (NotificationType), notificationTypeValue)) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "unsuppported notification type value: " + notificationTypeValue.ToString ());
					break;
				}
				*/

				if (broadcastFuelSDKNotificationEnabled != null) {
					broadcastFuelSDKNotificationEnabled (data);
				}

				//m_listener.OnNotificationEnabled ((NotificationType)notificationTypeValue);
				break;
			}
		case DataReceiverAction.fuelSDKNotificationDisabled:
			{
				/*
				object notificationTypeObject;
				keyExists = data.TryGetValue("notificationType", out notificationTypeObject);

				if (notificationTypeObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected notification type");
					break;
				}

				if (!(notificationTypeObject is long)) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid notification type data type: " + notificationTypeObject.GetType ().Name);
					break;
				}

				int notificationTypeValue = (int)((long)notificationTypeObject);

				if (!Enum.IsDefined (typeof (NotificationType), notificationTypeValue)) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "unsuppported notification type value: " + notificationTypeValue.ToString ());
					break;
				}
				*/
				if (broadcastFuelSDKNotificationDisabled != null) {
					broadcastFuelSDKNotificationDisabled (data);
				}

				//m_listener.OnNotificationDisabled ((NotificationType)notificationTypeValue);
				break;
			}
		case DataReceiverAction.fuelSDKSocialLoginRequest:
			{
				/*
				object allowCacheObject;
				keyExists = data.TryGetValue("allowCache", out allowCacheObject);

				if (allowCacheObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected allow cache flag");
					break;
				}

				if (!(allowCacheObject is bool)) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid allow cache data type: " + allowCacheObject.GetType ().Name);
					break;
				}

				bool allowCache = (bool)allowCacheObject;
				*/

				if (broadcastFuelSDKSocialLoginRequest != null) {
					broadcastFuelSDKSocialLoginRequest (data);
				}

				break;
			}
		case DataReceiverAction.fuelSDKSocialInviteRequest:
			{
				//m_listener.OnSocialInvite (FuelSDKCommon.ToStringDictionary<string, object> (data));
				break;
			}
		case DataReceiverAction.fuelSDKSocialShareRequest:
			{
				//m_listener.OnSocialShare (FuelSDKCommon.ToStringDictionary<string, object> (data));
				break;
			}
		case DataReceiverAction.fuelSDKImplicitLaunchRequest:
			{
				object applicationStateObject;
				keyExists = data.TryGetValue("applicationState", out applicationStateObject);

				if (applicationStateObject == null) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected application state");
					break;
				}

				if (!(applicationStateObject is string)) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid application state data type: " + applicationStateObject.GetType ().Name);
					break;
				}

				string applicationStateString = (string)applicationStateObject;

				ApplicationState applicationState = ApplicationState.none;

				if (!FuelSDKCommon.TryParseEnum<ApplicationState> (applicationStateString, out applicationState)) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "unsupported application state: " + applicationStateString);
					return;
				}

				//m_listener.OnImplicitLaunch (applicationState);
				break;
			}
		case DataReceiverAction.fuelSDKUserValues:
			{
				/*
				object conditionsObject;
				keyExists = data.TryGetValue("dynamicConditions", out conditionsObject);

				if (conditionsObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected dynamic conditions");
					break;
				}

				Dictionary<string, object> conditions = null;

				try{
					conditions = conditionsObject as Dictionary<string, object>;

					if (conditions == null) {
						FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid conditions data type: " + conditionsObject.GetType ().Name);
						break;
					}
				}catch(Exception e){
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid conditions data type: " + conditionsObject.GetType ().Name + " error message : " + e.Message);
					break;
				}

				object variablesObject;
				keyExists = data.TryGetValue("variables", out variablesObject);

				if (variablesObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected dynamic variables");
					break;
				}

				Dictionary<string, object> variables = null;

				try{
					variables = variablesObject as Dictionary<string, object>;

					if (variables == null) {
						FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid variables data type: " + variablesObject.GetType ().Name);
						break;
					}
				}catch(Exception e){
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid variables data type: " + variablesObject.GetType ().Name + " error message : " + e.Message);
					break;
				}
				*/

				if (broadcastFuelSDKUserValues != null) {
					broadcastFuelSDKUserValues (data);
				}
				break;
			}
		case DataReceiverAction.fuelSDKCompeteChallengeCount:
			{
				/*
				object countObject;
				keyExists = data.TryGetValue("count", out countObject);

				if (countObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected challenge count");
					break;
				}

				if (!(countObject is long)) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid challenge count data type: " + countObject.GetType ().Name);
					break;
				}

				int count = (int)((long)countObject);
				*/

				if (broadcastFuelSDKCompeteChallengeCount != null) {
					broadcastFuelSDKCompeteChallengeCount (data);
				}
				break;
			}
		case DataReceiverAction.fuelSDKCompeteTournamentInfo:
			{
				if (broadcastFuelSDKCompeteTournamentInfo != null) {
					broadcastFuelSDKCompeteTournamentInfo (FuelSDKCommon.ToStringDictionary<string, object> (data));
				}
				break;
			}
		case DataReceiverAction.fuelSDKCompeteUICompletedExit:
			{
				if (broadcastFuelSDKUICompetedExit != null) {
					broadcastFuelSDKUICompetedExit ();
				}
				break;
			}
		case DataReceiverAction.fuelSDKCompeteUICompletedMatch:
			{
				if (broadcastFuelSDKUICompetedMatch != null) {
					broadcastFuelSDKUICompetedMatch (data);
				}
				break;
			}
		case DataReceiverAction.fuelSDKCompeteUICompletedFail:
			{
				object reasonObject;
				keyExists = data.TryGetValue("message", out reasonObject);

				if (reasonObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected failure reason");
					break;
				}

				if (!(reasonObject is string)) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid failure reason data type: " + reasonObject.GetType ().Name);
					break;
				}

				string reason = (string)reasonObject;

				if (broadcastFuelSDKUICompetedFail != null) {
					broadcastFuelSDKUICompetedFail (reason);
				}

				//m_listener.OnCompeteUIFailed (reason);
				break;
			}
		case DataReceiverAction.fuelSDKIgniteEvents:
			{
				if (!validateDataReceived (dataReceiverAction, data)) {
					break;
				}

				if (broadcastFuelSDKIgniteEvents != null) {
					broadcastFuelSDKIgniteEvents (data);
				}

				/*
				object eventsObject;
				keyExists = data.TryGetValue("events", out eventsObject);

				if (eventsObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected event list");
					break;
				}

				List<object> events = null;

				try{
					events = eventsObject as List<object>;

					if (events == null) {
						FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid event list data type: " + eventsObject.GetType ().Name);
						break;
					}
				}catch(Exception e){

					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid event list data type: " + eventsObject.GetType ().Name + " error message : " + e.Message);
					break;
				}
				*/
				//m_listener.OnIgniteEvents (events);
				break;
			}
		case DataReceiverAction.fuelSDKIgniteSampleEvents:
			{

				if (!validateDataReceived (dataReceiverAction, data)) {
					break;
				}

				if (broadcastFuelSDKIgniteSampleEvents != null) {
					broadcastFuelSDKIgniteSampleEvents (data);
				}

				/*
				object eventsObject;
				keyExists = data.TryGetValue("events", out eventsObject);

				if (eventsObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected event list");
					break;
				}

				List<object> events = null;

				try{
					events = eventsObject as List<object>;

					if (events == null) {
						FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid event list data type: " + eventsObject.GetType ().Name);
						break;
					}
				}catch(Exception e){

					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid event list data type: " + eventsObject.GetType ().Name + " error message : " + e.Message);
					break;
				}
				*/
				//m_listener.OnIgniteSampleEvents (events);
				break;
			}
					case DataReceiverAction.fuelSDKIgniteLeaderBoard:
			{
				if (!validateDataReceived (dataReceiverAction, data)) {
					break;
				}

				object leaderBoardObject;
				keyExists = data.TryGetValue("leaderBoard", out leaderBoardObject);

				if (leaderBoardObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected leaderboard data");
					break;
				}

				Dictionary<string, object> leaderBoard = null;

				try{
					leaderBoard = leaderBoardObject as Dictionary<string, object>;

					if (leaderBoard == null) {
						FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid leaderboard data type: " + leaderBoardObject.GetType ().Name);
						break;
					}
				}catch(Exception e){
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid leaderboard data type: " + leaderBoardObject.GetType ().Name + " error message : " + e.Message);
					break;
				}

				//m_listener.OnIgniteLeaderBoard (leaderBoard);
				break;
			}
		case DataReceiverAction.fuelSDKIgniteMission:
			{
				if (!validateDataReceived (dataReceiverAction, data)) {
					break;
				}

				if (broadcastFuelSDKIgniteMission != null) {
					broadcastFuelSDKIgniteMission (data);
				}

				/*
				object missionObject;
				keyExists = data.TryGetValue("mission", out missionObject);

				if (missionObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected mission data");
					break;
				}

				Dictionary<string, object> mission = null;

				try{
					mission = missionObject as Dictionary<string, object>;

					if (mission == null) {
						FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid mission data type: " + missionObject.GetType ().Name);
						break;
					}
				}catch(Exception e){
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid mission data type: " + missionObject.GetType ().Name + " error message : " + e.Message);
					break;
				}
				*/
				//m_listener.OnIgniteMission (mission);
				break;
			}
		case DataReceiverAction.fuelSDKIgniteQuest:
			{
				if (!validateDataReceived (dataReceiverAction, data)) {
					break;
				}

				object questObject;
				keyExists = data.TryGetValue("quest", out questObject);

				if (questObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected quest data");
					break;
				}

				Dictionary<string, object> quest = null;

				try{
					quest = questObject as Dictionary<string, object>;

					if (quest == null) {
						FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid quest data type: " + questObject.GetType ().Name);
						break;
					}
				}catch(Exception e){
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid quest data type: " + questObject.GetType ().Name + " error message : " + e.Message);
					break;
				}

				//m_listener.OnIgniteQuest (quest);
				break;
			}
		case DataReceiverAction.fuelSDKIgniteOffer:
			{
				if (!validateDataReceived (dataReceiverAction, data))
				{
					break;
				}

				object offerObject;
				keyExists = data.TryGetValue("offer", out offerObject);

				if ((offerObject == null) || (keyExists == false))
				{
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected offer data");
					break;
				}

				Dictionary<string, object> offer = null;

				try
				{
					offer = offerObject as Dictionary<string, object>;

					if (offer == null)
					{
						FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid offer data type: " + offerObject.GetType ().Name);
						break;
					}
				}
				catch (Exception exception)
				{
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid offer data type: " + offerObject.GetType ().Name + " error message : " + exception.Message);
					break;
				}

				//m_listener.OnIgniteOffer (offer);
				break;
			}
		case DataReceiverAction.fuelSDKIgniteAcceptOffer:
			{
				/*
				if (!validateDataReceived (dataReceiverAction, data))
				{
					break;
				}

				object offerIDObject;
				keyExists = data.TryGetValue("offerID", out offerIDObject);

				if ((offerIDObject == null) || (keyExists == false))
				{
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected offer ID");
				}

				if (!(offerIDObject is string)) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid offer ID data type: " + offerIDObject.GetType ().Name);
					break;
				}

				string offerID = (string)offerIDObject;

				object acceptedObject;
				keyExists = data.TryGetValue("accepted", out acceptedObject);

				if ((acceptedObject == null) || (keyExists == false))
				{
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected accepted status");
				}

				if (!(acceptedObject is bool)) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid accepted status data type: " + acceptedObject.GetType ().Name);
					break;
				}

				bool accepted = (bool)acceptedObject;
*/

				//m_listener.OnIgniteAcceptOffer (offerID, accepted);
				break;
			}
		case DataReceiverAction.fuelSDKIgniteJoinEvent:
			{
				/*
				if (!validateDataReceived (dataReceiverAction, data)) {
					break;
				}

				object eventIDObject;
				keyExists = data.TryGetValue("eventID", out eventIDObject);

				if (eventIDObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected event ID");
					break;
				}

				if (!(eventIDObject is string)) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid event ID data type: " + eventIDObject.GetType ().Name);
					break;
				}

				string eventID = (string)eventIDObject;

				object joinStatusObject;
				keyExists = data.TryGetValue("joinStatus", out joinStatusObject);

				if (joinStatusObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected join status");
					break;
				}

				if (!(joinStatusObject is bool)) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid join status data type: " + joinStatusObject.GetType ().Name);
					break;
				}

				bool joinStatus = (bool)joinStatusObject;
				*/
				//m_listener.OnIgniteJoinEvent (eventID, joinStatus);
				break;
			}
		case DataReceiverAction.fuelSDKLocalizationLoaded:
			{
				object fileNameObject;
				keyExists = data.TryGetValue("fileName", out fileNameObject);

				if (fileNameObject == null || keyExists == false) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "missing expected localization file path");
					break;
				}

				if (!(fileNameObject is string)) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid localization file path data type: " + fileNameObject.GetType ().Name);
					break;
				}

				string filePath = (string)fileNameObject;

				if(string.IsNullOrEmpty(filePath)){
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "invalid  file path, values is empty ");
					break;
				}

				//m_listener.OnLocalizationData(filePath);
				break;
			}
		case DataReceiverAction.fuelSDKUserInfo:
			{

				//m_listener.OnUserInfo(data);
				break;
			}
		case DataReceiverAction.fuelSDKUserUpdated:
			{

				//m_listener.OnUpdatedUserInfo(data);
				break;
			}
		case DataReceiverAction.fuelSDKUserAvatars:
			{
				if (!validateDataReceived (dataReceiverAction, data)) {
					break;
				}
				//m_listener.OnUserAvatars(data);
				break;
			}
		case DataReceiverAction.fuelSDKReceiveData:
			{
				//m_listener.OnReceiveData(data);
				break;
			}

		case DataReceiverAction.fuelSDKIgniteLoaded:
			{
				if (broadcastFuelSDKIgniteLoaded != null) {
					broadcastFuelSDKIgniteLoaded (data);
				}
				break;
			}
		case DataReceiverAction.fuelSDKDynamicsLoaded:
			{
				if (broadcastFuelSDKDynamicsLoaded != null) {
					broadcastFuelSDKDynamicsLoaded (data);
				}
				break;
			}
		case DataReceiverAction.fuelSDKServerAPIStatusUpdated:
			{
				if (broadcastFuelSDKServerAPIStatusUpdated != null) {
					broadcastFuelSDKServerAPIStatusUpdated (data);
				}
				break;
			}
		default:
			{
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.WARN, "unsupported action: " + action);
				break;
			}
		}
	}

	private bool validateDataReceived(DataReceiverAction action, Dictionary<string, object> data)
	{
		if (data == null) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, action.ToString () + " - undefined data");
			return false;
		}

		object successObject;
		bool keyExists = data.TryGetValue("success", out successObject);

		if (successObject == null || keyExists == false) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, action.ToString () + " - missing expected 'success' field");
			return false;
		}

		if (!(successObject is bool)) {
			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, action.ToString () + " - invalid success result data type: " + successObject.GetType ().Name);
			return false;
		}

		bool success = (bool)successObject;

		if (!success) {
			object reasonObject;
			keyExists = data.TryGetValue("reason", out reasonObject);

			if (reasonObject == null || keyExists == false) {
				reasonObject = "undefined failure reason";
			}

			if (!(reasonObject is string)) {
				reasonObject = "invalid failure reason data type: " + reasonObject.GetType ().Name;
			}

			FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, action.ToString () + " - " + reasonObject.ToString ());
		}

		return success;
	}

	#endregion

}
