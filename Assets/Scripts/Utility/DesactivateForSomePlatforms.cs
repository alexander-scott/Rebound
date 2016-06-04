using UnityEngine;
using System.Collections;

public class DesactivateForSomePlatforms : MonoBehaviour 
{
	void Awake()
	{
		#if UNITY_TVOS
		gameObject.SetActive(false);
		#else
		#endif
	}
}
