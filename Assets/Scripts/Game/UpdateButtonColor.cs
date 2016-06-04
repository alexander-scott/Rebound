using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/// <summary>
/// Script to change the start button accordign to the background color. 
/// This script is attached to CanvasStart/StartButotn GameObject.
/// </summary>
public class UpdateButtonColor : MonoBehaviorHelper {


	public Image image;

	void OnEnable()
	{
		StartCoroutine (ChangeColor ());

		GameManager.OnGameStarted += OnStarted;
	}

	void OnDisable()
	{
		GameManager.OnGameStarted -= OnStarted;
	}

	IEnumerator ChangeColor(){
		while (true) {
			Color32 c = colorManager.sprite.color;

			image.color = new Color32(c.r,c.g,c.b,c.a);

			yield return 0;
		}
	}

	void OnStarted()
	{
		StartCoroutine (StopCoroutineWithDelay ());
	}

	IEnumerator StopCoroutineWithDelay()
	{
		yield return new WaitForSeconds (5);

		StopAllCoroutines ();
	}


}
