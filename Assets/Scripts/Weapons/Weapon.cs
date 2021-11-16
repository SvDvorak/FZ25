using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public abstract bool CanFire();
	public abstract void Fire();
	public abstract void UpdateInput(bool isWeaponVisible);
	public abstract void ReloadFull();
	public abstract bool IsFull();
	public abstract bool IsEmpty();

	public virtual void Reset()
	{
		ReloadFull();
		if(!gameObject.activeSelf)
			OnDisable();
	}

	protected virtual void OnEnable() { }
	protected virtual void OnDisable() { }
}