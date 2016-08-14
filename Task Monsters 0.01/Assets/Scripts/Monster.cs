using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour {
	public int id;
	public string name;
	public int health;
	public int level;
	public int xp;
	public List <AttackMonster> attack;

	public Monster () {
	}

	public Monster (string name, int health, List<AttackMonster> attack) {
		this.name = name;
		this.health = health;
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

public class Category {
	public string name;
	public List <Monster> monsters = new List<Monster> ();

	public Category () {}

	public Category (string name, List <Monster> monsters) {}
}

public class MonsterSammlung : MonoBehaviour {
	public List <Category> categories = new List<Category>();

	public MonsterSammlung () {}

	public MonsterSammlung (List <Category> categories) {
		this.categories = categories;
	}
}