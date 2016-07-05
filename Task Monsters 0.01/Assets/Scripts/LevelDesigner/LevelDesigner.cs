using UnityEngine;
using System.Collections;

public class LevelDesigner : MonoBehaviour {

	public GameObject prefab;
	public Vector3 gizmoPosition;
	public float depth = 0;

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(new Vector3(gizmoPosition.x, gizmoPosition.y, depth),new Vector3(1,1,1)); 
	}
}