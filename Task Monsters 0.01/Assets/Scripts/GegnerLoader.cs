using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class GegnerLoader : MonoBehaviour {

	public Animy animy;

	private string jsonString;
	private JsonData animyData;

	public void CreateNewMonster (string category, int animyNumber) {

		jsonString = File.ReadAllText (Application.dataPath + "/Scripts/Json/monsterJson.json");
		animyData = JsonMapper.ToObject (jsonString);

		animy.name = (string)animyData [category] [animyNumber] ["Name"];
		animy.health = (int)animyData [category] [animyNumber] ["Health"];
	}
}