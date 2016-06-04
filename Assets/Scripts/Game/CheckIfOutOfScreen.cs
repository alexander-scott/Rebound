using UnityEngine;
using System.Collections;

/// <summary>
/// Class attached to Obstacle prefaabs (Rectangle and carre in Prefabs/Obstacles folder)
/// This call will dispawned all obstacles who are out of screen
/// </summary>
public class CheckIfOutOfScreen : MonoBehaviorHelper
{

	public Renderer m_renderer;

	float size = -1;

	void Awake()
	{
		Camera cam = Camera.main;
		float height = 2f * cam.orthographicSize;
		float width = height * cam.aspect;

		#if UNITY_TVOS
		size = width;
		#else
		size = height;
		#endif
	}

	void OnEnable()
	{
		StopAllCoroutines();

		LaunchCoUpdate();
	}


	void OnDisable()
	{
		StopAllCoroutines();
	}

	void LaunchCoUpdate()
	{
		if (!Application.isPlaying)
			return;

		StartCoroutine(CoUpdate());
	}

	void StopCoUpdate()
	{
		gameObject.SetActive (false);
		StopAllCoroutines();
	}
		
	/// <summary>
	/// Verify each seconds if the obstacle is out of screen.
	/// </summary>
	IEnumerator CoUpdate()
	{
		while(true)
		{


			if(IsBehind())
			{
				
				break;
			}

			yield return new WaitForSeconds(1);;
		}

	
		StopCoUpdate();
	}

	/// <summary>
	/// Check if the obstacle is out of screen.
	/// </summary>
	bool IsBehind()
	{
		if (playerTransform == null)
			return true;

		Vector3 forward = transform.TransformDirection(Vector3.up);
		Vector3 toOther = playerTransform.position - transform.position;
		if (Vector3.Dot (forward, toOther) > size / 1.8f)
			return true;

		return false;
	}

}
