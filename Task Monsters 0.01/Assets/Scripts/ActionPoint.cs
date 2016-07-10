using UnityEngine;
using System.Collections;

public class ActionPoint : MonoBehaviour {

	public string monsterCatecory;
	public int monsterID;
	public string tag;

	public MenuManager mm;
	public GegnerLoader gl;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == tag) {
			mm.ChangeCamera (2);
			gl.CreateNewMonster (monsterCatecory, monsterID);
		}
	}
}
