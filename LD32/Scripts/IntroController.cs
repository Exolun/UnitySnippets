using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntroEvents{
	public static string SetMessage = "SetMessage";
	public static string FadeIn = "FadeIn";
	public static string FadeOut = "FadeOut";
	public static string DisplayMessage = "DisplayMessage";
	public static string IntroComplete = "IntroComplete";
}

public class IntroController : MonoBehaviour {
	private List<string> introMessages = new List<string>();
	public float delayBetweenMessage;
	public GameObject introMsgObj;
	public GameObject startMsg;
	public GameObject moon;
	public GameObject titleMsg;

	private float lastMsgDisplayTime = 0;
	private UnityEngine.UI.Text textComponent;
	private string currentIntroEvent = IntroEvents.FadeIn;
	private CanvasRenderer grp;

	void Start () {
		introMessages.Add ("In the long distant Past...");
		introMessages.Add ("On the moon of Earth...");
		introMessages.Add ("There were once two great Kingdoms...");
		introMessages.Add ("One of Light, and one of Darkness...");
		introMessages.Add ("Primordial forces, their war was eternal...");
		introMessages.Add ("Until one day, the King of Light boasted:");
		introMessages.Add ("'None can defeat Aeterno.  He is the ultimate warrior.'");
		introMessages.Add ("Thus, the gauntlet thrown, he sent Light's champion into the heart of Darkness");
		introMessages.Add ("To stand alone against the minions of Darkness...");

		this.textComponent = introMsgObj.GetComponent<UnityEngine.UI.Text> ();
		this.grp = introMsgObj.GetComponent<CanvasRenderer> ();


		this.setNextMessage ();
	}
	
	void FixedUpdate () {
		Debug.Log (this.currentIntroEvent);
		this.handleCurrentEvent ();
	}

	private void handleCurrentEvent(){
		if (this.currentIntroEvent == IntroEvents.FadeIn) {
			this.fadeIn ();
		} 
		else if (this.currentIntroEvent == IntroEvents.FadeOut) {
			this.fadeOut();

		} else if (this.currentIntroEvent == IntroEvents.SetMessage) {
			this.setNextMessage ();
		} 
		else if (this.currentIntroEvent == IntroEvents.DisplayMessage) {
			this.displayMessage();
		}

		if (this.currentIntroEvent == IntroEvents.IntroComplete) {
			if(Input.anyKeyDown){
				Application.LoadLevel("Level1");
			}
		} 
		else {

			if(Input.GetKeyDown(KeyCode.Escape)){
				Application.LoadLevel("Level1");
			}

			moon.transform.localScale =  (moon.transform.localScale + (new Vector3(.015f, .015f, .015f) * Time.deltaTime));
		}
	}

	private void displayMessage(){
		if (Time.time > this.lastMsgDisplayTime + delayBetweenMessage && this.introMessages.Count > 0) {
			this.currentIntroEvent = IntroEvents.SetMessage;
		} 
		else if(this.introMessages.Count == 0) {
			this.startMsg.SetActive(true);
			this.currentIntroEvent = IntroEvents.IntroComplete;
			this.titleMsg.SetActive(true);
		}
	}

	private void fadeOut(){
		if (this.textComponent.color.a <= 0) {
			this.currentIntroEvent = IntroEvents.SetMessage;
		} 
		else {
			var color = this.textComponent.color;
			var nextAlpha = color.a - Time.deltaTime * 255;
			if(nextAlpha < 0){
				nextAlpha = 0;
			}
			
			this.textComponent.color = new Color(color.r, color.g, color.b, nextAlpha);
			this.textComponent.Rebuild(UnityEngine.UI.CanvasUpdate.PreRender);
		}	
	}

	private void fadeIn(){
		if (this.textComponent.color.a >= 255) {
			this.currentIntroEvent = IntroEvents.DisplayMessage;
			this.lastMsgDisplayTime = Time.time;
		} 
		else {
			var color = this.textComponent.color;
			var nextAlpha = color.a + Time.deltaTime * 255;
			if(nextAlpha > 255){
				nextAlpha = 255;
			}

			this.textComponent.color = new Color(color.r, color.g, color.b, nextAlpha);
			this.textComponent.Rebuild(UnityEngine.UI.CanvasUpdate.PreRender);
		}		
	}

	private void setNextMessage(){
		var msg = introMessages[0];
		introMessages.Remove(msg);
		this.textComponent.text = msg;
		this.currentIntroEvent = IntroEvents.FadeIn;
	}
}
