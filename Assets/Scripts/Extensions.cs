using UnityEngine;

public static class Extensions
{
	public static float RoundToGrid(float value)
	{
		const float gridSize = 0.01f;
		return Mathf.Round(value * (1 / gridSize)) * gridSize;
	}

	public static bool RandomBool() => Random.Range(0, 1) == 0;
}