using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class InteractionArrow : MonoBehaviour
{
	public Sprite Up;
	public Sprite Down;
	public Sprite Left;
	public Sprite Right;

	private SpriteRenderer spriteRenderer;

	void OnEnable()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

    public void SetDirection(string direction)
    {
	    switch(direction)
	    {
		    case "Up":
			    spriteRenderer.sprite = Up;
			    break;
		    case "Down":
			    spriteRenderer.sprite = Down;
			    break;
		    case "Left":
			    spriteRenderer.sprite = Left;
			    break;
		    case "Right":
			    spriteRenderer.sprite = Right;
			    break;
			default:
				spriteRenderer.sprite = null;
				break;
	    }
    }
}
