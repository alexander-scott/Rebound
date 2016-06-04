using UnityEngine;
using System.Collections;

/// <summary>
/// A script to handle the score and save the best score.
/// </summary>
public class ScoreManager 
{

	/// <summary>
	/// Save the score
	/// </summary>
	public static void SaveScore(int lastScore)
	{
		PlayerPrefs.SetInt ("LAST_SCORE", lastScore);

		int bestScore = PlayerPrefs.GetInt ("BEST_SCORE");

		if (lastScore > bestScore) {
			Debug.Log ("NEW BEST SCORE : " + lastScore);
			PlayerPrefs.SetInt ("BEST_SCORE", lastScore);
			PlayerPrefs.SetInt ("LAST_SCORE_IS_NEW_BEST", 1);
			return;
		}

		PlayerPrefs.SetInt ("LAST_SCORE_IS_NEW_BEST", 0);

	}

	/// <summary>
	/// Get the last score
	/// </summary>
	public static int GetLastScore()
	{
		return PlayerPrefs.GetInt ("LAST_SCORE");
	}

	/// <summary>
	/// Return true if the last score is a new best score
	/// </summary>
	public static bool GetLastScoreIsBest(){
		int temp = PlayerPrefs.GetInt ("LAST_SCORE_IS_NEW_BEST");
		if (temp == 1) {
			return true;
		}
		return false;
	}

	/// <summary>
	/// Get the best score
	/// </summary>
	public static int GetBestScore()
	{
		return PlayerPrefs.GetInt ("BEST_SCORE");
	}
}
