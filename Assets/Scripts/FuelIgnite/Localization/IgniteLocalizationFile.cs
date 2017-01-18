using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FuelSDKIntegration.IgniteLocalization {

	public interface IgniteLocalizationFile  {
		bool Read(string filePath);
		Dictionary<string, object> ToDictionary();
	}

}
