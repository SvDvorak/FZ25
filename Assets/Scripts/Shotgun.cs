using UnityEngine;

public class Shotgun : Weapon
{
	public AmmoCounter AmmoCounter;
	public ShotgunReload ShotgunReload;
	public override bool CanFire() => ShotgunReload.isShotCocked;
	public override bool IsFull() => AmmoCounter.IsFull;

	public override bool IsEmpty() => AmmoCounter.IsEmpty();

	public override void Fire()
	{
		if(ShotgunReload.isShotCocked)
		{
			AmmoCounter.RemoveSingle();
			ShotgunReload.isShotCocked = false;
		}
	}

	public override void UpdateInput(bool isWeaponVisible)
	{
		ShotgunReload.UpdateInput(AmmoCounter, isWeaponVisible);
		AmmoCounter.SetAmmoColor(ShotgunReload.isShotCocked);
	}
}