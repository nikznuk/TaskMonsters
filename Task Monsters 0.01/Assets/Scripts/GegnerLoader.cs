using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class GegnerLoader : MonoBehaviour {

	private string jsonString;
	private JsonData itemData;

	void Start () {
		jsonString = File.ReadAllText (Path.Combine(Application.dataPath, "Scripts/animyJson.json"));
		itemData = JsonMapper.ToObject (jsonString);

		Debug.Log (GetAnimyData());
	}

	public JsonData GetAnimyData ()
	{
		return itemData [0][0][2][0][0];
	}
}
