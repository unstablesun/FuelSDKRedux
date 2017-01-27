using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FuelSDKMiniJSON;

public class TestFuelSDK : MonoBehaviour {

	#region ===================================== Editor Variables =====================================
	
	[SerializeField]
	protected string messageStringTest;

	#endregion

	#region ===================================== Editor Variables =====================================

	protected FuelSDK fuelSDKInstance;

	#endregion

	TimeSpan epochTicks;
	TimeSpan start1;
	TimeSpan end1;

	TimeSpan start2;
	TimeSpan end2;

	TimeSpan start3;
	TimeSpan end3;

	#region ===================================== MonoBehaviour =====================================

	void Awake () {
		fuelSDKInstance = GetComponent<FuelSDK>();

		/* Unused variables are errors in sonic
		epochTicks = new TimeSpan(new DateTime(1970, 1, 1).Ticks);
		start1 = new TimeSpan(DateTime.UtcNow.Ticks) - epochTicks;
		end1 = new TimeSpan(DateTime.UtcNow.AddMinutes(1).Ticks) - epochTicks;

		start2 = new TimeSpan(DateTime.UtcNow.AddMinutes(1).Ticks) - epochTicks;
		end2 = new TimeSpan(DateTime.UtcNow.AddMinutes(2).Ticks) - epochTicks;

		start3 = new TimeSpan(DateTime.UtcNow.AddMinutes(2).Ticks) - epochTicks;
		end3 = new TimeSpan(DateTime.UtcNow.AddMinutes(3).Ticks) - epochTicks;
		*/
	}

	void OnGUI() {
		
		if (GUI.Button(new Rect(10, 10, 130, 30), "Test Fuel Message")) {
			TestFuelMessage();
		}

		/*
		if (GUI.Button(new Rect(10, 10, 130, 30), "Test Fuel Message")) {
			SendEvents();
		}

		if (GUI.Button(new Rect(10, 50, 130, 30), "Join Event")) {
			JoinEvent();
		}

		if (GUI.Button(new Rect(10, 100, 130, 30), "Recieve VirtualGood")) {
			RecieveVirtualGood();
		}

		if (GUI.Button(new Rect(10, 150, 130, 30), "Send VirtualGood")) {
			SendVirtualGood();
		}
		*/
	}
	
	#endregion

	#region ===================================== Buttons =====================================

	void TestFuelMessage() {
		if( fuelSDKInstance != null && !String.IsNullOrEmpty(messageStringTest) ) {
			fuelSDKInstance.gameObject.SendMessage( "DataReceiver" , messageStringTest );
		}
	}

	#if UNITY_EDITOR

	void SendEvents() {
		if( fuelSDKInstance == null ) {
			return;
		}

		string eventMessage = "{\"data\":{\"success\":true,\"events\":[{\"id\":\"57feb798e229dc042d006bc8\",\"startTime\":"+start1.TotalSeconds+",\"authorized\":true,\"achieved\":false,\"joined\":false,\"eventId\":\"57feb798e229dc042d006bd2\",\"score\":18,\"state\":\"active\",\"type\":0,\"endTime\": "+end1.TotalSeconds+",\"metadata\":{\"imageUrl\":\"\\/\\/cdn.fuelpowered.com\\/images\\/0b65676e0758f6c8000fdf40d07a0bb4_default.png\",\"name\":\"Cascade Test\",\"cascadeInterval\": 30,\"cascadeId\":\"57feb798e229dc042d006bd5\",\"joinedLeaderboard\": \"\"}},{\"id\":\"57feb797e229dc042d006bb7\",\"startTime\": "+start2.TotalSeconds+",\"authorized\":true,\"achieved\":false,\"joined\":false,\"eventId\":\"57feb797e229dc042d006bc1\",\"score\":-1,\"state\":\"active\",\"type\":0,\"endTime\": "+end2.TotalSeconds+",\"metadata\":{\"imageUrl\":\"\\/\\/cdn.fuelpowered.com\\/images\\/0b65676e0758f6c8000fdf40d07a0bb4_default.png\",\"name\":\"Cascade Test\",\"cascadeInterval\": 30,\"cascadeId\":\"57feb798e229dc042d006bd5\",\"joinedLeaderboard\": \"\"}},{\"id\":\"57feb797e229dc042d006ba6\",\"startTime\": "+start3.TotalSeconds+",\"authorized\":true,\"achieved\":false,\"joined\":false,\"eventId\":\"57feb797e229dc042d006bb0\",\"score\":-1,\"state\":\"active\",\"type\":0,\"endTime\": "+end3.TotalSeconds+",\"metadata\":{\"imageUrl\":\"\\/\\/cdn.fuelpowered.com\\/images\\/0b65676e0758f6c8000fdf40d07a0bb4_default.png\",\"name\":\"Cascade Test\",\"cascadeInterval\": 30,\"cascadeId\":\"57feb798e229dc042d006bd5\",\"joinedLeaderboard\": \"\"}}]},\"action\":\"fuelSDKIgniteEvents\"}";
		fuelSDKInstance.gameObject.SendMessage( "DataReceiver" , eventMessage );

		SendLeaderBoard();
	}

