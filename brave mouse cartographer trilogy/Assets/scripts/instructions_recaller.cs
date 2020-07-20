using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instructions_recaller : MonoBehaviour {

	//for people picking it up while its already running

	public string keyname;
	public GameObject instructions;

	void Update () {
		if(Input.GetKeyDown(keyname)){
			Instantiate(instructions, transform.position, Quaternion.identity, transform);
		}
	}
}
