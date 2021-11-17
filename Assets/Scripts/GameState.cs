using UnityEngine;

public class GameState : MonoBehaviour
{
	public static bool Playing;
	public static float StartTime;
	public static float ElapsedGameTime => Time.time - StartTime;
	public bool HasPlayedIntro;

	public Weapon ActiveWeapon;

	public Weapon Pistol;
	public Weapon Shotgun;

	void OnEnable() => Events.OnPlayerDied += StopPlaying;
	void OnDisable() => Events.OnPlayerDied -= StopPlaying;

	void Start()
	{
		Pistol.gameObject.SetActive(false);
		Shotgun.gameObject.SetActive(false);
	}

	public void StartGame()
	{
		HasPlayedIntro = true;
		Restart();
	}

	private void StopPlaying()
	{
		Playing = false;
	}

	public void Restart()
	{
		StartTime = Time.time;
		Playing = true;
		Pistol.Reset();
		Shotgun.Reset();
		SetActiveWeapon(Pistol);
		Events.GameStarted();
	}

	public void SwitchWeapon()
	{
		SetActiveWeapon(ActiveWeapon == Pistol ? Shotgun : Pistol);
	}

	private void SetActiveWeapon(Weapon newWeapon)
	{
		if(newWeapon == null || newWeapon == ActiveWeapon)
			return;

		if(ActiveWeapon != null)
			ActiveWeapon.gameObject.SetActive(false);
		ActiveWeapon = newWeapon;
		ActiveWeapon.gameObject.SetActive(true);
	}
}
