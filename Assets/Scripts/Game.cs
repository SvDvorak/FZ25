using UnityEngine;

public static class Game
{
	public const float PixelSize = 0.01f;

	public static string GetInput()
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
}