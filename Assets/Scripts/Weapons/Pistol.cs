using UnityEngine;

public class Pistol : Weapon
{
	public AmmoCounter AmmoCounter;
	public PistolReload PistolReload;
	public PistolFiring PistolFiring;
	public Transform Reticle;

	public SoundEffect FireSound;
	public SoundEffect ClickSound;

	public override bool CanFire() => !IsEmpty() && PistolReload.IsCocked;
	public override bool IsFull() => AmmoCounter.IsFull;
	public override bool IsEmpty() => AmmoCounter.IsEmpty;

	public override void Fire()
	{
		PistolFiring.Fire();
		FireSound.Play();
		AmmoCounter.RemoveSingle();
	}

	public override void DryFire() => ClickSound.Play();

	public override void UpdateInput(bool isWeaponVisible)
	{
		PistolReload.UpdateInput(isWeaponVisible);
	}

	public override void ReloadFull()
	{
		AmmoCounter.AddFull();
	}

	protected override void OnEnable()
	{
		AmmoCounter.gameObject.SetActive(true);
		Reticle.gameObject.SetActive(true);
		base.OnEnable();
	}

	protected override void OnDisable()
	{
		AmmoCounter.gameObject.SetActive(false);
		Reticle.gameObject.SetActive(false);
		base.OnDisable();
	}
}