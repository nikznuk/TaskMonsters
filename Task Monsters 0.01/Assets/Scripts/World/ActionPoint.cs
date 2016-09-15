using UnityEngine;
using System.Collections;

public class ActionPoint : MonoBehaviour {

	public string monsterCatecory;
	public int monsterID;
	public string tag;

	public MenuManager menuManager;
	public FightManager fightManager;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == tag) {
			menuManager.ChangeCamera (2);
			//fightManager.CreateNewMonster (monsterCatecory, monsterID);
		}
	}
}
