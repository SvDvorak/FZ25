using UnityEngine;

public class PistolReload : Reload
{
	public InteractionArrow InteractionArrow;
	public AmmoCounter AmmoCounter;
	public Animator Animator;

	public bool IsCocked { get; private set; } = true;

	public override void UpdateInput(bool isWeaponVisible)
	{
		base.UpdateInput(isWeaponVisible);

		var clipName = Animator.GetCurrentClipName();
		if (clipName == "Idle")
		{
			if(CachedInput == "Down")
			{
				Animator.SetTrigger("Unload");
				InteractionArrow.SetDirection("Up");
				IsCocked = false;
				ResetCachedInput();
			}
			else
			{
				InteractionArrow.SetDirection("Down");
			}
		}
		else if (clipName == "Unload" && CachedInput == "Up")
		{
			Animator.SetTrigger("Load");
			AmmoCounter.AddFull();
			InteractionArrow.SetDirection("Left");
			ResetCachedInput();
		}
		else if (clipName == "Load" && CachedInput == "Left")
		{
			Animator.SetTrigger("Cock");
			IsCocked = true;
			ResetCachedInput();
		}
	}
}