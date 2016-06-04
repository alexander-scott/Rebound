using UnityEngine;
using System.Collections;

/// <summary>
/// Class to avoid some duplicate codes. 
/// </summary>
public class MonoBehaviorHelper : MonoBehaviour
{

	private GameManager _gameManager;
	public GameManager gameManager
	{
		get
		{
			if (_gameManager == null)
				_gameManager = FindObjectOfType<GameManager> ();

			return _gameManager;
		}
	}

	private PlayerManager _playerManager;
	public PlayerManager playerManager
	{
		get
		{
			if (_playerManager == null)
				_playerManager = FindObjectOfType<PlayerManager> ();

			return _playerManager;
		}
	}

	private Transform _playerTransform;
	public Transform playerTransform
	{
		get
		{
			if (playerManager == null)
				return null;
			
			if (_playerTransform == null)
				_playerTransform = playerManager.transform;

			return _playerTransform;
		}
	}

	private SoundManager _soundManager;
	public SoundManager soundManager
	{
		get
		{
			if (_soundManager == null)
				_soundManager = FindObjectOfType<SoundManager> ();

			return _soundManager;
		}
	}

	private MainCameraManager _mainCameraManager;
	public MainCameraManager mainCameraManager
	{
		get
		{
			if (_mainCameraManager == null)
				_mainCameraManager = FindObjectOfType<MainCameraManager> ();

			return _mainCameraManager;
		}
	}

	private Transform _camTransform;
	public Transform camTransform
	{
		get
		{
			if (_camTransform == null)
				_camTransform = Camera.main.transform;

			return _camTransform;
		}
	}


	private ColorManager _colorManager;
	public ColorManager colorManager
	{
		get
		{
			if (_colorManager == null)
				_colorManager = FindObjectOfType<ColorManager> ();

			return _colorManager;
		}
	}


}
