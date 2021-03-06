﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class MenuMonsterZweig : MonoBehaviour {

	public List<Button> categoryButtons = new List<Button>();
	public Text monsterName;
	public Text xpLabel;
	public Text levelLabel;
	public Text attackenName1;
	public Text attackenName2;
	public Text attackenName3;
	public Text attackenName4;
	int monsterNummer;
	public string categoryString = "";

	List <int> activeMonster = new List<int>(){0, 0, 0, 0, 0};
	int categoryInt = 0;

	KategorieSammlung kategorieSammlungMonster;

	private string jsonString;
	JsonData monsterData;
	private JsonData animyData;

	public Menu menu;
	public MenuMonsterZweig mmz;
	public Transform pref;
	public MenuManager menuManager;
	public Transform parentPanel;

	void Start () {
		kategorieSammlungMonster = new KategorieSammlung ();
		GetAktiveMonster ();
		ReadJsonFile ();
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

		Debug.Log (monsterNummer + "ohne");
		monsterName.text = kategorieSammlungMonster.categories [categoryInt].monsters [monsterNummer].name;
		xpLabel.text = "XP: " + kategorieSammlungMonster.categories [categoryInt].monsters [monsterNummer].xp;
		levelLabel.text = "Level: " + kategorieSammlungMonster.categories [categoryInt].monsters [monsterNummer].level;
		attackenName1.text = kategorieSammlungMonster.categories [categoryInt].monsters [monsterNummer].attack [0].name;
		attackenName2.text = kategorieSammlungMonster.categories [categoryInt].monsters [monsterNummer].attack [1].name;
		attackenName3.text = kategorieSammlungMonster.categories [categoryInt].monsters [monsterNummer].attack [2].name;
		attackenName4.text = kategorieSammlungMonster.categories [categoryInt].monsters [monsterNummer].attack [3].name;
	}

	public void SetMonsterData (int monsterNummer) {
		this.monsterNummer = monsterNummer;
		jsonString = File.ReadAllText (Application.dataPath + "/Scripts/Json/monsterJson.json");
		animyData = JsonMapper.ToObject (jsonString);

		Debug.Log (monsterNummer + "mit");
		monsterName.text = kategorieSammlungMonster.categories [categoryInt].monsters [monsterNummer].name;
		xpLabel.text = "XP: " + kategorieSammlungMonster.categories [categoryInt].monsters [monsterNummer].xp;
		levelLabel.text = "Level: " + kategorieSammlungMonster.categories [categoryInt].monsters [monsterNummer].level;
		attackenName1.text = kategorieSammlungMonster.categories [categoryInt].monsters [monsterNummer].attack [0].name;
		attackenName2.text = kategorieSammlungMonster.categories [categoryInt].monsters [monsterNummer].attack [1].name;
		attackenName3.text = kategorieSammlungMonster.categories [categoryInt].monsters [monsterNummer].attack [2].name;
		attackenName4.text = kategorieSammlungMonster.categories [categoryInt].monsters [monsterNummer].attack [3].name;
	}

	public void AktivesMonsterAufleveln (int category, int xp) {
		switch (category) {
		case 0:
			kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Stärke")].xp += xp;
			if (kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Stärke")].xp > 500) {
				kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Stärke")].xp -= 500;
				kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Stärke")].level += 1;
			}
			xpLabel.text = kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Stärke")].xp.ToString();
			break;
		case 1:			
			kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Gesundheit")].xp += xp;
			if (kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Stärke")].xp > 500) {
				kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Stärke")].xp -= 500;
				kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Stärke")].level += 1;
			}
			xpLabel.text = kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Gesundheit")].xp.ToString();
			break;
		case 2:
			kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Intelligenz")].xp += xp;
			if (kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Stärke")].xp > 500) {
				kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Stärke")].xp -= 500;
				kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Stärke")].level += 1;
			}
			xpLabel.text = (string)kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Intelligenz")].xp.ToString();
			break;
		case 3:
			kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Charisma")].xp += xp;
			if (kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Stärke")].xp > 500) {
				kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Stärke")].xp -= 500;
				kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Stärke")].level += 1;
			}
			xpLabel.text = (string)kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Charisma")].xp.ToString();
			break;
		case 4:
			kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Willenskraft")].xp += xp;
			if (kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Stärke")].xp > 500) {
				kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Stärke")].xp -= 500;
				kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Stärke")].level += 1;
			}	
			xpLabel.text = (string)kategorieSammlungMonster.categories [category].monsters [PlayerPrefs.GetInt ("Aktive Monster Willenskraft")].xp.ToString();
			break;
		}
		WriteJsonFile ();
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
		int animyAnzahl = kategorieSammlungMonster.categories[categoryInt].monsters.Count;

		parentPanel.GetComponent<RectTransform> ().offsetMax = new Vector2 ((animyAnzahl + 1) * 25 + animyAnzahl * 300 ,0);
		parentPanel.GetComponent<RectTransform> ().offsetMin = new Vector2 ((animyAnzahl + 1) * -25 - animyAnzahl * 300 ,0);

		for (int i = 0; i < kategorieSammlungMonster.categories[categoryInt].monsters.Count; i++) {
			Transform go = Instantiate (pref);
			go.transform.parent = parentPanel;
			go.GetComponent<RectTransform> ().offsetMax = new Vector2 (650 + i * 50 + i * 600, 300);
			go.GetComponent<RectTransform> ().offsetMin = new Vector2 (50 + i * 50 + i * 600, -300);
			go.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
			go.GetComponent<Monster> ().id = i;
			go.GetComponentInChildren<Text> ().text = kategorieSammlungMonster.categories [categoryInt].monsters [i].name;
			go.GetComponent<Button> ().onClick.AddListener (delegate {
				menuManager.ShowMenu (menu);
				mmz.SetMonsterData (go.GetComponent<Monster>().id);
			});
		}
	}

	public void ReadJsonFile () {
		kategorieSammlungMonster.categories.Clear ();
		jsonString = File.ReadAllText (Application.dataPath + "/Scripts/Json/monsterJson.json");
		monsterData = JsonMapper.ToObject (jsonString);
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
			kategorieSammlungMonster.categories.Add (new Category ((string)monsterData[0][a]["Name"], monsterlist));
		}
	}

	public void WriteJsonFile () {
		string jsonString = "{\n\t\"Kategorien\": [";
		for (int a = 0; a < kategorieSammlungMonster.categories.Count; a++) {
			jsonString += "\n\t\t{";
			jsonString += "\n\t\t\t\"Name\": \"" + kategorieSammlungMonster.categories[a].name + "\",";
			jsonString += "\n\t\t\t\"Monster\": [";

			for (int b = 0; b < kategorieSammlungMonster.categories[a].monsters.Count; b++) {
				jsonString += "\n\t\t\t\t{";
				jsonString += "\n\t\t\t\t\t\"ID\": " + kategorieSammlungMonster.categories [a].monsters [b].id + ",";
				jsonString += "\n\t\t\t\t\t\"Name\": \"" + kategorieSammlungMonster.categories [a].monsters [b].name + "\",";
				jsonString += "\n\t\t\t\t\t\"Health\": " + kategorieSammlungMonster.categories [a].monsters [b].health + ",";
				jsonString += "\n\t\t\t\t\t\"Level\": " + kategorieSammlungMonster.categories [a].monsters [b].level + ",";
				jsonString += "\n\t\t\t\t\t\"XP\": " + kategorieSammlungMonster.categories [a].monsters [b].xp + ",";
				jsonString += "\n\t\t\t\t\t\"Attacks\": [";

				for (int c = 0; c < kategorieSammlungMonster.categories[a].monsters[b].attack.Count; c++) {
					jsonString += "\n\t\t\t\t\t\t{";
					jsonString += "\n\t\t\t\t\t\t\t\"Name\": \"" + kategorieSammlungMonster.categories[a].monsters[b].attack[c].name + "\",";
					jsonString += "\n\t\t\t\t\t\t\t\"Damage\": " + kategorieSammlungMonster.categories[a].monsters[b].attack[c].damage + ",";
					jsonString += "\n\t\t\t\t\t\t\t\"Activated\": " + kategorieSammlungMonster.categories[a].monsters[b].attack[c].activated + ",";
					jsonString += "\n\t\t\t\t\t\t\t\"CooldownAttacks\": " + kategorieSammlungMonster.categories[a].monsters[b].attack[c].cooldownAttacks;
					jsonString += "\n\t\t\t\t\t\t}";

					if (c != kategorieSammlungMonster.categories[a].monsters[b].attack.Count - 1) {
						jsonString += ",";
					}
				}

				jsonString += "\n\t\t\t\t\t]";
				jsonString += "\n\t\t\t\t}";

				if (b != kategorieSammlungMonster.categories[a].monsters.Count - 1) {
					jsonString += ",";
				}
			}

			jsonString += "\n\t\t\t]";
			jsonString += "\n\t\t}";
			if (a != kategorieSammlungMonster.categories.Count - 1) {
				jsonString += ",";
			}
		}
		jsonString += "\n\t]\n}";
		File.WriteAllText (Application.dataPath + "/Scripts/Json/monsterJson.json", jsonString);
	}
}
