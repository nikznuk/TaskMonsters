using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour 
{
	public Menu currentMenu;
	public Menu first_Menu;
	public List<Camera> cameraList = new List<Camera>();

	void Start () 
	{
		ShowMenu (first_Menu);
	}

	public void ShowMenu(Menu menu)
	{
		if (currentMenu != null)
			currentMenu.IsOpen = false;

		currentMenu = menu;
		currentMenu.IsOpen = true;
	}

	public void ChangeCamera (int cameraIndex)
	{
		foreach (Camera cam in cameraList)
		{
			cam.enabled = false;
		}
		cameraList[cameraIndex].enabled = true;
	}
}
