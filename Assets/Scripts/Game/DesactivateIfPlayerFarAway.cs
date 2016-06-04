using UnityEngine;
using System.Collections;

public class DesactivateIfPlayerFarAway : MonoBehaviorHelper 
{
	void Update () 
	{
		if (playerTransform == null)
			return;

		float distance = Vector2.Distance(transform.position,playerTransform.position);

		if (distance > 10)
			gameObject.SetActive (false);
	}
}
