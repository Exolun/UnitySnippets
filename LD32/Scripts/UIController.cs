using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {
	public GameObject generalDialog;

	public void ShowDeathDialog() {
		this.generalDialog.SetActive (true);
		GameObject.Find ("GeneralDialogMsg").GetComponent<UnityEngine.UI.Text> ().text = "You Died!";
	}

	public void ShowVictoryDialog(){
		this.generalDialog.SetActive (true);
		GameObject.Find ("GeneralDialogMsg").GetComponent<UnityEngine.UI.Text> ().text = "You Win!";
	}

	public void RestartLevel(){
		Application.LoadLevel("Level1");
		this.generalDialog.SetActive (false);
	}

	public void ExitGame(){
		Application.Quit ();
		this.generalDialog.SetActive (false);
	}
}
