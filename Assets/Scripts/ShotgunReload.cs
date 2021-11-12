using System;
using UnityEngine;

public class ReloadState
{
	public string AwaitedInput;
	public ReloadState NextState;
	public Action<Animator> UpdateAnimation;
}

public class ShotgunReload : MonoBehaviour
{
	public Animator Animator;

	public void UpdateInput(AmmoCounter ammoCounter, InteractionArrow interactionArrow)
	{
		ammoCounter.AddFull();
		ammoCounter.Cocked = true;

	    var clipName = GetCurrentClipName();
	    if(clipName == "Idle" && !ammoCounter.IsFull && GetInput() == "Up")
	    {
			Animator.SetTrigger("Load");
			ammoCounter.AddSingle();
	    }
		else if(clipName == "Idle" && GetInput() == "Right")
	    {
		    Animator.SetBool("Cocked", true);
			interactionArrow.SetDirection("Left");
	    }
		else if(clipName == "Cock" && GetInput() == "Left")
	    {
		    Animator.SetBool("Cocked", false);
		    if(ammoCounter.HasRoundsLoaded)
			    ammoCounter.Cocked = true;
	    }

	    if(clipName == "Idle" && !ammoCounter.IsFull)
		    interactionArrow.SetDirection("Up");
	    else if(clipName == "Idle" && !ammoCounter.Cocked)
		    interactionArrow.SetDirection("Right");
	    else if(clipName == "Uncock")
		    interactionArrow.SetDirection("Right");
	    else if(clipName == "Cock")
		    interactionArrow.SetDirection("Left");
		else
		    interactionArrow.SetDirection("None");
    }

    private string GetInput()
    {
	    if(Input.GetKeyDown(KeyCode.UpArrow))
		    return "Up";
	    if(Input.GetKeyDown(KeyCode.DownArrow))
		    return "Down";
	    if(Input.GetKeyDown(KeyCode.LeftArrow))
		    return "Left";
	    if(Input.GetKeyDown(KeyCode.RightArrow))
		    return "Right";
	    return "None";
    }

    public string GetCurrentClipName()
    {
	    var clipInfo = Animator.GetCurrentAnimatorClipInfo(0);
	    if(clipInfo.Length == 0)
		    return "Idle";
	    return clipInfo[0].clip.name;
    }
}
