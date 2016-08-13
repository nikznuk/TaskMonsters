using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Aufgabe : MonoBehaviour {

	public int id;
	public string aufgabe; 
	public int erledigt;
	public string bisWann;
	public string wiederholen;
	public int xp;
	public string category;
	public AufgabenManager am;

	public Aufgabe () {}

	public Aufgabe (int id, string aufgabe, int erledigt, string bisWann, string wiederholen, int xp, string category) {
		this.id = id;
		this.aufgabe = aufgabe;
		this.erledigt = erledigt;
		this.bisWann = bisWann;
		this.wiederholen = wiederholen;
		this.xp = xp;
		this.category = category;
	}

	public void CheckErledigt () {
		am.CheckErledigt (id);
	}
}

public class AufgabenSammlung : MonoBehaviour{
	public List <Aufgabe> aufgaben = new List<Aufgabe>();

	public AufgabenSammlung () {}

	public AufgabenSammlung (List<Aufgabe> aufgaben) {
		this.aufgaben = aufgaben;
	}
}