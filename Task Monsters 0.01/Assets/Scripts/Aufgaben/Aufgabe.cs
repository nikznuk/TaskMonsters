using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Aufgabe {

	public string aufgabe { get; set; }
	public int erledigt;
	public string bisWann;
	public string wiederholen;
	public int xp;
	public string category;

	public Aufgabe () {}

	public Aufgabe (string aufgabe, int erledigt, string bisWann, string wiederholen, int xp, string category) {
		this.aufgabe = aufgabe;
		this.erledigt = erledigt;
		this.bisWann = bisWann;
		this.wiederholen = wiederholen;
		this.xp = xp;
		this.category = category;
	}
}

public class AufgabenSammlung {
	public List <Aufgabe> aufgaben = new List<Aufgabe>();

	public AufgabenSammlung () {}

	public AufgabenSammlung (List<Aufgabe> aufgaben) {
		this.aufgaben = aufgaben;
	}
}