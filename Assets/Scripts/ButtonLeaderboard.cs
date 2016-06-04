using UnityEngine;
using System.Collections;

public class ButtonLeaderboard : MonoBehaviour
{
	public void OnClicked()
	{
		LeaderboardManager.ShowLeaderboardUI();
	}
}
