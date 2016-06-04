using UnityEngine;
using System.Collections;

/// <summary>
/// Class in charge to change the background color 
/// </summary>
public class ColorManager : MonoBehaviour 
{
	/// <summary>
	/// Reference to the background sprite we will change to update the background color
	/// </summary>
	public SpriteRenderer sprite;
	/// <summary>
	/// Time we wait between each background color changed
	/// </summary>
	public float timeChangeColor = 10;

	void OnEnable()
	{
		StartCoroutine(ChangeColor ());
	}

	void OnDisable(){
		StopAllCoroutines ();
	} 

	/// <summary>
	/// The current background color
	/// </summary>
	public Color color;

	/// <summary>
	/// Change the color each "timeChangeColor" seconds
	/// </summary>
	IEnumerator ChangeColor(){

		while (true) {

			Color colorTemp =  Utils.GetRandomColor();

			StartCoroutine(DoLerp(sprite.color, colorTemp, 1f));

			yield return new WaitForSeconds (timeChangeColor);
		}

	}

	/// <summary>
	/// Smoothly change the color of the background
	/// </summary>
	public IEnumerator DoLerp(Color from, Color to, float time)
	{
		float timer = 0;
		while (timer <= time)
		{
			timer += Time.deltaTime;
			sprite.color = Color.Lerp(from, to, timer / time);
			yield return null;
		}
		sprite.color = to;
	}



}
