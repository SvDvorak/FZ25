using UnityEngine;

public class Shotgun : Weapon
{
	public AmmoCounter AmmoCounter;
	public ShotgunReload ShotgunReload;
	public Firing ShotgunFiring;

	public override bool CanFire() => ShotgunReload.IsShotCocked;
	public override bool IsFull() => AmmoCounter.IsFull;
	public override bool IsEmpty() => AmmoCounter.IsEmpty;

	public override void Fire()
	{
		if(ShotgunReload.IsShotCocked)
		{
			AmmoCounter.RemoveSingle();
			ShotgunFiring.Fire();
			ShotgunReload.IsShotCocked = false;
		}
	}

	public override void ReloadFull()
	{
		ShotgunReload.IsShotCocked = true;
		AmmoCounter.AddFull();
		AmmoCounter.SetAmmoColor(true);
	}

	public override void UpdateInput(bool isWeaponVisible)
	{
		ShotgunReload.UpdateInput(isWeaponVisible);
		AmmoCounter.SetAmmoColor(ShotgunReload.IsShotCocked);
	}
}