using UnityEngine;

public abstract class Reload : MonoBehaviour
{
	protected string CachedInput { get; private set; }
	private float UncacheTime { get; set; }

	protected void ResetCachedInput()
	{
		CachedInput = null;
		UncacheTime = 0;
	}

	public virtual void UpdateInput(bool isWeaponVisible)
	{
		if (Game.GetInput() != null)
		{
			CachedInput = Game.GetInput();
			UncacheTime = Time.time + 0.5f;
		}
	}

	protected virtual void Update()
	{
		if (UncacheTime < Time.time)
			ResetCachedInput();
	}
}