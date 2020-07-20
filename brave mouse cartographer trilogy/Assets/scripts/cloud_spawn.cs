using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud_spawn : MonoBehaviour {

	//clouds spawn to the southwest of the player so as to drift past you on their route
	//(i am aware i am not a particularly efficient programmer)

	bool ready = true;
	int whichcloud, xory, frequency;
	float xamount, yamount;
	GameObject cloud;
	public GameObject cloud1, cloud2, cloud3, cloud4, cloud5, cloud6, cloud7, cloud8;

	void Update () {
		if(ready){
			ready = false;
			whichcloud = Random.Range(1, 9);
			if(whichcloud == 1){
				cloud = cloud1;
			}
			if(whichcloud == 2){
				cloud = cloud2;
			}
			if(whichcloud == 3){
				cloud = cloud3;
			}
			if(whichcloud == 4){
				cloud = cloud4;
			}
			if(whichcloud == 5){
				cloud = cloud5;
			}
			if(whichcloud == 6){
				cloud = cloud6;
			}
			if(whichcloud == 7){
				cloud = cloud7;
			}
			if(whichcloud == 8){
				cloud = cloud8;
			}
			xory = Random.Range(1, 3);
			if(xory == 1){
				xamount = Random.Range(-1f, 1.5f);
				yamount = 0;
			}
			if(xory == 2){
				xamount = 0;
				yamount = Random.Range(-1f, 1.1f);
			}
			var spawnposition = new Vector3(transform.position.x, transform.position.y, 0) + new Vector3(xamount, yamount, -10);
			Instantiate(cloud, spawnposition, Quaternion.identity);
			StartCoroutine(Doitagain());
		}
	}

	IEnumerator Doitagain(){
		frequency = Random.Range(3, 20);
		yield return new WaitForSeconds(frequency);
		ready = true;
	}
}
