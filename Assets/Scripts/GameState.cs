using UnityEngine;

public class GameState : MonoBehaviour
{
	public static bool Playing = true;
	public static float StartTime;
	public static float ElapsedGameTime => Time.time - StartTime;

	public Weapon ActiveWeapon;

	void OnEnable() => Events.OnPlayerDied += StopPlaying;
	void OnDisable() => Events.OnPlayerDied -= StopPlaying;

	void Start()
	{
		StartTime = Time.time;
		Events.GameStarted();
	}

	private void StopPlaying()
	{
		Playing = false;
	}

	public void Restart()
	{
		StartTime = Time.time;
		Playing = true;
		Events.GameStarted();
	}
}
