using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class esc_to_quit : MonoBehaviour {

	//i never include this in anything i make but for this it seemed cruel not to
	//total control is not fun but a total lack of it isnt either

	void Update () {
		if(Input.GetKeyDown("escape")){
			Application.Quit();
		}
	}
}
