using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class XPChange : MonoBehaviour {

	public Text text;
	public AufgabenManager am;

	void Update () {
		text.text = GetComponent<Slider> ().value.ToString();
		int i;
		int.TryParse (text.text, out i);
		am.SetXP (i);
	}

	public void ChangeSlider (string value) {
		int valueI;
		int.TryParse (value, out valueI); 
		GetComponent<Slider> ().value = valueI;
	}
}
