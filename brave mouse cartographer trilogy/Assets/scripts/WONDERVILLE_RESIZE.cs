using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WONDERVILLE_RESIZE : MonoBehaviour {

	//resizes screen to fit on the screen table outside at wonderville
	//thank you KRL for the opportunity to need a script like this
	//DEACTIVATE THIS SCRIPT BEFORE MAKING BUILDS FOR DESKTOPS

	void Start () {
		Screen.SetResolution(2560, 800, false);
	}
}
