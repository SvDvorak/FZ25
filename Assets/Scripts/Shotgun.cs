using UnityEngine;

public class Shotgun : Weapon
{
	public AmmoCounter AmmoCounter;
	public ShotgunReload ShotgunReload;
	public Firing ShotgunFiring;

	public override bool CanFire() => ShotgunReload.IsShotCocked;
	public override bool IsFull() => AmmoCounter.IsFull;

	public override bool IsEmpty() => AmmoCounter.IsEmpty();

	public void Start()
	{
		AmmoCounter.AddFull();
		ShotgunReload.IsShotCocked = true;
	}

	public override void Fire()
	{
		if(ShotgunReload.IsShotCocked)
		{
			AmmoCounter.RemoveSingle();
			ShotgunFiring.Fire();
			ShotgunReload.IsShotCocked = false;
		}
	}

	public override void UpdateInput(bool isWeaponVisible)
	{
		ShotgunReload.UpdateInput(AmmoCounter, isWeaponVisible);
		AmmoCounter.SetAmmoColor(ShotgunReload.IsShotCocked);
	}
}