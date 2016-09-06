using UnityEngine;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using System;
using UnityEngine.UI;

public class AufgabenManager : MonoBehaviour {

	public string aufgabeEingabe;
	public int erledigtEingabe;
	public string bisWannEingabe, bisWannEingabeTT, bisWannEingabeMM, bisWannEingabeYYYY;
	public string wiederholenEingabe;
	public int xpEingabe;
	public string kategorieEingabe;

	public List <Image> kategorieButtons;
	public Sprite pressedSprite;
	public Sprite defaultSprite;
	public Sprite importantSprite;

	public Transform parentPanel;
	public Transform dataPref;

	public Menu aktuellesMonster;
	public Menu popUpPanel;
	public MenuManager mm;

	public MenuMonsterZweig monsterZweig;

	public List <Color> categorieColor;

	public List <Transform> aufgabenListe;

	string jsonString;
	JsonData aufgabenData;
	AufgabenSammlung aufgabenSammlung;

	DateTime dateTimeAufgabe;

	int idAufgabe;
	string cat;

	public void Start () {
		aufgabenListe = new List<Transform>();
		aufgabenSammlung = new AufgabenSammlung ();
		ReadJsonFile ();
	}

	void Update () {
		for (int i = 0; i < aufgabenSammlung.aufgaben.Count; i++) {
			aufgabenListe [i].GetComponent <Aufgabe> ().erledigt = aufgabenSammlung.aufgaben [i].erledigt;
		}
	}

	public void CheckErledigt (int id) {
		mm.ShowPopUpMenu (popUpPanel);
		idAufgabe = id;
	}

	public void ClosePopUp (int value) {
		mm.ClosePopUpMenu ();
		if (value == 1) {

			//
			// Wenn Aufgabe noch weiter nur mit Haken angezeigt werden soll
			//
//			aufgabenSammlung.aufgaben [idAufgabe].erledigt = 1;
//			aufgabenListe [idAufgabe].GetChild(0).GetChild(0).GetComponent <Image> ().color = new Color (1, 1, 1, 1);

			SendXP ((string)aufgabenSammlung.aufgaben[idAufgabe].category, aufgabenSammlung.aufgaben[idAufgabe].xp);

			aufgabenSammlung.aufgaben.RemoveAt (idAufgabe);
			WriteJsonFile ();
			ReadJsonFile ();
			CreateList ();
			if (cat == "") SetPanelSize ();
			if (cat != "") SetPanelSize (cat);
		}
		if (value == 0) {
			aufgabenSammlung.aufgaben [idAufgabe].erledigt = 0;
			aufgabenListe [idAufgabe].GetChild(0).GetChild(0).GetComponent <Image> ().color = new Color (1, 1, 1, 0);
		}
	}

