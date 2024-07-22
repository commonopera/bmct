using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour {

	public RectTransform MainPage, ExtrasPage;
	bool Transitioning = false;

	public List<Sprite> BookletPages;
	public Image BookletImage;

	int CurrentBookletPage = 0;

	public GameObject PrevPageButton, NextPageButton, PasswordPopup, ComposersNotesPage;

	string SecretCode = "";

	bool PasswordPopupAvailable = true, PasswordPopupActive = false;

	public TMP_InputField InputField;
	public AudioSource GoodAudioFeedback, BadAudioFeedback;

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

	public void OpenComposersNotesPage(){
		ComposersNotesPage.SetActive(true);
	}

	public void CloseComposersNotesPage(){
		ComposersNotesPage.SetActive(false);
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
				AchievementManager.TryUnlockAchievement(AchievementID.WELL_READ);
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

	public void ClosePasswordPopup(){
		PasswordPopup.SetActive(false);
		PasswordPopupActive = false;
	}

	void Update(){
		if(PasswordPopupAvailable && Input.GetKey("c") && Input.GetKey("o")){
			PasswordPopup.SetActive(true);
			PasswordPopupActive = true;
		}
		if(Input.GetKeyDown(KeyCode.Return) && PasswordPopupActive){
			string checkString = InputField.text.ToLower().Trim();
			if(checkString == "baroque technologies" || checkString == "dream manufacture" ||
			checkString == "genre refinement" || checkString == "systemic works" ||
			checkString == "interactive tapestry" || checkString == "density engineering" ||
			checkString == "intentional craft" || checkString == "human labor" ||
			checkString == "schematic entertainment" || checkString == "precision worlding" ||
			checkString == "detail smithing" || checkString == "media development" ||
			checkString == "concept choreography" || checkString == "personal fantasy" ||
			checkString == "orchestral media" || checkString == "generic mythos" ||
			checkString == "bespoke sagas" || checkString == "idea translation" ||
			checkString == "universal experience" || checkString == "data sculpture" ||
			checkString == "ritual programming" || checkString == "patient chrysalis"){
				GoodAudioFeedback.Play();
				PasswordPopupAvailable = false;
				PasswordPopup.SetActive(false);
				PasswordPopupActive = false;
				AchievementManager.TryUnlockAchievement(AchievementID.TRUE_CARTOGRAPHER);
			}
			else{
				BadAudioFeedback.Play();
				PasswordPopup.SetActive(false);
				PasswordPopupActive = false;
			}
		}
		/*
		if(Input.anyKeyDown){
			if(Input.GetKey("v")){
				if(SecretCode == ""){
					SecretCode = "v";
				}
				else{
					SecretCode = "";
				}
			}
			else if(Input.GetKey("o")){
				if(SecretCode == "v"){
					SecretCode = "vo";
				}
				else{
					SecretCode = "";
				}
			}
			else if(Input.GetKey("u")){
				if(SecretCode == "vo"){
					SecretCode = "vou";
				}
				else{
					SecretCode = "";
				}
			}
			else if(Input.GetKey("c")){
				if(SecretCode == "vou"){
					SecretCode = "vouc";
				}
				else{
					SecretCode = "";
				}
			}
			else if(Input.GetKey("h")){
				if(SecretCode == "vouc"){
					SecretCode = "vouch";
				}
				else{
					SecretCode = "";
				}
			}
			else if(Input.GetKey("e")){
				if(SecretCode == "vouch"){
					SecretCode = "vouche";
				}
				else{
					SecretCode = "";
				}
			}
			else if(Input.GetKey("r")){
				if(SecretCode == "vouche"){
					AchievementManager.TryUnlockAchievement(AchievementID.TRUE_CARTOGRAPHER);
				}
				else{
					SecretCode = "";
				}
			}
			else{
				SecretCode = "";
			}
		}*/
	}
}
