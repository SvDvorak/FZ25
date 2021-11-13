using UnityEngine;

public class HitMarker : MonoBehaviour
{
	public SpriteRenderer Sprite;
	public float LifeTime;
	private float startTime;

	public void Activate()
	{
		startTime = Time.time;
		Sprite.SetAlpha(1);
	}

    public bool UpdateLifetime()
    {
	    var elapsed = (Time.time - startTime) / LifeTime;
	    Sprite.SetAlpha(1 - Mathf.Clamp01(elapsed));
	    return elapsed > 1;
    }
}
