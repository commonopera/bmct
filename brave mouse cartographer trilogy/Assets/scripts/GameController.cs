using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public RectTransform MainPage, ExtrasPage;
	bool Transitioning = false;

	public List<Sprite> BookletPages;
	public Image BookletImage;

	int CurrentBookletPage = 0;

	public GameObject PrevPageButton, NextPageButton;

	public void Awake(){
		ExtrasPage.localScale = new Vector2(0, 1);
		BookletImage.sprite = BookletPages[0];
	}

	public void StartBMC1(){
		if(!Transitioning){
			SceneManager.LoadScene(2);
		}
	}

	public void StartBMC2(){
		if(!Transitioning){
			SceneManager.LoadScene(3);
		}
	}

	public void StartBMC3(){
		if(!Transitioning){
			SceneManager.LoadScene(4);
		}
	}

	public void OpenExtrasMenu(){
		if(!Transitioning){
			StartCoroutine(ToggleExtras(true));
		}
	}

	public void CloseExtrasMenu(){
		if(!Transitioning){
			StartCoroutine(ToggleExtras(false));
		}
	}

	IEnumerator ToggleExtras(bool entering){
    Transitioning = true;
    float dt = 0f;
    float time = 1f;
    while (dt < time)
    {
        dt += Time.deltaTime;
        MainPage.localScale = entering? Vector2.Lerp(new Vector2(1, 1), new Vector2(0, 1), dt/time) : Vector2.Lerp(new Vector2(0, 1), new Vector2(1, 1), dt/time);
				ExtrasPage.localScale = entering? Vector2.Lerp(new Vector2(0, 1), new Vector2(1, 1), dt/time) : Vector2.Lerp(new Vector2(1, 1), new Vector2(0, 1), dt/time);
        yield return null;
    }
    Transitioning = false;
  }



	public void NextBookletPage(){
		if(CurrentBookletPage == 0){
			PrevPageButton.SetActive(true);
		}
		if(CurrentBookletPage < BookletPages.Count){
			CurrentBookletPage++;
			BookletImage.sprite = BookletPages[CurrentBookletPage];
			if(CurrentBookletPage + 1 == BookletPages.Count){
				NextPageButton.SetActive(false);
			}
		}
	}

	public void PrevBookletPage(){
		if(CurrentBookletPage + 1 == BookletPages.Count){
			NextPageButton.SetActive(true);
		}
		if(CurrentBookletPage > 0){
			CurrentBookletPage--;
			BookletImage.sprite = BookletPages[CurrentBookletPage];
			if(CurrentBookletPage == 0){
				PrevPageButton.SetActive(false);
			}
		}
	}
}
