using UnityEngine;

public class Shotgun : Weapon
{
	public AmmoCounter AmmoCounter;
	public ShotgunReload ShotgunReload;
	public Firing ShotgunFiring;

	public override bool CanFire() => ShotgunReload.isShotCocked;
	public override bool IsFull() => AmmoCounter.IsFull;

	public override bool IsEmpty() => AmmoCounter.IsEmpty();

	public void Start()
	{
		AmmoCounter.AddFull();
		ShotgunReload.isShotCocked = true;
	}

	public override void Fire()
	{
		if(ShotgunReload.isShotCocked)
		{
			AmmoCounter.RemoveSingle();
			ShotgunFiring.Fire();
			ShotgunReload.isShotCocked = false;
		}
	}

	public override void UpdateInput(bool isWeaponVisible)
	{
		ShotgunReload.UpdateInput(AmmoCounter, isWeaponVisible);
		AmmoCounter.SetAmmoColor(ShotgunReload.isShotCocked);
	}
}