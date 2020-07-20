using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdstart : MonoBehaviour {

	//initially youd just always start in the same place but now you dont
	//it feels more 'generative' this way i think, but this is a deception

	public bool i, ii, iii;

	void Start () {
		if(i){
			this.transform.position = new Vector3((Random.Range(-10f, 13f)), (Random.Range(-6f, 20f)), -10);
		}
		if(ii){
			this.transform.position = new Vector3((Random.Range(-8f, 8f)), (Random.Range(-3f, 15f)), -10);
		}
		if(iii){
			this.transform.position = new Vector3((Random.Range(-20f, 18f)), (Random.Range(-6f, 16f)), -10);
		}
	}
}