	public void SetPanelSize () {
		float yLenght = 0;
		cat = "";
		var children = new List<GameObject>();
		foreach (Transform child in parentPanel) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));

		yLenght = aufgabenSammlung.aufgaben.Count * -250;
		parentPanel.GetComponent <RectTransform> ().offsetMax = new Vector2 (0, 0);
		parentPanel.GetComponent <RectTransform> ().offsetMin = new Vector2 (0, yLenght);

		for (int i = 0; i < aufgabenSammlung.aufgaben.Count; i++) {
			Transform go = Instantiate (dataPref);
			go.transform.parent = parentPanel;
			go.GetComponent <RectTransform> ().offsetMax = new Vector2 (0, i * -250);
			go.GetComponent <RectTransform> ().offsetMin = new Vector2 (0, i * -250 - 250);
			go.GetComponent <RectTransform> ().localScale = new Vector3 (1, 1, 1);
			aufgabenListe [i] = go;

			go.GetComponent <Aufgabe> ().id = aufgabenSammlung.aufgaben [i].id;
			go.GetComponent <Aufgabe> ().aufgabe = aufgabenSammlung.aufgaben [i].aufgabe;
			go.GetComponent <Aufgabe> ().xp = aufgabenSammlung.aufgaben [i].xp;
			go.GetComponent <Aufgabe> ().wiederholen = aufgabenSammlung.aufgaben [i].wiederholen;
			go.GetComponent <Aufgabe> ().bisWann = ChangeTimeSting(aufgabenSammlung.aufgaben [i].bisWann);
			go.GetComponent <Aufgabe> ().category = aufgabenSammlung.aufgaben [i].category;
			go.GetComponent <Aufgabe> ().am = this;

			go.GetChild (1).GetComponent <Text> ().text = aufgabenSammlung.aufgaben [i].aufgabe;
			go.GetChild (2).GetComponent <Text> ().text = aufgabenSammlung.aufgaben [i].xp + " XP";
			go.GetChild (3).GetComponent <Text> ().text = ChangeTimeSting(aufgabenSammlung.aufgaben [i].bisWann);
			go.GetChild (0).GetComponent <Image> ().color = categorieColor[ConvertCategoryStringToInt (aufgabenSammlung.aufgaben[i].category)];
			if (CheckZeitabstand (aufgabenSammlung.aufgaben[i].bisWann)) {
				go.GetChild (4).GetComponent <Image> ().sprite = importantSprite;
				go.GetChild (4).GetComponent <Image> ().color = new Color (1, 1, 1, 1);
			}
		}
	}

	public void SetPanelSize (string category) {
		float yLenght = 0;
		cat = category;
		var children = new List<GameObject>();
		foreach (Transform child in parentPanel) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));

		for (int i = 0; i < aufgabenSammlung.aufgaben.Count; i++) {
			if (aufgabenSammlung.aufgaben [i].category == category) {yLenght++;}
		}

		yLenght = yLenght * -250;
		parentPanel.GetComponent <RectTransform> ().offsetMax = new Vector2 (0, 0);
		parentPanel.GetComponent <RectTransform> ().offsetMin = new Vector2 (0, yLenght);
		yLenght = 0;
		for (int i = 0; i < aufgabenSammlung.aufgaben.Count; i++) {
			if (aufgabenSammlung.aufgaben [i].category == category) {
				Transform go = Instantiate (dataPref);
				go.transform.parent = parentPanel;
				go.GetComponent <RectTransform> ().offsetMax = new Vector2 (0, yLenght * -250);
				go.GetComponent <RectTransform> ().offsetMin = new Vector2 (0, yLenght * -250 - 250);
				go.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
				aufgabenListe [i] = go;

				go.GetComponent <Aufgabe> ().id = aufgabenSammlung.aufgaben [i].id;
				go.GetComponent <Aufgabe> ().aufgabe = aufgabenSammlung.aufgaben [i].aufgabe;
				go.GetComponent <Aufgabe> ().xp = aufgabenSammlung.aufgaben [i].xp;
				go.GetComponent <Aufgabe> ().wiederholen = aufgabenSammlung.aufgaben [i].wiederholen;
				go.GetComponent <Aufgabe> ().bisWann = ChangeTimeSting(aufgabenSammlung.aufgaben [i].bisWann);
				go.GetComponent <Aufgabe> ().category = aufgabenSammlung.aufgaben [i].category;
				go.GetComponent <Aufgabe> ().am = this;

				go.GetChild (1).GetComponent <Text> ().text = aufgabenSammlung.aufgaben [i].aufgabe;
				go.GetChild (2).GetComponent <Text> ().text = aufgabenSammlung.aufgaben [i].xp + " XP";
				go.GetChild (3).GetComponent <Text> ().text = ChangeTimeSting(aufgabenSammlung.aufgaben [i].bisWann);
				go.GetChild (0).GetComponent <Image> ().color = categorieColor[ConvertCategoryStringToInt (aufgabenSammlung.aufgaben[i].category)];
				if (CheckZeitabstand (aufgabenSammlung.aufgaben[i].bisWann)) {
					go.GetChild (4).GetComponent <Image> ().sprite = importantSprite;
					go.GetChild (4).GetComponent <Image> ().color = new Color (1, 1, 1, 1);
				}
				yLenght++;
			}
		}
	}

	public string ChangeTimeSting (string time) {
		DateTime dt = Convert.ToDateTime (time);
		string outtime = dt.ToString ("dd-MM-yyyy");
		return outtime;
	}

	public bool CheckZeitabstand (string date) {
		dateTimeAufgabe = Convert.ToDateTime (date);
		DateTime today = System.DateTime.Now ;
		System.TimeSpan elapsed = dateTimeAufgabe.Subtract(today) ;
		double days = elapsed.TotalDays + 1;
		if (days < 3) {return true;} 
		else {return false;}
	}

	public int ConvertCategoryStringToInt (string category) {
		switch (category) {
		case "Stärke":
			return 0;
			break;
		case "Gesundheit":
			return 1;
			break;
		case "Intelligenz":
			return 2;
			break;
		case "Charisma":
			return 3;
			break;
		case "Willenskraft":
			return 4;
			break;
		default:
			return 0;
			break;
		}
	}

	public void SetAufgabe (string aufgabe) {aufgabeEingabe = aufgabe;}
	public void SetErledigt (int erledigt) {erledigtEingabe = erledigt;}
	public void SetBisWannTT (string bisWann) {bisWannEingabeTT = bisWann;}
	public void SetBisWannMM (string bisWann) {bisWannEingabeMM = bisWann;}
	public void SetBisWannYYYY (string bisWann) {bisWannEingabeYYYY = bisWann;}
	public void SetBisWann () {
		bisWannEingabe = bisWannEingabeMM + "-" + bisWannEingabeTT + "-" + bisWannEingabeYYYY;
	}
	public void SetWiederholen (int wiederholenInt) {
		switch (wiederholenInt) {
		case 0:
			wiederholenEingabe = "Nie";
			break;
		case 1:
			wiederholenEingabe = "Täglich";
			break;
		case 2:
			wiederholenEingabe = "Wöchentlich";
			break;
		default:
			break;
		}
	}
	public void SetXP (int xp) {xpEingabe = xp;}
	public void SetKategorie (string kategorie) {kategorieEingabe = kategorie;}

	public void AddAufgabe () {
		SetBisWann ();
		aufgabenSammlung.aufgaben.Add (new Aufgabe (aufgabenSammlung.aufgaben.Count, aufgabeEingabe, erledigtEingabe, bisWannEingabe, wiederholenEingabe, xpEingabe, kategorieEingabe));
		WriteJsonFile ();
	}

	public void SelectCategory (int index) {
		for (int i = 0; i < 5; i++) {
			kategorieButtons [i].sprite = defaultSprite;
		}
		switch (index) {
		case 0: 
			kategorieEingabe = "Stärke";
			kategorieButtons [0].sprite = pressedSprite;
			break;
		case 1: 
			kategorieEingabe = "Gesundheit";
			kategorieButtons [1].sprite = pressedSprite;
			break;
		case 2: 
			kategorieEingabe = "Intelligenz";
			kategorieButtons [2].sprite = pressedSprite;
			break;
		case 3: 
			kategorieEingabe = "Charisma";
			kategorieButtons [3].sprite = pressedSprite;
			break;
		case 4: 
			kategorieEingabe = "Willenskraft";
			kategorieButtons [4].sprite = pressedSprite;
			break;
		default:
			break;
		}
	}

	public void ReadJsonFile () {
		aufgabenSammlung.aufgaben.Clear();
		jsonString = File.ReadAllText (Application.dataPath + "/Scripts/Json/aufgaben.json");
		aufgabenData = JsonMapper.ToObject (jsonString);
		for (int i = 0; i < aufgabenData[0].Count; i++) {
			aufgabenSammlung.aufgaben.Add (new Aufgabe ((int)aufgabenData [0] [i] ["ID"], 
				(string)aufgabenData [0] [i] ["Aufgabe"],
				(int)	aufgabenData [0] [i] ["Erledigt"],
				(string)aufgabenData [0] [i] ["BisWann"],
				(string)aufgabenData [0] [i] ["Wiederholen"],
				(int)	aufgabenData [0] [i] ["XP"],
				(string)aufgabenData [0] [i] ["Kategorie"]));
		}
		CreateList ();
	}

	public void WriteJsonFile () {
		string jsonString = "{\"aufgaben\": [";
		for (int i = 0; i < aufgabenSammlung.aufgaben.Count; i++) {
			if (i + 1 == aufgabenSammlung.aufgaben.Count) {
				jsonString += "\t\t{";
				jsonString += "\"ID\": " + i + ",";
				jsonString += "\"Aufgabe\": \"" + aufgabenSammlung.aufgaben [i].aufgabe + "\",";
				jsonString += "\"Erledigt\": " + aufgabenSammlung.aufgaben [i].erledigt + ",";
				jsonString += "\"BisWann\": \"" + aufgabenSammlung.aufgaben [i].bisWann + "\",";
				jsonString += "\"Wiederholen\": \"" + aufgabenSammlung.aufgaben [i].wiederholen + "\",";
				jsonString += "\"XP\": " + aufgabenSammlung.aufgaben [i].xp + ",";
				jsonString += "\"Kategorie\": \"" + aufgabenSammlung.aufgaben [i].category + "\"";
				jsonString += "}";
			} else {
				jsonString += "\t{";
				jsonString += "\"ID\": " + i + ",";
				jsonString += "\"Aufgabe\": \"" + aufgabenSammlung.aufgaben [i].aufgabe + "\",";
				jsonString += "\"Erledigt\": " + aufgabenSammlung.aufgaben [i].erledigt + ",";
				jsonString += "\"BisWann\": \"" + aufgabenSammlung.aufgaben [i].bisWann + "\",";
				jsonString += "\"Wiederholen\": \"" + aufgabenSammlung.aufgaben [i].wiederholen + "\",";
				jsonString += "\"XP\": " + aufgabenSammlung.aufgaben [i].xp + ",";
				jsonString += "\"Kategorie\": \"" + aufgabenSammlung.aufgaben [i].category + "\"";
				jsonString += "},\n";
			}
		}
		jsonString += "]}";
		File.WriteAllText (Application.dataPath + "/Scripts/Json/aufgaben.json", jsonString);
	}

	public void CreateList () {
		aufgabenListe.Clear ();
		for (int i = 0; i < aufgabenSammlung.aufgaben.Count; i++) {
			aufgabenListe.Add (null);
		}
	}

	public void SendXP (string category, int xp) {
		mm.ShowMenu (aktuellesMonster);
		monsterZweig.AktivesMonsterAufleveln (ConvertCategoryStringToInt(category), xp);
	}
}