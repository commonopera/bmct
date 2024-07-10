using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird_flying : MonoBehaviour {

	//randomly select one of eight directions and fly in it for a random length of time
	//upon nearing the edge of the known world, turn around and seek not the void beyond

	bool ready = true;
	int direction, frequency;
	//1=up, 2=upright, 3=right, 4=downright, 5=down, 6=downleft, 7=left, 8=upleft
	public float speed;
	Rigidbody2D rb2d;

    private void Update()
    {
		AchievementManager.TryIncrementStat(AchievementStatID.FLIGHT_TIME, Time.deltaTime);
    }

    void FixedUpdate () {
		if(ready){
			rb2d = GetComponent<Rigidbody2D> ();
			rb2d.AddForce(new Vector2(0, 0) * speed);
			ready = false;
			direction = Random.Range(1, 9);
			StartCoroutine(Repick());
		}
		if(direction == 1){
			rb2d.AddForce(new Vector2(0, 1) * speed);
			PlayerPrefs.SetInt("direction", 1);
		}
		if(direction == 2){
			rb2d.AddForce(new Vector2(1, 1) * speed);
			PlayerPrefs.SetInt("direction", 2);
		}
		if(direction == 3){
			rb2d.AddForce(new Vector2(1, 0) * speed);
			PlayerPrefs.SetInt("direction", 3);
		}
		if(direction == 4){
			rb2d.AddForce(new Vector2(1, -1) * speed);
			PlayerPrefs.SetInt("direction", 4);
		}
		if(direction == 5){
			rb2d.AddForce(new Vector2(0, -1) * speed);
			PlayerPrefs.SetInt("direction", 5);
		}
		if(direction == 6){
			rb2d.AddForce(new Vector2(-1, -1) * speed);
			PlayerPrefs.SetInt("direction", 6);
		}
		if(direction == 7){
			rb2d.AddForce(new Vector2(-1, 0) * speed);
			PlayerPrefs.SetInt("direction", 7);
		}
		if(direction == 8){
			rb2d.AddForce(new Vector2(-1, 1) * speed);
			PlayerPrefs.SetInt("direction", 8);
		}
	}

	IEnumerator Repick(){
		frequency = Random.Range(6, 20);
		yield return new WaitForSeconds(frequency);
		ready = true;
	}

	IEnumerator LongRepick(){
		yield return new WaitForSeconds(15);
		ready = true;
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "leftwall"){
			StopAllCoroutines();
			ready = false;
			direction = 3;
			StartCoroutine(LongRepick());
		}
		if(col.gameObject.tag == "downwall"){
			StopAllCoroutines();
			ready = false;
			direction = 1;
			StartCoroutine(LongRepick());
		}
		if(col.gameObject.tag == "rightwall"){
			StopAllCoroutines();
			ready = false;
			direction = 7;
			StartCoroutine(LongRepick());
		}
		if(col.gameObject.tag == "upwall"){
			StopAllCoroutines();
			ready = false;
			direction = 5;
			StartCoroutine(LongRepick());
		}
	}
}
