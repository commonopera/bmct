using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {


	public void StartBMC1(){
		SceneManager.LoadScene(1);
	}

	public void StartBMC2(){
		SceneManager.LoadScene(2);
	}

	public void StartBMC3(){
		SceneManager.LoadScene(3);
	}
}
