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
	int monsterNummer;
	public string categoryString = "";

	List <int> activeMonster = new List<int>(){0, 0, 0, 0, 0};
	int categoryInt = 0;

	KategorieSammlung kategorieSammlung;

	private string jsonString;
	JsonData monsterData;
	private JsonData animyData;

	public Menu menu;
	public MenuMonsterZweig mmz;
	public Transform pref;
	public MenuManager menuManager;
	public Transform parentPanel;

	void Start () {
		kategorieSammlung = new KategorieSammlung ();
		GetAktiveMonster ();
		ReadJsonFile ();
		//WriteJsonFile ();
	}

	// speichert die angeklickte Kategorie 
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
		monsterNummer = activeMonster [categoryInt];
		SetMonsterData ();
	}

	public void SetMonsterData () {
		jsonString = File.ReadAllText (Application.dataPath + "/Scripts/Json/monsterJson.json");
		animyData = JsonMapper.ToObject (jsonString);

		Debug.Log (categoryString + " " + monsterNummer + " " + categoryInt);
		monsterName.text = (string)animyData [categoryString] [monsterNummer] ["Name"];
		attackenName1.text = (string)animyData [categoryString] [monsterNummer] ["Attacks"] [0] ["Name"];
		attackenName2.text = (string)animyData [categoryString] [monsterNummer] ["Attacks"] [1] ["Name"];
		attackenName3.text = (string)animyData [categoryString] [monsterNummer] ["Attacks"] [2] ["Name"];
		attackenName4.text = (string)animyData [categoryString] [monsterNummer] ["Attacks"] [3] ["Name"];
	}

	public void SetMonsterData (int monsterNummer) {
		this.monsterNummer = monsterNummer;
		jsonString = File.ReadAllText (Application.dataPath + "/Scripts/Json/monsterJson.json");
		animyData = JsonMapper.ToObject (jsonString);

		Debug.Log (categoryString + " " + monsterNummer + " " + categoryInt);
		monsterName.text = (string)animyData [categoryString] [monsterNummer] ["Name"];
		attackenName1.text = (string)animyData [categoryString] [monsterNummer] ["Attacks"] [0] ["Name"];
		attackenName2.text = (string)animyData [categoryString] [monsterNummer] ["Attacks"] [1] ["Name"];
		attackenName3.text = (string)animyData [categoryString] [monsterNummer] ["Attacks"] [2] ["Name"];
		attackenName4.text = (string)animyData [categoryString] [monsterNummer] ["Attacks"] [3] ["Name"];
	}

	public void AktivesMonsterAufleveln (string category, int xp) {
		
	}

	public void MonsterAuswaehlen () {
		activeMonster [categoryInt] = monsterNummer;
		SaveAktiveMonster ();
	}

	void GetAktiveMonster () {
		activeMonster [0] = PlayerPrefs.GetInt ("Aktive Monster Stärke");
		activeMonster [1] = PlayerPrefs.GetInt ("Aktive Monster Gesundheit");
		activeMonster [2] = PlayerPrefs.GetInt ("Aktive Monster Intelligenz");
		activeMonster [3] = PlayerPrefs.GetInt ("Aktive Monster Charisma");
		activeMonster [4] = PlayerPrefs.GetInt ("Aktive Monster Willenskraft");
	}

	public void SaveAktiveMonster () {
		PlayerPrefs.SetInt ("Aktive Monster Stärke", activeMonster [0]);
		PlayerPrefs.SetInt ("Aktive Monster Gesundheit", activeMonster [1]);
		PlayerPrefs.SetInt ("Aktive Monster Intelligenz", activeMonster [2]);
		PlayerPrefs.SetInt ("Aktive Monster Charisma", activeMonster [3]);
		PlayerPrefs.SetInt ("Aktive Monster Willenskraft", activeMonster [4]);
	}

	public void CalculatePanelSize () {

		jsonString = File.ReadAllText (Application.dataPath + "/Scripts/Json/monsterJson.json");
		animyData = JsonMapper.ToObject (jsonString);

		int animyAnzahl = animyData[mmz.categoryString].Count;

		parentPanel.GetComponent<RectTransform> ().offsetMax = new Vector2 ((animyAnzahl + 1) * 25 + animyAnzahl * 300 ,0);
		parentPanel.GetComponent<RectTransform> ().offsetMin = new Vector2 ((animyAnzahl + 1) * -25 - animyAnzahl * 300 ,0);

		for (int i = 0; i < animyData [mmz.categoryString].Count; i++) {
			Transform go = Instantiate (pref);
			go.transform.parent = parentPanel;
			go.GetComponent<RectTransform> ().offsetMax = new Vector2 (650 + i * 50 + i * 600, 300);
			go.GetComponent<RectTransform> ().offsetMin = new Vector2 (50 + i * 50 + i * 600, -300);
			go.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
			go.GetComponent<Animy> ().id = i;
			go.GetComponentInChildren<Text>().text = (string)animyData [mmz.categoryString] [i] ["Name"];
			go.GetComponent<Button> ().onClick.AddListener (delegate {
				menuManager.ShowMenu (menu);
				mmz.SetMonsterData (go.GetComponent<Animy>().id);
			});
		}
	}

	public void ReadJsonFile () {
		kategorieSammlung.categories.Clear ();
		jsonString = File.ReadAllText (Application.dataPath + "/Scripts/Json/monsterJson.json");
		monsterData = JsonMapper.ToObject (jsonString);
		Debug.Log (monsterData[0][0][1][0][5][0]["Name"]);
		for (int a = 0; a < monsterData[0].Count; a++) {
			
			List <Monster> monsterlist = new List<Monster> ();
			for (int b = 0; b < monsterData[0][a][1].Count; b++) {

				List <AttackMonster> attackmonsterlist = new List<AttackMonster> ();
				for (int c = 0; c < monsterData[0][a][1][b][5].Count; c++) {
					
					attackmonsterlist.Add (new AttackMonster ((string) monsterData [0][a][1][b][5][c]["Name"],
						(int) monsterData [0][a][1][b][5][c]["Damage"],
						(int) monsterData [0][a][1][b][5][c]["Activated"],
						(int) monsterData [0][a][1][b][5][c]["CooldownAttacks"]));
				}
				monsterlist.Add (new Monster ((int) monsterData [0] [a] [1] [b] ["ID"],
					(string) monsterData [0] [a] [1] [b] ["Name"],
					(int) monsterData [0] [a] [1] [b] ["Health"],
					(int) monsterData [0] [a] [1] [b] ["Level"],
					(int) monsterData [0] [a] [1] [b] ["XP"],
					attackmonsterlist));
			}
			kategorieSammlung.categories.Add (new Category ((string)monsterData[0][a]["Name"], monsterlist));
		}
	}

	public void WriteJsonFile () {
		string jsonString = "{";
		for (int a = 0; a < monsterData.Count - 1; a++) {
			jsonString += "\"" + kategorieSammlung.categories [0];
			for (int b = 0; b < monsterData[a].Count - 1; b++) {
				
				for (int c = 0; c < monsterData[a][b][5].Count - 1; c++) {
					
					for (int d = 0; d < monsterData [a][b][5][c].Count - 1; d++) {
						
					}
				}

			}
		}
		jsonString += "}";
		File.WriteAllText (Application.dataPath + "/Scripts/Json/monsterJsonTest.json", jsonString);
	}
}
