using UnityEngine;

public class SplashEvents : MonoBehaviour
{
	public GameState GameState;
	public MusicController MusicController;
	public Animator PowerAnimator;
	public SoundEffect BootSound;
	public SoundEffect Beats1Sound;
	public SoundEffect Beats2Sound;

	public void StartMusic()
	{
		MusicController.StartMusic();
	}

	public void StartGame()
	{
		GameState.StartGame();
	}

	public void PowerOn()
	{
		PowerAnimator.SetBool("IsOn", true);
	}

	public void PlayBleeps()
	{
		BootSound.Play();
	}

	public void PlayBeats1()
	{
		Beats1Sound.Play();
	}

	public void PlayBeats2()
	{
		Beats2Sound.Play();
	}
}
