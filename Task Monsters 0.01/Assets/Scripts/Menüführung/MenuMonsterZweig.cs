using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class MenuMonsterZweig : MonoBehaviour {

	public List<Button> categoryButtons = new List<Button>();
	public Text monsterName;
	public Text attackenName1;
	public Text attackenName2;
	public Text attackenName3;
	public Text attackenName4;
	public string categoryString = "";

	List <int> activeMonster = new List<int>(){0, 0, 0, 0, 0};
	int categoryInt = 0;

	private string jsonString;
	private JsonData animyData;

	public void SetClickedCategory (string categoryString) {
		this.categoryString = categoryString;
		switch (this.categoryString) {
		case "Stärke":
			categoryInt = 0;
			break;
		case "Gesundheit":
			categoryInt = 1;
			break;
		case "Intelligenz":
			categoryInt = 2;
			break;
		case "Charisma":
			categoryInt = 3;
			break;
		case "Willenskraft":
			categoryInt = 4;
			break;
		}
		SetMonsterData (activeMonster[categoryInt]);
	}

	public void SetMonsterData (int monsterNummer) {
		jsonString = File.ReadAllText (Application.dataPath + "/Scripts/Json/animyJson.json");
		animyData = JsonMapper.ToObject (jsonString);

		Debug.Log (categoryString + " " + monsterNummer + " " + categoryInt);
		monsterName.text = (string)animyData [categoryString] [monsterNummer] ["Name"];
		attackenName1.text = (string)animyData [categoryString] [monsterNummer] ["Attacks"] [0] ["Name"];
		attackenName2.text = (string)animyData [categoryString] [monsterNummer] ["Attacks"] [1] ["Name"];
		attackenName3.text = (string)animyData [categoryString] [monsterNummer] ["Attacks"] [2] ["Name"];
		attackenName4.text = (string)animyData [categoryString] [monsterNummer] ["Attacks"] [3] ["Name"];
	}
}
