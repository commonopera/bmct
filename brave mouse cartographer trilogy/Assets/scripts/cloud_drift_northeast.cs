using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud_drift_northeast : MonoBehaviour {

	//all clouds are drawn by a single great source of energy residing in the northeast
	//a young boy from the solemn desert magnetically bound to the empire state

	Rigidbody2D rb2d;
	public float speed;

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.AddForce(new Vector2(1, 1) * speed);
	}
}
