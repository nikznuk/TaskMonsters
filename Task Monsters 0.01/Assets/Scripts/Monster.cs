using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KategorieSammlung : MonoBehaviour{
	public List <Category> categories = new List<Category>();

	public KategorieSammlung () {}

	public KategorieSammlung (List <Category> categories) {
		this.categories = categories;
	}
}

public class Category {
	public string name;
	public List <Monster> monsters = new List<Monster> ();

	public Category () {}

	public Category (string name) {
		this.name = name;
	}

	public Category (string name, List <Monster> monsters) {
		this.name = name;
		this.monsters = monsters;
	}
}

public class Monster : MonoBehaviour {
	public int id;
	public string name;
	public int health;
	public int level;
	public int xp;
	public List <AttackMonster> attack;

	public Monster () {
	}

	public Monster (int id, string name, int health, int level, int xp, List<AttackMonster> attack) {
		this.id = id;
		this.name = name;
		this.health = health;
		this.level = level;
		this.xp = xp;
		this.attack = attack;
	}
}

public class AttackMonster {
	public string name;
	public int damage;
	public int activated;
	public int cooldownAttacks;

	public AttackMonster (){
	}

	public AttackMonster (string name, int damage, int activated, int cooldownAttacks) {
		this.name = name;
		this.damage = damage;
		this.activated = activated;
		this.cooldownAttacks = cooldownAttacks;
	}
}