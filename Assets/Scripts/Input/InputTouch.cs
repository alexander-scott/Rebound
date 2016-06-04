using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Class in charge to listen the touch or click, and send event to subscribers
/// </summary>
public class InputTouch : MonoBehaviorHelper
{
	/// <summary>
	/// Delegate to listen the touch or click, and send event to subscribers
	/// </summary>
	public delegate void TouchScreen();
	/// <summary>
	/// Event trigger when the player touch or click, send to all subscribers
	/// </summary>
	public static event TouchScreen OnTouchScreen;

	#if UNITY_TVOS
	private Vector2 startPosition;

	void OnEnable()
	{
		GameManager.OnGameStarted += OnGameStart;

		GameManager.OnGameEnded += OnGameOver;
	}
	void OnDisable()
	{
		GameManager.OnGameStarted -= OnGameStart;

		GameManager.OnGameEnded -= OnGameOver;
	}

	bool gameStarted = true;

	void OnGameStart()
	{
		print("Input Touch - OnGameStart");
		UnityEngine.Apple.TV.Remote.touchesEnabled = true;
		UnityEngine.Apple.TV.Remote.allowExitToHome = true;

		gameStarted = true;

		FindObjectOfType<StandaloneInputModule>().forceModuleActive = false;
	}

	void OnGameOver()
	{
//		UnityEngine.Apple.TV.Remote.touchesEnabled = false;
//		UnityEngine.Apple.TV.Remote.allowExitToHome = true;
//
//		gameStarted = false;
//
//		FindObjectOfType<EventSystem>().firstSelectedGameObject = gameManager.buttonStart.gameObject;
//		FindObjectOfType<StandaloneInputModule>().forceModuleActive = true;

		print("Input Touch - OnGameOver");
		UnityEngine.Apple.TV.Remote.touchesEnabled = false;
		UnityEngine.Apple.TV.Remote.allowExitToHome = true;

		FindObjectOfType<StandaloneInputModule>().forceModuleActive = true;

		var es = FindObjectOfType<EventSystem>();

		es.firstSelectedGameObject = es.currentSelectedGameObject;

		es.SetSelectedGameObject(es.currentSelectedGameObject);


		gameStarted = false;
	}

	void Start()
	{
		UnityEngine.Apple.TV.Remote.reportAbsoluteDpadValues = true;


	}


	#endif

	void Update () 
	{

		#if (UNITY_ANDROID || UNITY_IOS || UNITY_TVOS)

		#if UNITY_TVOS
		if(!gameStarted)
		{
			
			gameManager.OnStart();
			return;
		}
		#endif


		if( Input.touchCount > 0)
		{
			
			Touch touch = Input.GetTouch(0);

			TouchPhase phase = touch.phase;

			if (phase == TouchPhase.Began)
			{
				if(OnTouchScreen != null)
					OnTouchScreen();
			}
		}

		#endif

		#if (!UNITY_ANDROID && !UNITY_IOS && !UNITY_TVOS) || UNITY_EDITOR
	
		if(Input.GetMouseButtonDown(0))
		{
			if(OnTouchScreen != null)
				OnTouchScreen();
		}

	
		#endif
	}


}
//#if UNITY_TVOS
//private Vector2 startPosition;
//
//void Awake()
//{
//	OnGameStart();
//}
//
//bool gameStarted = true;
//
//void OnGameStart()
//{
//	UnityEngine.Apple.TV.Remote.touchesEnabled = true;
//	UnityEngine.Apple.TV.Remote.allowExitToHome = false;
//
//	gameStarted = true;
//
//	FindObjectOfType<StandaloneInputModule>().forceModuleActive = false;
//}
//
//public void OnGameOver()
//{
//	print("do game over");
//	UnityEngine.Apple.TV.Remote.touchesEnabled = false;
//	UnityEngine.Apple.TV.Remote.allowExitToHome = true;
//
//	FindObjectOfType<StandaloneInputModule>().forceModuleActive = true;
//
//	var es = FindObjectOfType<EventSystem>();
//
//	es.firstSelectedGameObject = es.currentSelectedGameObject;
//
//	es.SetSelectedGameObject(es.currentSelectedGameObject);
//
//
//	gameStarted = false;
//}
//
//void Start()
//{
//	UnityEngine.Apple.TV.Remote.reportAbsoluteDpadValues = true;
//}
//
//
//#endif