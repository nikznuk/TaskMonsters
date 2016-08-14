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

	MonsterSammlung monsterSammlung;

	private string jsonString;
	JsonData monsterData;
	private JsonData animyData;

	public Menu menu;
	public MenuMonsterZweig mmz;
	public Transform pref;
	public MenuManager menuManager;
	public Transform parentPanel;

	void Start () {
		monsterSammlung = new MonsterSammlung ();
		GetAktiveMonster ();
		ReadJsonFile ();
		Debug.Log (monsterSammlung.categories[0].monsters.Count);
		WriteJsonFile ();
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
		monsterSammlung.categories.Clear ();
		jsonString = File.ReadAllText (Application.dataPath + "/Scripts/Json/monsterJson.json");
		monsterData = JsonMapper.ToObject (jsonString);
		for (int a = 0; a < monsterData.Count - 1; a++) {
			Debug.Log (monsterData[0][0][0]);
			monsterSammlung.categories [a].name = (string)monsterData [a];
			monsterSammlung.categories.Add (new Category ());

			for (int b = 0; b < monsterData[a].Count - 1; b++) {
				monsterSammlung.categories [a].monsters.Add (new Monster());

				List <AttackMonster> attacks = new List<AttackMonster> ();
				for (int c = 0; c < monsterData[a][b][5].Count - 1; c++) {
					attacks.Add (new AttackMonster());
					for (int d = 0; d < monsterData [a][b][5][c].Count - 1; d++) {
						attacks [c].name = (string)monsterData [a][b][5][c]["Name"];
						attacks [c].damage = (int)monsterData [a][b][5][c]["Damage"];
						attacks [c].activated = (int)monsterData [a][b][5][c]["Activated"];
					}
				}

				monsterSammlung.categories [a].monsters [b].id = (int)monsterData [a][b]["ID"];
				monsterSammlung.categories [a].monsters [b].name = (string)monsterData [a][b]["Name"];
				monsterSammlung.categories [a].monsters [b].health = (int)monsterData [a][b]["Health"];
				monsterSammlung.categories [a].monsters [b].level = (int)monsterData [a][b]["Level"];
				monsterSammlung.categories [a].monsters [b].xp = (int)monsterData [a][b]["XP"];
				monsterSammlung.categories [a].monsters [b].attack = attacks;
			}
		}
	}

	public void WriteJsonFile () {
		string jsonString = "{";
		for (int a = 0; a < monsterData.Count - 1; a++) {
			jsonString += "\"" + monsterSammlung.categories [0];
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
