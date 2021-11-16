public class Pistol : Weapon
{
	public AmmoCounter AmmoCounter;
	public PistolReload PistolReload;
	public PistolFiring PistolFiring;

	public override bool CanFire() => !IsEmpty() && PistolReload.IsCocked;
	public override bool IsFull() => AmmoCounter.IsFull;
	public override bool IsEmpty() => AmmoCounter.IsEmpty;

	public override void Fire()
	{
		PistolFiring.Fire();
		AmmoCounter.RemoveSingle();
	}

	public override void UpdateInput(bool isWeaponVisible)
	{
		PistolReload.UpdateInput(isWeaponVisible);
	}

	public override void ReloadFull()
	{
		AmmoCounter.AddFull();
	}
}