using System.Collections;
using UnityEngine;

public class InputOrder : MonoBehaviour
{
	public Aiming Aiming;
	public Animator WeaponParent;
	public InteractionArrow InteractionArrow;
	public GameState GameState;
	private Coroutine waitAndHideRoutine;
	private bool waitForTimeOrButtonRelease;
	private float waitTimeEnd;
	private bool weaponHadFocusLastFrame = true;

	void OnEnable() => Events.OnPlayerDied += PauseInput;
	void OnDisable() => Events.OnPlayerDied -= PauseInput;

	void Update()
	{
		if(GameState.Playing)
			UpdateGame();
		else
			UpdateMenu();
	}

	private void UpdateGame()
	{
		if(waitForTimeOrButtonRelease)
		{
			if(waitTimeEnd < Time.time)
				waitForTimeOrButtonRelease = false;
			else if(Input.anyKey)
				return;
		}

		waitForTimeOrButtonRelease = false;
		var weaponHasFocus = IsWeaponVisible;

		if(PressedWeaponSelect())
		{
			GameState.SwitchWeapon();
			if(GameState.ActiveWeapon.CanFire() && IsWeaponVisible)
				SetWeaponVisible(false);
		}

		if(IsWeaponVisible)
		{
			var weaponFullyLoaded = GameState.ActiveWeapon.IsFull() && GameState.ActiveWeapon.CanFire();
			if(weaponFullyLoaded && waitAndHideRoutine == null)
				waitAndHideRoutine = StartCoroutine(WaitAndHideWeapon());
		}

		if(GameState.ActiveWeapon.IsEmpty())
			SetWeaponVisible(true);

		if(GameState.ActiveWeapon.CanFire())
		{
			if(Input.GetKeyDown(KeyCode.Space) && !IsWeaponVisible)
				GameState.ActiveWeapon.Fire();
		}
		else
		{
			if(Input.GetKeyDown(KeyCode.Space))
				GameState.ActiveWeapon.DryFire();
			weaponHasFocus = true;
		}

		if(weaponHadFocusLastFrame != weaponHasFocus)
		{
			WaitForButtonRelease();
			InteractionArrow.SetDirection("None");
		}
		else if(weaponHasFocus)
		{
			GameState.ActiveWeapon.UpdateInput(IsWeaponVisible);
		}
		else
		{
			Aiming.UpdateInput();
		}

		weaponHadFocusLastFrame = weaponHasFocus;
	}

	private void PauseInput()
	{
		waitTimeEnd = Time.time + 2;
	}

	private void UpdateMenu()
	{
		HideGameFunctions();

		if(waitTimeEnd < Time.time && Input.GetKeyDown(KeyCode.Space))
		{
			GameState.Restart();
			Aiming.SetVisible(true);
		}
	}

	private IEnumerator WaitAndHideWeapon()
    {
	    yield return new WaitForSeconds(0.3f);
		SetWeaponVisible(false);
		WaitForButtonRelease();
		waitAndHideRoutine = null;
    }

	private void WaitForButtonRelease(float wait = 0.2f)
	{
		waitForTimeOrButtonRelease = true;
		waitTimeEnd = Time.time + wait;
	}

	private static bool PressedWeaponSelect()
    {
	    return Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
    }

	private void SetWeaponVisible(bool visible)
    {
		WeaponParent.SetBool("Visible", visible);
		Aiming.SetVisible(!visible);
    }

	private void HideGameFunctions()
	{
		WeaponParent.SetBool("Visible", false);
		Aiming.SetVisible(false);
		InteractionArrow.SetDirection("None");
	}

	private bool IsWeaponVisible => WeaponParent.GetBool("Visible");
}