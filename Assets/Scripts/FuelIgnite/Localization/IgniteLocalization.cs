using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

namespace FuelSDKIntegration.IgniteLocalization {

	public abstract class  IgniteLocalization<T> where T : IgniteLocalization<T>, new() 
	{
		private static T _instance = new T();
		public static T Instance
		{
			get
			{                
				return _instance;
			}   
		}
			
		private string constantToReplace = "@";
		private CultureInfo cultureInfo;
		protected IgniteLocalizationFile localizationFile;
		protected Dictionary<string, object> languageDict = new Dictionary<string, object>();
		protected abstract string GetSystemLanguage();
		protected abstract bool DoReadLocalizationFile(string filePath);
		protected abstract Dictionary<string,object> DoParseLocalizationFileToDictionary();
		protected abstract string DoFormatLocalizedText(string localizedText, string language);

		/// <summary>
		/// Loads the localization file.
		/// </summary>
		/// <returns><c>true</c>, if localization file was loaded, <c>false</c> otherwise.</returns>
		/// <param name="filePath">File path.</param>
		public bool LoadLocalizationFile(string filePath) {
			try
			{
				string content = "";

				using (StreamReader sr = new StreamReader(filePath))
				{
					string line;
					while ((line = sr.ReadLine()) != null) 
					{
						content += line;
					}
				}

				languageDict = parseToDic(content);
			}
			catch(Exception e)
			{
				Debug.LogError("Cannot read localization file " + e.StackTrace);
			}

			return false;
		}

		/// <summary>
		/// Localize the specified keyCode and listValues.
		/// </summary>
		/// <param name="keyCode">Key code.</param>
		/// <param name="listValues">List values that are going to replace the @ values in the localized text.</param>
		public string LocalizeText(string keyCode, params string[] listValues) {
			
			string finalLocalizedText = "";
			string localizedText =  LocalizeText(keyCode);
			
			string c;
			int position = 0;
			for (int i = 0; i < localizedText.Length; i++) { 
				c = localizedText[i].ToString(); 
				if(c.Equals(constantToReplace)){
					c = listValues[position];
					position++;
				}
				finalLocalizedText += c;
			}
			
			return finalLocalizedText;
		}

		/// <summary>
		/// Localize the specified keyCode.
		/// </summary>
		/// <param name="keyCode">Key code.</param>
		public string LocalizeText(string keyCode) {
			if( languageDict == null ) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG,"there is no localization data loaded");
				return keyCode;
			}
			if (languageDict.Count == 0) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG,"there is no localization data loaded");
				return keyCode;
			}
			if (keyCode == null) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.DEBUG,"Localization key is null");
				return keyCode;
			}
			string language = GetSystemLanguage();
			try
			{
				cultureInfo = new CultureInfo(language);
			}
			catch (System.ArgumentException)
			{
				cultureInfo = CultureInfo.CurrentCulture;
			}
		
			object objectValue = null;
			languageDict.TryGetValue (keyCode, out objectValue);

			if (objectValue == null) {
				return keyCode;
			} else {
				object localizedText = null;
				Dictionary<string, object> dicValue = new Dictionary<string, object>();

				try{
					dicValue = (Dictionary<string, object>)objectValue;
				}catch(Exception e){
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR,"Localization file doesn't have the format excepted " + e.Message);
					return keyCode;
				}

				dicValue.TryGetValue(language, out localizedText);

				if(localizedText == null)
				{
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR,"There is not " + language + " localization for " + keyCode);
					return keyCode;
				}else{
					return string.Format(cultureInfo ,localizedText.ToString());
				}
			}
		}

		private Dictionary<string, object> parseToDic(string content) {
			
			object messageObject = FuelSDKCommon.Deserialize (content);

			if (messageObject == null) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, "message could not be deserialized");
				return null;
			}

			Dictionary<string, object> messageDictionary = null;

			try{

				messageDictionary = messageObject as Dictionary<string, object>;

				if (messageDictionary == null) {
					FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, " message data type: " + messageObject.GetType ().Name);
					return null;
				}

			}catch(Exception e) {
				FuelSDKCommon.Log (FuelSDKCommon.LogLevel.ERROR, " message data type: " + messageObject.GetType ().Name + " error message : " + e.Message);
				return null;
			}

			return messageDictionary;

		}
	}	

}
