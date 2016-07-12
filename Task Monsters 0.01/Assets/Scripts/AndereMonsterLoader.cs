using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class AndereMonsterLoader : MonoBehaviour {

	private string jsonString;
	private JsonData animyData;

	public Transform parentPanel;
	public Transform pref;
	public MenuManager menuManager;
	public Menu menu;
	public MenuMonsterZweig mmz;

	public void CalculatePanelSize () {
		
		jsonString = File.ReadAllText (Application.dataPath + "/Scripts/Json/animyJson.json");
		animyData = JsonMapper.ToObject (jsonString);

		int animyAnzahl = animyData[mmz.categoryString].Count;

		parentPanel.GetComponent<RectTransform> ().offsetMax = new Vector2 ((animyAnzahl + 1) * 25 + animyAnzahl * 300 ,0);
		parentPanel.GetComponent<RectTransform> ().offsetMin = new Vector2 ((animyAnzahl + 1) * -25 - animyAnzahl * 300 ,0);

		for (int i = 0; i < animyData [mmz.categoryString].Count; i++) {
			Transform go = Instantiate (pref);
			go.transform.parent = parentPanel;
			go.GetComponent<RectTransform> ().offsetMax = new Vector2 (650 + i * 50 + i * 600, 300);
			go.GetComponent<RectTransform> ().offsetMin = new Vector2 (50 + i * 50 + i * 600, -300);
			go.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
			go.GetComponent<Animy> ().id = i;
			go.GetComponentInChildren<Text>().text = (string)animyData [mmz.categoryString] [i] ["Name"];
			go.GetComponent<Button> ().onClick.AddListener (delegate {
				menuManager.ShowMenu (menu);
				mmz.SetMonsterData (go.GetComponent<Animy>().id);
			});
		}
	}
}
