using UnityEngine;
using System.Collections;

/// <summary>
/// Class in charge to follow the player and to place the left and right walls on the screen
/// 
/// This script is attached to the Main Camera. This script is in charge to follow the Player vertically.
/// </summary>
public class MainCameraManager : MonoBehaviorHelper
{
	/// <summary>
	/// Reference to the player
	/// </summary>
	public Transform player;
	/// <summary>
	/// Reference to the left wall
	/// </summary>
	public Transform left;
	/// <summary>
	/// Reference to the right wall
	/// </summary>
	public Transform right;

	/// <summary>
	/// if true = stop followgind player. false at game over
	/// </summary>
	public bool stopFollow = false;

	/// <summary>
	/// True by default. If true, the left and right walls will have always the same space between them
	/// </summary>
	public bool useContantWidth = true;
	/// <summary>
	/// If useContantWidth = true, the space between the left and right walls
	/// </summary>
	public float constantWidth = 7f;

	#if UNITY_TVOS
	void Awake()
	{
		constantWidth *= 1.5f;
	}
	#endif


	void OnEnable()
	{
		GameManager.OnGameStarted += OnStarted;

		GameManager.OnGameEnded += OnFinished;
	}
	void OnDisable()
	{
		GameManager.OnGameStarted -= OnStarted;

		GameManager.OnGameEnded -= OnFinished;
	}


	void Start ()
	{
		

		stopFollow = false;

		Camera cam = Camera.main;
		float height = 2f * cam.orthographicSize;
		float width = height * cam.aspect;


		float camHalfHeight = height/2f;
		float camHalfWidth = width/2f; 

		float size = Mathf.Min(camHalfHeight, camHalfWidth);

		if(useContantWidth)
			size = constantWidth;


		float decal = Mathf.Min(size*0.15f, size*0.15f);

		left.position = new Vector2 (-size + decal, 0);   

		right.position = new Vector2 (+size - decal, 0);   


	



	}

	private void OnStarted(){
		stopFollow = false;


	}


	#if UNITY_TVOS
	
	public void StartTVOS()
	{

		StartCoroutine(DoRotate());

	}



	/// <summary>
	/// Smoothly change the rotation on TV
	/// </summary>
	public IEnumerator DoRotate()
	{
		float timer = 0;
		float time = 0.3f;

		while (timer <= time)
		{
			timer += Time.deltaTime;

			transform.eulerAngles = Vector3.forward * Mathf.Lerp(0,90,timer/time);

			left.parent.localEulerAngles = Vector3.forward * Mathf.Lerp(0,90,timer/time);
			yield return null;
		}
		left.parent.localEulerAngles = Vector3.forward * 90;
	}

	#endif

	private void OnFinished()
	{
		stopFollow = true;
	}

	/// <summary>
	/// To update the Y position of the camera, y position always  player Y position (if the game is not at Game Over state)
	/// </summary>
	public void UpdatePos()
	{

		if (stopFollow)
			return;


		Vector3 pos = transform.position;

		if (player == null)
			return;
		pos.y = player.transform.position.y;

		transform.position = pos;



	}
}
