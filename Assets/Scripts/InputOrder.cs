using System.Collections;
using UnityEngine;

public class InputOrder : MonoBehaviour
{
	public Aiming Aiming;
	public Animator WeaponParent;
	public InteractionArrow InteractionArrow;
	public GameState GameState;
	private Coroutine waitAndHideRoutine;
	private bool waitForButtonRelease;
	private bool weaponHadFocusLastFrame = true;

	void Update()
	{
		if(GameState.Playing)
			UpdateGame();
		else
			UpdateMenu();
	}

	private void UpdateGame()
	{
		if(waitForButtonRelease && Input.anyKey)
			return;

		waitForButtonRelease = false;
		var weaponHasFocus = IsWeaponVisible;

		if(IsWeaponVisible)
		{
			var weaponFullyLoaded = GameState.ActiveWeapon.IsFull() && GameState.ActiveWeapon.CanFire();
			if(weaponFullyLoaded && waitAndHideRoutine == null)
				waitAndHideRoutine = StartCoroutine(WaitAndHideWeapon());
			else if(PressedWeaponSelect())
			{
				SetWeaponVisible(false);
				waitForButtonRelease = true;
			}
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
			weaponHasFocus = true;
		}

		if(weaponHadFocusLastFrame != weaponHasFocus)
		{
			waitForButtonRelease = true;
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

	private void UpdateMenu()
	{
		HideGameFunctions();

		if(Input.GetKeyDown(KeyCode.Space))
		{
			GameState.Restart();
			Aiming.SetVisible(true);
		}
	}

	private IEnumerator WaitAndHideWeapon()
    {
	    yield return new WaitForSeconds(0.3f);
		SetWeaponVisible(false);
		waitForButtonRelease = true;
		waitAndHideRoutine = null;
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