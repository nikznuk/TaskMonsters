using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class GegnerLoader : MonoBehaviour {

	private string jsonString;
	private JsonData itemData;

	// Use this for initialization
	void Start () {
		jsonString = File.ReadAllText (Path.Combine(Application.dataPath, "Scripts/animyJson.json"));
		itemData = JsonMapper.ToObject (jsonString);

		Debug.Log (GetAnimyData());
		Debug.Log (itemData [0][0][1]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public JsonData GetAnimyData ()
	{
		return itemData [0][1][0];
	}
}
