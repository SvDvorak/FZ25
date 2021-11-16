using UnityEngine;

public static class Extensions
{
	public const float GlobalGridSize = 0.2f;
	public const float LocalGridSize = GlobalGridSize / 20;
	public static readonly Vector2 GlobalPixelSize = new Vector2(GlobalGridSize, GlobalGridSize);

	public static float RoundToLocalGrid(float value) => Mathf.Round(value * (1 / LocalGridSize)) * LocalGridSize;
	public static Vector2 RoundToLocalGrid(Vector2 v) => new Vector2(RoundToLocalGrid(v.x), RoundToLocalGrid(v.y));

	public static float RoundToGlobalGrid(float value) =>
		Mathf.Round((value + GlobalGridSize / 2) * (1 / GlobalGridSize)) * GlobalGridSize - GlobalGridSize / 2;
	public static Vector2 RoundToGlobalGrid(Vector2 v) => new Vector2(RoundToGlobalGrid(v.x), RoundToGlobalGrid(v.y));

	public static bool RandomBool() => Random.Range(0, 2) == 0;

	public static Color SetAlpha(this SpriteRenderer spr, float a) => spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, a);

	public static void DrawBox(Vector2 pos, Vector2 size, Color color, float duration = 3)
	{
		Debug.DrawLine(pos, pos + new Vector2(size.x, 0), color, duration);
		Debug.DrawLine(pos, pos + new Vector2(0, size.y), color, duration);
	}

	public static Vector2 ToV2(this Vector3 v)
	{
		return new Vector2(v.x, v.y);
	}

    public static string GetCurrentClipName(this Animator animator)
    {
	    var clipInfo = animator.GetCurrentAnimatorClipInfo(0);
	    if(clipInfo.Length == 0)
		    return "Idle";
	    return clipInfo[0].clip.name;
    }
}