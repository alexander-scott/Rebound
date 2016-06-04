using UnityEngine;
using System.Collections;

/// <summary>
/// A simple script attached to "AmbiantLightBackground" (who is a child of the Main Camera) to fit this sprite to the camera size. 
/// </summary>
public class ResizeBackgroundSize : MonoBehaviour 
{
	void Start()
	{
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if (sr == null) return;

		transform.localScale = new Vector3(1,1,1);

		float width = sr.sprite.bounds.size.x;
		float height = sr.sprite.bounds.size.y;

		var worldScreenHeight = Camera.main.orthographicSize * 2.0f;
		var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height, 1);
	}
}
