using UnityEngine;
using System.Collections;

public class ObstacleDestroyer : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		gameObject.SetActive (false);
		transform.position = Vector3.zero;
	}
}
