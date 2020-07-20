using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wait_then_destroy : MonoBehaviour {

	//WAITS SPECIFIED LENGTH THEN DESTROYS

	public float wait_time;

	void Start () {
		StartCoroutine(Activate());
	}

	IEnumerator Activate(){
		yield return new WaitForSeconds(wait_time);
		Destroy(gameObject);
	}
}
