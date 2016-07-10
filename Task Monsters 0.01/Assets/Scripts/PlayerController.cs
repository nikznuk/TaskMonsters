using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public Camera cam;
	public float speed;
	public Transform parent;
	private Rigidbody2D rb; 
	public Animator _animator;

	public int direction
	{
		get { return _animator.GetInteger ("Direction"); }
		set { _animator.SetInteger ("Direction", value); }
	}

	void Start (){
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		if (cam.enabled) {
			float inputV = Input.GetAxis ("Vertical");
			float inputH = Input.GetAxis ("Horizontal");

			if (inputH >= 0 && inputV == 0) 
				direction = 3;
			if (inputH <= 0 && inputV == 0) 
				direction = 4;
			if (inputH == 0 && inputV >= 0) 
				direction = 2;
			if (inputH == 0 && inputV <= 0) 
				direction = 1;
			if (inputH == 0 && inputV == 0) 
				direction = 0;
			switch(direction)
			{
			case 1:
				rb.AddForce (gameObject.transform.up * -1 * speed);
				break;
			case 2:
				rb.AddForce (gameObject.transform.up * speed);
				break;
			case 3:
				rb.AddForce (gameObject.transform.right * speed);
				break;
			case 4:
				rb.AddForce (gameObject.transform.right * -1 * speed);
				break;
			}
		}
	}
}
