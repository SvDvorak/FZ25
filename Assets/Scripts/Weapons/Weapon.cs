using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public abstract bool CanFire();
	public abstract void Fire();
	public abstract void UpdateInput(bool isWeaponVisible);
	public abstract void ReloadFull();
	public abstract bool IsFull();
	public abstract bool IsEmpty();

	protected virtual void OnEnable() => Events.OnGameStarted += ReloadFull;
	protected virtual void OnDisable() => Events.OnGameStarted -= ReloadFull;
}