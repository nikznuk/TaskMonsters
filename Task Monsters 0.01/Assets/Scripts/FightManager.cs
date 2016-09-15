using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

public class FightManager : MonoBehaviour {

	KategorieSammlung kategorieSammlungGegner;
	private string jsonString;
	JsonData monsterData;
	private JsonData animyData;

	void Start () {
		kategorieSammlungGegner = new KategorieSammlung ();
		ReadJsonFile ();
		Debug.Log (kategorieSammlungGegner.categories[0].monsters[0].name);
	}

	public void ReadJsonFile () {
		kategorieSammlungGegner.categories.Clear ();
		jsonString = File.ReadAllText (Application.dataPath + "/Scripts/Json/gegnerJson.json");
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
			kategorieSammlungGegner.categories.Add (new Category ((string)monsterData[0][a]["Name"], monsterlist));
		}
	}
}