	void SendLeaderBoard() {
		if( fuelSDKInstance == null ) {
			return;
		}

		string leaderBoardMessage1 = "{\"data\":{\"success\":true,\"leaderBoard\":{\"currentUserRank\":{\"countryCode\":\"us\",\"score\":0,\"id\":\"54ac91c6636f6f031b923700\",\"name\":\"serolo\"},\"leaderList\":{\"state\":\"active\",\"leaders\":[{\"countryCode\":\"us\",\"score\":0,\"id\":\"54ac91c6636f6f031b923700\",\"name\":\"serolo\"}]},\"id\":\"57feb798e229dc042d006bc8\",\"currentUserId\":\"54ac91c6636f6f031b923700\",\"rule\":{\"id\":\"57feb798e229dc042d006bc7\",\"score\":0,\"variable\":\"stage\",\"kind\":1,\"metadata\":{\"imageUrl\":\"https:\\/\\/cdn-internal.fuelpowered.com\\/images\\/e2eb9454ff4c87cfeb450169bc6e6b56_default.png\",\"name\":\"Stage Variable Test's rule\"}},\"metadata\":{\"imageUrl\":\"https:\\/\\/cdn-internal.fuelpowered.com\\/images\\/e2eb9454ff4c87cfeb450169bc6e6b56_default.png\",\"participationVirtualGood\":{\"id\":\"57eea07c315873725e00298c\",\"description\":\"Empty Virtual Good\",\"goodId\":\"empty\",\"iconUrl\":\"\\/\\/cdn.fuelpowered.com\\/images\\/0b997a02ebad9932d524fe524affab9d_default.png\"},\"name\":\"Stage Variable Test\",\"virtualGoods\": [ {\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 1\",\"id\": \"57ee9f632dccd13bef00265a\",\"goodId\": \"gems_10|points_10|weapons_10\",\"ratio\": 0.05,\"cap\": 1},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 2\",\"id\": \"57ee9f953158737465002949\",\"goodId\": \"gems_9|points_9|weapons_9\",\"ratio\": 0.05,\"cap\": 1},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 3\",\"id\": \"57ee9fb681bdd92f0500017c\",\"goodId\": \"gems_8|points_8|weapons_8\",\"ratio\": 0.05,\"cap\": 1},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 4\",\"id\": \"57ee9fd62dccd13d3e0028a2\",\"goodId\": \"gems_7|points_7|weapons_7\",\"ratio\": 0.05,\"cap\": 1},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 5\",\"id\": \"57f806e22dccd1526a001a85\",\"goodId\": \"gems_6|points_6|weapons_6\",\"ratio\": 0.05,\"cap\": 1},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 6 to 10\",\"id\": \"57ee9ff731587376450027ef\",\"goodId\": \"gems_2|points_2|weapons_2\",\"ratio\": 0.25,\"cap\": 5},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 11 to 20\",\"id\": \"57eea01331587374b7002704\",\"goodId\": \"gems_1|points_1|weapons_1\",\"ratio\": 0.5,\"cap\": 10}]}}},\"action\":\"fuelSDKIgniteLeaderBoard\"}";
		fuelSDKInstance.gameObject.SendMessage( "DataReceiver" , leaderBoardMessage1 );

		string leaderBoardMessage2 = "{\"data\":{\"success\":true,\"leaderBoard\":{\"currentUserId\":\"54ac91c6636f6f031b923700\",\"progress\":-1,\"id\":\"57feb797e229dc042d006bb7\",\"leaderList\":{\"state\":\"active\",\"leaders\":[]},\"rule\":{\"id\":\"57feb797e229dc042d006bb6\",\"score\":0,\"variable\":\"stage\",\"kind\":1,\"metadata\":{\"imageUrl\":\"https:\\/\\/cdn-internal.fuelpowered.com\\/images\\/e2eb9454ff4c87cfeb450169bc6e6b56_default.png\",\"name\":\"Stage Variable Test's rule\"}},\"metadata\":{\"imageUrl\":\"https:\\/\\/cdn-internal.fuelpowered.com\\/images\\/e2eb9454ff4c87cfeb450169bc6e6b56_default.png\",\"participationVirtualGood\":{\"id\":\"57eea07c315873725e00298c\",\"description\":\"Empty Virtual Good\",\"goodId\":\"empty\",\"iconUrl\":\"\\/\\/cdn.fuelpowered.com\\/images\\/0b997a02ebad9932d524fe524affab9d_default.png\"},\"name\":\"Stage Variable Test\",\"virtualGoods\": [ {\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 1\",\"id\": \"57ee9f632dccd13bef00265a\",\"goodId\": \"gems_10|points_10\",\"ratio\": 0.05,\"cap\": 1},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 2\",\"id\": \"57ee9f953158737465002949\",\"goodId\": \"gems_9|points_9\",\"ratio\": 0.05,\"cap\": 1},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 3\",\"id\": \"57ee9fb681bdd92f0500017c\",\"goodId\": \"gems_8|points_8\",\"ratio\": 0.05,\"cap\": 1},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 4\",\"id\": \"57ee9fd62dccd13d3e0028a2\",\"goodId\": \"gems_7|points_7\",\"ratio\": 0.05,\"cap\": 1},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 5\",\"id\": \"57f806e22dccd1526a001a85\",\"goodId\": \"gems_6|points_6\",\"ratio\": 0.05,\"cap\": 1},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 6 to 10\",\"id\": \"57ee9ff731587376450027ef\",\"goodId\": \"gems_2|points_2\",\"ratio\": 0.25,\"cap\": 5},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 11 to 20\",\"id\": \"57eea01331587374b7002704\",\"goodId\": \"gems_1|points_1|weapons_1\",\"ratio\": 0.5,\"cap\": 10}]}}},\"action\":\"fuelSDKIgniteLeaderBoard\"}";
		fuelSDKInstance.gameObject.SendMessage( "DataReceiver" , leaderBoardMessage2 );

		string leaderBoardMessage3 = "{\"data\":{\"success\":true,\"leaderBoard\":{\"currentUserId\":\"54ac91c6636f6f031b923700\",\"progress\":-1,\"id\":\"57feb797e229dc042d006ba6\",\"leaderList\":{\"state\":\"active\",\"leaders\":[]},\"rule\":{\"id\":\"57feb797e229dc042d006ba5\",\"score\":0,\"variable\":\"stage\",\"kind\":1,\"metadata\":{\"imageUrl\":\"https:\\/\\/cdn-internal.fuelpowered.com\\/images\\/e2eb9454ff4c87cfeb450169bc6e6b56_default.png\",\"name\":\"Stage Variable Test's rule\"}},\"metadata\":{\"imageUrl\":\"https:\\/\\/cdn-internal.fuelpowered.com\\/images\\/e2eb9454ff4c87cfeb450169bc6e6b56_default.png\",\"participationVirtualGood\":{\"id\":\"57eea07c315873725e00298c\",\"description\":\"Empty Virtual Good\",\"goodId\":\"empty\",\"iconUrl\":\"\\/\\/cdn.fuelpowered.com\\/images\\/0b997a02ebad9932d524fe524affab9d_default.png\"},\"name\":\"Stage Variable Test\",\"virtualGoods\": [ {\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 1\",\"id\": \"57ee9f632dccd13bef00265a\",\"goodId\": \"gems_10|points_10|weapons_10\",\"ratio\": 0.05,\"cap\": 1},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 2\",\"id\": \"57ee9f953158737465002949\",\"goodId\": \"gems_9|points_9|weapons_9\",\"ratio\": 0.05,\"cap\": 1},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 3\",\"id\": \"57ee9fb681bdd92f0500017c\",\"goodId\": \"gems_8|points_8|weapons_8\",\"ratio\": 0.05,\"cap\": 1},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 4\",\"id\": \"57ee9fd62dccd13d3e0028a2\",\"goodId\": \"gems_7|points_7|weapons_7\",\"ratio\": 0.05,\"cap\": 1},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 5\",\"id\": \"57f806e22dccd1526a001a85\",\"goodId\": \"gems_6|points_6|weapons_6\",\"ratio\": 0.05,\"cap\": 1},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 6 to 10\",\"id\": \"57ee9ff731587376450027ef\",\"goodId\": \"gems_2|points_2|weapons_2\",\"ratio\": 0.25,\"cap\": 5},{\"iconUrl\": \"//cdn.fuelpowered.com/images/0b997a02ebad9932d524fe524affab9d_default.png\",\"description\": \"Rank 11 to 20\",\"id\": \"57eea01331587374b7002704\",\"goodId\": \"gems_1|points_1|weapons_1\",\"ratio\": 0.5,\"cap\": 10}]}}},\"action\":\"fuelSDKIgniteLeaderBoard\"}";
		fuelSDKInstance.gameObject.SendMessage( "DataReceiver" , leaderBoardMessage3 );

	}

