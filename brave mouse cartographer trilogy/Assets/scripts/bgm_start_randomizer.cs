using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgm_start_randomizer : MonoBehaviour {

	//starts the quadrant audios at random points so the overlaps arent always the same

	AudioSource audiosource;

	void Start () {
		audiosource = GetComponent<AudioSource>();
		audiosource.time = Random.Range(0, 10);
		audiosource.Play();
	}
}
