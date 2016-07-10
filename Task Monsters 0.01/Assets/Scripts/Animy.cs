using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class Animy : MonoBehaviour {
	public string name;
	public int health;
	public Attack[] attack;

	public Animy () {
	}

	public Animy (string name, int health, Attack[] attack) {
		this.name = name;
		this.health = health;
		this.attack = attack;
	}
}

public class Attack {
	public string name;
	public int damage;
	public bool activated;

	public Attack (){
	}

	public Attack (string name, int damage, bool activated) {
		this.name = name;
		this.damage = damage;
		this.activated = activated;
	}
}