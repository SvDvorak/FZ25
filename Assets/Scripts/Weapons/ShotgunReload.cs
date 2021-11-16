using UnityEngine;

public class ShotgunReload : Reload
{
	public InteractionArrow InteractionArrow;
	public AmmoCounter AmmoCounter;
	public Animator Animator;
	public bool IsShotCocked;

	public override void UpdateInput(bool isWeaponVisible)
	{
		base.UpdateInput(isWeaponVisible);

	    var clipName = Animator.GetCurrentClipName();
	    if(isWeaponVisible && clipName == "Idle" && !AmmoCounter.IsFull && CachedInput == "Up")
	    {
			Animator.SetTrigger("Load");
			AmmoCounter.AddSingle();
			ResetCachedInput();
	    }
		else if(clipName == "Idle" && CachedInput == "Right")
	    {
		    Animator.SetBool("Cocked", true);
			InteractionArrow.SetDirection("Left");
			ResetCachedInput();
	    }
		else if(clipName == "Cock" && CachedInput == "Left")
	    {
		    Animator.SetBool("Cocked", false);
		    if(AmmoCounter.HasRoundsLoaded)
			    IsShotCocked = true;
			ResetCachedInput();
	    }

	    if(clipName == "Idle" && isWeaponVisible && !AmmoCounter.IsFull)
		    InteractionArrow.SetDirection("Up");
	    else if(clipName == "Idle" && !IsShotCocked && !AmmoCounter.IsEmpty)
		    InteractionArrow.SetDirection("Right");
	    else if(clipName == "Uncock")
		    InteractionArrow.SetDirection("Right");
	    else if(clipName == "Cock")
		    InteractionArrow.SetDirection("Left");
		else
		    InteractionArrow.SetDirection("None");
    }
}
