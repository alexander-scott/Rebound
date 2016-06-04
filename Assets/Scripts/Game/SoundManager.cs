using UnityEngine;
using System.Collections;

/// <summary>
/// Class in charge to play musics and fx
/// 
/// Script attached to the "SoundManager" GameObject (child of the MainCamara). In charge to play musics and sound effects.
/// 
/// To Change a background music: Find the GameObject "Main Camera", and find the GameObject "SoundManager" and add your Audioclip music in the "Music Game" field. Same thing for the Music Menu; and for the FX sounds.
/// </summary>
public class SoundManager : MonoBehaviorHelper
{
	/// <summary>
	/// Reference to the audio source use for music
	/// </summary>
	public AudioSource music;
	/// <summary>
	/// Reference to the audio source use for fx
	/// </summary>
	public AudioSource fx;

	/// <summary>
	/// Reference to the music use during the game
	/// </summary>
	public AudioClip musicGame;
	/// <summary>
	/// Reference to the music use in the menu
	/// </summary>
	public AudioClip musicMenu;

	/// <summary>
	/// Reference to the music use when the player touch an obstacle 
	/// </summary>
	public AudioClip musicGameOver;

	/// <summary>
	/// Reference to the fx played when the player jumps
	/// </summary>
	public AudioClip jumpFX;

	void Start()
	{
		PlayMusicMenu ();
	}

	/// <summary>
	/// Play the music game
	/// </summary>
	public void PlayMusicGame()
	{
		PlayMusic (musicGame);
	}
	/// <summary>
	/// Play the music game over
	/// </summary>
	public void PlayMusicGameOver()
	{
		playFX (musicGameOver);
	}
	/// <summary>
	/// Play the music menu
	/// </summary>
	public void PlayMusicMenu()
	{
		PlayMusic (musicMenu);
	}
	/// <summary>
	/// Play the jump fx
	/// </summary>
	public void PlayJumpFX()
	{
		playFX (jumpFX);
	}


	/// <summary>
	/// Play an audioclip to be used with music audio source
	/// </summary>
	private void PlayMusic(AudioClip a)
	{
		if (music != null && music.clip != null)
			music.Stop ();


		music.clip = a;
		music.Play ();
	}

	/// <summary>
	/// Play an audioclip to be used with fx audio source
	/// </summary>
	private void playFX(AudioClip a)
	{
		if (fx != null && fx.clip != null)
			fx.Stop ();

		fx.PlayOneShot (a);
	}


	public void MuteAllMusic()
	{
		music.Pause();
		fx.Pause();
	}

	public void UnmuteAllMusic()
	{
		music.Play();
		fx.Play();
	}






}