	void SendVirtualGood() {
		if( fuelSDKInstance == null ) {
			return;
		}

		string virtualGoodMessage = "{\"action\": \"fuelSDKVirtualGoodList\" , \"data\" : {\"virtualGoods\":[{\"goodId\":\"empty\",\"virtualGoodUrlIcon\":\"\\/\\/cdn.fuelpowered.com\\/images\\/0b997a02ebad9932d524fe524affab9d_default.png\",\"id\":\"57eea07c315873725e00298c\",\"contextMetadata\":\"{\\\"eventName\\\":\\\"SebTest01\\\",\\\"activityName\\\":\\\"Stage Variable Test\\\",\\\"eventUrlIcon\\\":\\\"\\/\\/cdn.fuelpowered.com\\/images\\/5a153d94d0b3ea374b219f39586e63ea_default.png\\\",\\\"rank\\\":1}\",\"context\":\"leaderboardParticipation\",\"acknowledgementToken\":\"57eea07c315873725e00298c:context:leaderboardParticipation:context_id:580f98175ed03217fe00440e:rank:1:timeStamp:1477420801\",\"description\":\"Empty Virtual Good\",\"context_id\":\"580f98175ed03217fe00440e\",\"timestamp\":1477420801},{\"goodId\":\"gems_10|points_10|weapons_10\",\"virtualGoodUrlIcon\":\"\\/\\/cdn.fuelpowered.com\\/images\\/0b997a02ebad9932d524fe524affab9d_default.png\",\"id\":\"57ee9f632dccd13bef00265a\",\"contextMetadata\":\"{\\\"eventName\\\":\\\"SebTest01\\\",\\\"activityName\\\":\\\"Stage Variable Test\\\",\\\"eventUrlIcon\\\":\\\"\\/\\/cdn.fuelpowered.com\\/images\\/5a153d94d0b3ea374b219f39586e63ea_default.png\\\",\\\"rank\\\":1}\",\"context\":\"leaderboardRanking\",\"acknowledgementToken\":\"57ee9f632dccd13bef00265a:context:leaderboardRanking:context_id:580f98175ed03217fe00440e:rank:1:timeStamp:1477420801\",\"description\":\"Rank 1\",\"context_id\":\"580f98175ed03217fe00440e\",\"timestamp\":1477420801}],\"transactionID\":\"132409595\"}}";
		fuelSDKInstance.gameObject.SendMessage( "DataReceiver" , virtualGoodMessage );
	}

	void JoinEvent() {
		//FuelPoweredDataManager.Instance.JoinEvent( "57feb798e229dc042d006bd2");
	}

	void RecieveVirtualGood() {
		//FuelPoweredDataManager.Instance.RecieveVirtualGood( "57feb798e229dc042d006bc8" );
	}

	#endif

	#endregion
}
