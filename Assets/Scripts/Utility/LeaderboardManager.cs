using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SocialPlatforms;

/// <summary>
/// Class in charge of the leaderboard. Attached to the prefab LeaderboardManager, and create at start by the GameManager. Change the id!
/// </summary>
public static class LeaderboardManager
{
	/// <summary>
	/// Change it with your id!
	/// </summary>
	const string LEADERBOARDID = "fr.barouch.gravityball";

	static ILeaderboard lb;



	/// <summary>
	/// Authenticate and register a ProcessAuthentication callback
	/// This call needs to be made before we can proceed to other calls in the Social API
	/// </summary>
	public static void Init()
	{
		try
		{
			Social.localUser.Authenticate (ProcessAuthentication);
		}
		catch(Exception e)
		{
			Debug.Log("------ Authenticate - EXCEPTION : " + e.ToString());
		}
	}

	/// <summary>
	/// This function gets called when Authenticate completes
	/// Note that if the operation is successful, Social.localUser will contain data from the server.
	/// </summary>
	public static void ProcessAuthentication (bool success) 
	{
		#if UNITY_IOS && !UNITY_EDITOR
		try
		{
			if (success) 
			{
//				Debug.Log ("Authenticated, checking achievements");
//				try
//				{
//
//					// Request loaded achievements, and register a callback for processing them
//					Social.LoadAchievements ((IAchievement[] achievements) => {
//						//				if (achievements.Length == 0)
//						//				{
//						//					Debug.Log ("Error: no achievements found");
//						//				}
//						//				else
//						//				{
//						//					Debug.Log ("Got " + achievements.Length + " achievements");
//						//				}
//						//
//						//				// You can also call into the functions like this
//						//				Social.ReportProgress ("Achievement01", 100.0, delegate(bool result) 
//						//					{
//						//						if (result)
//						//						{
//						//							Debug.Log ("Successfully reported achievement progress");
//						//						}
//						//						else
//						//						{
//						//							Debug.Log ("Failed to report achievement");
//						//						}
//						//					});
//					});
//				}
//				catch(Exception e)
//				{
//					Debug.Log("------ LoadAchievements - EXCEPTION : " + e.ToString());
//				}
				try
				{
					Social.LoadScores(LEADERBOARDID, (IScore[] scores) => {

					});
				}
				catch(Exception e)
				{
					Debug.Log("------ LEADERBOARDID - EXCEPTION : " + e.ToString());
				}
			}
			else
			{
				Debug.Log ("Failed to authenticate");
			}
		}
		catch(Exception e)
		{
			Debug.Log("------ ProcessAuthentication - EXCEPTION : " + e.ToString());
		}
		#endif
	}

	/// <summary>
	/// Call this function to open the leaderboard UI
	/// </summary>
	public static void ShowLeaderboardUI()
	{
		#if UNITY_IOS && !UNITY_EDITOR
		Social.ShowLeaderboardUI();
		#endif
	}

	/// <summary>
	/// Check if the game service is initialized
	/// </summary>
	public static bool IsInitialized()
	{
		#if UNITY_IOS && !UNITY_EDITOR
		return Social.localUser.authenticated;
		#else
		return false;
		#endif

	}


	/// <summary>
	/// Report the score to the game service
	/// </summary>
	public static void ReportScore(int score)
	{

		#if UNITY_IOS && !UNITY_EDITOR
		try
		{
			if(IsInitialized())
			{
				try
				{
					Social.ReportScore(score,LEADERBOARDID,(bool success) => {
						Debug.Log("succefully post score leaderboard ? " + success);
					});
				}
				catch(Exception e)
				{
					Debug.Log("------ ReportScore - EXCEPTION : " + e.ToString());
				}
			}
		}
		catch(Exception e)
		{
			Debug.Log("------ IsInitialized - EXCEPTION : " + e.ToString());
		}
		#endif
	}


}
