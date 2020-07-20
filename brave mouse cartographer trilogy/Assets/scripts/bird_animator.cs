using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird_animator : MonoBehaviour {

	//the bird faces the direction it is moving as it moves
	//its just one animation rotated in all eight directions, which feels a little like cheating

	Animator animator;

	void Start () {
		animator = GetComponent<Animator>();
	}

	void Update () {
		if(PlayerPrefs.GetInt("direction") == 1){
			animator.SetInteger ("state", 1);
		}
		if(PlayerPrefs.GetInt("direction") == 2){
			animator.SetInteger ("state", 2);
		}
		if(PlayerPrefs.GetInt("direction") == 3){
			animator.SetInteger ("state", 3);
		}
		if(PlayerPrefs.GetInt("direction") == 4){
			animator.SetInteger ("state", 4);
		}
		if(PlayerPrefs.GetInt("direction") == 5){
			animator.SetInteger ("state", 5);
		}
		if(PlayerPrefs.GetInt("direction") == 6){
			animator.SetInteger ("state", 6);
		}
		if(PlayerPrefs.GetInt("direction") == 7){
			animator.SetInteger ("state", 7);
		}
		if(PlayerPrefs.GetInt("direction") == 8){
			animator.SetInteger ("state", 8);
		}
	}
}
