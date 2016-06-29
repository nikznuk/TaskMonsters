using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelDesigner))]
public class LevelDesignerEditor : Editor
{
	
	LevelDesigner script;
	BatchMode batchmode = BatchMode.None;
	bool leftControl = false; 
	Vector3 oldTilePos = new Vector3(); 
	
	enum BatchMode
	{
		Create,
		Delete,
		None
	}
	
	void OnEnable()
	{
		script = (LevelDesigner) target;
		
		if (!Application.isPlaying) 
		{
			Tools.current = Tool.View;
		}
	} 
	
	public override void OnInspectorGUI()
	{
		EditorGUILayout.BeginVertical ();
		EditorGUILayout.PrefixLabel ("Tile");
		script.prefab = (GameObject) EditorGUILayout.ObjectField (script.prefab,typeof(GameObject), false);
		EditorGUILayout.EndVertical ();
		
		EditorGUILayout.BeginVertical ();
		EditorGUILayout.PrefixLabel ("Depth");
		script.depth = EditorGUILayout.Slider (script.depth,-5,5);
		EditorGUILayout.EndVertical ();
		
		if (GUI.changed)
			EditorUtility.SetDirty(target); 
	}
	
	void OnSceneGUI()
	{
		Ray ray = HandleUtility.GUIPointToWorldRay (Event.current.mousePosition);
		Vector3 tilePos = new Vector3 ();
		tilePos.x = Mathf.RoundToInt (ray.origin.x); 
		tilePos.z = Mathf.RoundToInt (ray.origin.z);  	
		
		if (tilePos != oldTilePos) 
		{
			script.gizmoPosition = tilePos;
			SceneView.RepaintAll ();
			oldTilePos = tilePos;
		}
		
		Event current = Event.current;
		
		if (current.keyCode == KeyCode.LeftControl)
		{
			if(current.type == EventType.keyDown)
			{
				leftControl = true;
			}
			else if(current.type == EventType.keyUp)
			{
				leftControl = false;
				batchmode = BatchMode.None;
			}
		}
		
		if (leftControl)
		{
			if (current.type == EventType.mouseDown)
			{
				if(current.button == 0)
				{
					batchmode = BatchMode.Create;
				}
				else if (current.button == 1)
				{
					batchmode = BatchMode.Delete;
				}
			}
		}
		
		if ((current.type == EventType.mouseDown) || (batchmode != BatchMode.None))
		{
			
			string name = string.Format ("Tile{0}_{1}_{2}", script.depth, tilePos.z, tilePos.x);
			
			if ((current.button == 0) || (batchmode == BatchMode.Create))
			{
				CreateTile (tilePos, name);
			}
			if ((current.button == 1) || (batchmode == BatchMode.Delete)) 
			{
				DeleteTile (name);
			}
			
			if (GUI.changed)
				EditorUtility.SetDirty (target);
		}
	}
	void CreateTile(Vector3 tilePos, string name)
	{
		if (!GameObject.Find (name))
		{
			Vector3 pos = new Vector3(tilePos.x,script.depth, tilePos.z);
			GameObject go = (GameObject)Instantiate (script.prefab,pos,Quaternion.identity);
			go.name = name;
		}
	}
	
	void DeleteTile(string name)
	{
		GameObject go = GameObject.Find (name);
		if (go != null) 
			DestroyImmediate (go);
	}
}