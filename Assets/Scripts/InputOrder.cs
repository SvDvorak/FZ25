using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public abstract bool CanFire();
	public abstract void Fire();
	public abstract void UpdateInput(bool isWeaponVisible);
	public abstract bool IsFull();
	public abstract bool IsEmpty();
}

public class InputOrder : MonoBehaviour
{
	public Aiming Aiming;
	public Animator WeaponParent;
	public Weapon ActiveWeapon;
	public InteractionArrow InteractionArrow;
	private Coroutine waitAndHideRoutine;
	private bool waitForButtonRelease;
	private bool weaponHadFocusLastFrame;

	void Update()
	{
		if(waitForButtonRelease && Input.anyKey)
			return;

		waitForButtonRelease = false;
		var weaponHasFocus = IsWeaponVisible;

		if(IsWeaponVisible)
		{
			var weaponFullyLoaded = ActiveWeapon.IsFull() && ActiveWeapon.CanFire();
			if (weaponFullyLoaded && waitAndHideRoutine == null)
				waitAndHideRoutine = StartCoroutine(WaitAndHideWeapon());
 			else if (PressedWeaponSelect())
            {
	            SetWeaponVisible(false);
	            waitForButtonRelease = true;
            }
		}

		if(ActiveWeapon.IsEmpty())
			SetWeaponVisible(true);

        if(ActiveWeapon.CanFire())
        {
	        if(Input.GetKeyDown(KeyCode.Space) && !IsWeaponVisible)
		        ActiveWeapon.Fire();
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
			ActiveWeapon.UpdateInput(IsWeaponVisible);
		}
		else
		{
			Aiming.UpdateInput();
		}

		weaponHadFocusLastFrame = weaponHasFocus;
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
    }

	private bool IsWeaponVisible => WeaponParent.GetBool("Visible");
}