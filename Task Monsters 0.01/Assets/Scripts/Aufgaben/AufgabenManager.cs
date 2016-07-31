using UnityEngine;
using System.Collections;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class AufgabenManager : MonoBehaviour {

	public string aufgabeEingabe;
	public int erledigtEingabe;
	public string bisWannEingabe;
	public string wiederholenEingabe;
	public int xpEingabe;
	public string kategorieEingabe;

	string jsonString;
	JsonData aufgabenData;
	AufgabenSammlung aufgabenSammlung;

	public void Start () {
		aufgabenSammlung = new AufgabenSammlung ();
		ReadJsonFile ();
		WriteJsonFile ();
	}

	void Update () {Debug.Log (xpEingabe);}

	public void SetAufgabe (string aufgabe) {aufgabeEingabe = aufgabe;}
	public void SetErledigt (int erledigt) {erledigtEingabe = erledigt;}
	public void SetBisWann (string bisWann) {bisWannEingabe = bisWann;}
	public void SetWiederholen (int wiederholenInt) {
		switch (wiederholenInt) {
		case 0:
			wiederholenEingabe = "Nie";
			break;
		case 1:
			wiederholenEingabe = "Wöchentlich";
			break;
		case 2:
			wiederholenEingabe = "Monatlich";
			break;
		default:
			break;
		}
	}
	public void SetXP (int xp) {xpEingabe = xp;}
	public void SetKategorie (string kategorie) {kategorieEingabe = kategorie;}

	public void AddAufgabe () {
		aufgabenSammlung.aufgaben.Add (new Aufgabe (aufgabeEingabe, erledigtEingabe, bisWannEingabe, wiederholenEingabe, xpEingabe, kategorieEingabe));
		WriteJsonFile ();
	}

	public void ReadJsonFile () {
		aufgabenSammlung.aufgaben.Clear();
		jsonString = File.ReadAllText (Application.dataPath + "/Scripts/Json/aufgaben.json");
		aufgabenData = JsonMapper.ToObject (jsonString);
		for (int i = 0; i < aufgabenData[0].Count; i++) {
			aufgabenSammlung.aufgaben.Add (new Aufgabe ((string)aufgabenData [0] [i] ["Aufgabe"],
				(int)	aufgabenData [0] [i] ["Erledigt"],
				(string)aufgabenData [0] [i] ["BisWann"],
				(string)aufgabenData [0] [i] ["Wiederholen"],
				(int)	aufgabenData [0] [i] ["XP"],
				(string)aufgabenData [0] [i] ["Kategorie"]));
		}
	}

	public void WriteJsonFile () {
		string jsonString = "{\"aufgaben\":[";
		for (int i = 0; i < aufgabenSammlung.aufgaben.Count; i++) {
			if (i + 1 == aufgabenSammlung.aufgaben.Count) {
				jsonString += "{";
				jsonString += "\"Aufgabe\": \"" + aufgabenSammlung.aufgaben [i].aufgabe + "\",";
				jsonString += "\"Erledigt\": " + aufgabenSammlung.aufgaben [i].erledigt + ",";
				jsonString += "\"BisWann\": \"" + aufgabenSammlung.aufgaben [i].bisWann + "\",";
				jsonString += "\"Wiederholen\": \"" + aufgabenSammlung.aufgaben [i].wiederholen + "\",";
				jsonString += "\"XP\": " + aufgabenSammlung.aufgaben [i].xp + ",";
				jsonString += "\"Kategorie\": \"" + aufgabenSammlung.aufgaben [i].category + "\"";
				jsonString += "}";
			} else {
				jsonString += "{";
				jsonString += "\"Aufgabe\": \"" + aufgabenSammlung.aufgaben [i].aufgabe + "\",";
				jsonString += "\"Erledigt\": " + aufgabenSammlung.aufgaben [i].erledigt + ",";
				jsonString += "\"BisWann\": \"" + aufgabenSammlung.aufgaben [i].bisWann + "\",";
				jsonString += "\"Wiederholen\": \"" + aufgabenSammlung.aufgaben [i].wiederholen + "\",";
				jsonString += "\"XP\": " + aufgabenSammlung.aufgaben [i].xp + ",";
				jsonString += "\"Kategorie\": \"" + aufgabenSammlung.aufgaben [i].category + "\"";
				jsonString += "},";
			}
		}
		jsonString += "]}";
		Debug.Log (jsonString);
		File.WriteAllText (Application.dataPath + "/Scripts/Json/aufgaben.json", jsonString);
	}
}