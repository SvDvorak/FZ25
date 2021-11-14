using System;
using UnityEngine;

public class ShotgunReload : MonoBehaviour
{
	public InteractionArrow InteractionArrow;
	public Animator Animator;
	public bool IsShotCocked;

	public void UpdateInput(AmmoCounter ammoCounter, bool isWeaponVisible)
	{
	    var clipName = GetCurrentClipName();
	    if(isWeaponVisible && clipName == "Idle" && !ammoCounter.IsFull && GetInput() == "Up")
	    {
			Animator.SetTrigger("Load");
			ammoCounter.AddSingle();
	    }
		else if(clipName == "Idle" && GetInput() == "Right")
	    {
		    Animator.SetBool("Cocked", true);
			InteractionArrow.SetDirection("Left");
	    }
		else if(clipName == "Cock" && GetInput() == "Left")
	    {
		    Animator.SetBool("Cocked", false);
		    if(ammoCounter.HasRoundsLoaded)
			    IsShotCocked = true;
	    }

	    if(clipName == "Idle" && isWeaponVisible && !ammoCounter.IsFull)
		    InteractionArrow.SetDirection("Up");
	    else if(clipName == "Idle" && !IsShotCocked)
		    InteractionArrow.SetDirection("Right");
	    else if(clipName == "Uncock")
		    InteractionArrow.SetDirection("Right");
	    else if(clipName == "Cock")
		    InteractionArrow.SetDirection("Left");
		else
		    InteractionArrow.SetDirection("None");
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
