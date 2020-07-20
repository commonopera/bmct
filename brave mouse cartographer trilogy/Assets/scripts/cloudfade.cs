using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudfade : MonoBehaviour {

	//things fade and disappear after waiting for a while
	//checking 'instructions' makes it start from opaque, otherwise it begins at 70%

	SpriteRenderer Render;
	public int waittime;
	public bool instructions, blacksquare;

	void Start () {
		Render = GetComponent<SpriteRenderer>();
		StartCoroutine(SlowFade());
	}

	IEnumerator SlowFade(){
		if(instructions){
			Render.color = new Color(1f,1f,1f,.1f);
			yield return new WaitForSeconds(.2f);
			Render.color = new Color(1f,1f,1f,.3f);
			yield return new WaitForSeconds(.2f);
			Render.color = new Color(1f,1f,1f,.5f);
			yield return new WaitForSeconds(.2f);
			Render.color = new Color(1f,1f,1f,.7f);
			yield return new WaitForSeconds(.2f);
			Render.color = new Color(1f,1f,1f,.9f);
			yield return new WaitForSeconds(.2f);
			Render.color = new Color(1f,1f,1f,1f);
		}
		yield return new WaitForSeconds(waittime);
		if(instructions || blacksquare){
			Render.color = new Color(1f,1f,1f,.95f);
			yield return new WaitForSeconds(.2f);
			Render.color = new Color(1f,1f,1f,.9f);
			yield return new WaitForSeconds(.2f);
			Render.color = new Color(1f,1f,1f,.85f);
			yield return new WaitForSeconds(.2f);
			Render.color = new Color(1f,1f,1f,.8f);
			yield return new WaitForSeconds(.2f);
			Render.color = new Color(1f,1f,1f,.75f);
			yield return new WaitForSeconds(.2f);
		}
		Render.color = new Color(1f,1f,1f,.7f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,.65f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,.6f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,.55f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,.5f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,.45f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,.4f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,.35f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,.3f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,.25f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,.2f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,.15f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,.1f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,.05f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,.04f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,.03f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,.02f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,.01f);
		yield return new WaitForSeconds(.2f);
		Render.color = new Color(1f,1f,1f,0f);
		Destroy(gameObject);
	}
}
