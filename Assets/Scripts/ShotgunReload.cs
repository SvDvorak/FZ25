using System;
using UnityEngine;

public class ShotgunReload : MonoBehaviour
{
	public InteractionArrow InteractionArrow;
	public Animator Animator;
	public bool IsShotCocked;

	private string cachedInput;
	private float uncacheTime;

	public void UpdateInput(AmmoCounter ammoCounter, bool isWeaponVisible)
	{
		if(GetInput() != null)
		{
			cachedInput = GetInput();
			uncacheTime = Time.time + 0.5f;
		}

	    var clipName = GetCurrentClipName();
	    if(isWeaponVisible && clipName == "Idle" && !ammoCounter.IsFull && cachedInput == "Up")
	    {
			Animator.SetTrigger("Load");
			ammoCounter.AddSingle();
			ResetCachedInput();
	    }
		else if(clipName == "Idle" && cachedInput == "Right")
	    {
		    Animator.SetBool("Cocked", true);
			InteractionArrow.SetDirection("Left");
			ResetCachedInput();
	    }
		else if(clipName == "Cock" && cachedInput == "Left")
	    {
		    Animator.SetBool("Cocked", false);
		    if(ammoCounter.HasRoundsLoaded)
			    IsShotCocked = true;
			ResetCachedInput();
	    }

	    if(clipName == "Idle" && isWeaponVisible && !ammoCounter.IsFull)
		    InteractionArrow.SetDirection("Up");
	    else if(clipName == "Idle" && !IsShotCocked && !ammoCounter.IsEmpty())
		    InteractionArrow.SetDirection("Right");
	    else if(clipName == "Uncock")
		    InteractionArrow.SetDirection("Right");
	    else if(clipName == "Cock")
		    InteractionArrow.SetDirection("Left");
		else
		    InteractionArrow.SetDirection("None");
    }

	private void ResetCachedInput()
	{
		cachedInput = null;
		uncacheTime = 0;
	}

	void Update()
	{
		if(uncacheTime < Time.time)
			ResetCachedInput();
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
	    return null;
    }

    public string GetCurrentClipName()
    {
	    var clipInfo = Animator.GetCurrentAnimatorClipInfo(0);
	    if(clipInfo.Length == 0)
		    return "Idle";
	    return clipInfo[0].clip.name;
    }
}
