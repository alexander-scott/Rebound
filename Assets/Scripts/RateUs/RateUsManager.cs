using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

/// <summary> 
/// Class to prompt a popup to ask the player to rate the game on the store
/// </summary>
public class RateUsManager : MonoBehaviour 
{

	#if UNITY_TVOS
	#else
	/// <summary> 
	/// Number of play to show the popup to ask the player to rate us. default value = 30
	/// </summary>
	public int NumberOfLevelPlayedToShowRateUs = 30;
	/// <summary> 
	/// iOS URL. Replace with your url
	/// </summary>
	public string iOSURL = "itms://itunes.apple.com/us/app/apple-store/id933517422?mt=8";
	/// <summary> 
	/// Android URL. Replace with your url
	/// </summary>
	public string ANDROIDURL = "http://app-advisory.com";


	public Button btnYes;
	public Button btnLater;
	public Button btnNever;

	public CanvasGroup popupCanvasGroup;

	void Awake()
	{
		popupCanvasGroup.alpha = 0;
		popupCanvasGroup.gameObject.SetActive(false);
	}

	void Start()
	{
		CheckIfPromptRateDialogue();
	}

	void AddButtonListeners()
	{
		btnYes.onClick.AddListener(OnClickedYes);
		btnLater.onClick.AddListener(OnClickedLater);
		btnNever.onClick.AddListener(OnClickedNever);
	}

	void RemoveButtonListener()
	{
		btnYes.onClick.RemoveListener(OnClickedYes);
		btnLater.onClick.RemoveListener(OnClickedLater);
		btnNever.onClick.RemoveListener(OnClickedNever);
	}

	void OnClickedYes()
	{
		#if UNITY_IPHONE
		Application.OpenURL(iOSURL);
		#endif

		#if UNITY_ANDROID
		Application.OpenURL(ANDROIDURL);
		#endif

		PlayerPrefs.SetInt("NumberOfLevelPlayedToShowRateUs",-1);
		PlayerPrefs.Save();
		HidePopup();
	}

	void OnClickedLater()
	{
		PlayerPrefs.SetInt("NumberOfLevelPlayedToShowRateUs",0);
		PlayerPrefs.Save();
		HidePopup();
	}

	void OnClickedNever()
	{
		PlayerPrefs.SetInt("NumberOfLevelPlayedToShowRateUs",-1);
		PlayerPrefs.Save();
		HidePopup();
	}

	void CheckIfPromptRateDialogue()
	{
		int count = PlayerPrefs.GetInt("NumberOfLevelPlayedToShowRateUs",0);

		if(count == -1)
			return;
		
		count ++;

		if(count > NumberOfLevelPlayedToShowRateUs)
		{
			PromptPopup();
		}
		else
		{
			PlayerPrefs.SetInt("NumberOfLevelPlayedToShowRateUs",count);
		}

		PlayerPrefs.Save();
	}

	public void PromptPopup()
	{
		popupCanvasGroup.alpha = 0;
		popupCanvasGroup.gameObject.SetActive(true);

		StartCoroutine(DoLerpAlpha(popupCanvasGroup, 0, 1, 1, () => {
			AddButtonListeners();
		}));
	}

	void HidePopup()
	{
		StartCoroutine(DoLerpAlpha(popupCanvasGroup, 1, 0, 1, () => {
			popupCanvasGroup.gameObject.SetActive(false);
			RemoveButtonListener();
		}));
	}

	public IEnumerator DoLerpAlpha(CanvasGroup c, float from, float to, float time, Action callback)
	{
		float timer = 0;

		c.alpha = from;

		while (timer <= time)
		{
			timer += Time.deltaTime;
			c.alpha = Mathf.Lerp(from, to, timer / time);
			yield return null;
		}

		c.alpha = to;

		if (callback != null)
			callback ();
	}

	#endif
}

