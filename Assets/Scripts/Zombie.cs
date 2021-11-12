using UnityEngine;

public class Zombie : MonoBehaviour
{
	public Sprite[] Sprites;

	public static int MaxDistance = 5;
	public int Distance { get; set; } = 5;
	public float StepElapsed => Time.time - stepTime;

	private float stepTime;
	private SpriteRenderer spriteRenderer;
	private Vector3 offset;

	void OnEnable()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = Sprites[Sprites.Length - 1];
		spriteRenderer.flipX = Extensions.RandomBool();

		stepTime = Time.time;

		offset = GetRandomOffset();
		transform.localPosition += offset;
	}

	public void StepCloser(Transform parent, int newDistance)
	{
		Distance = newDistance;
		transform.SetParent(parent, false);
		transform.localPosition = offset;
		stepTime = Time.time;

		spriteRenderer.sprite = Sprites[Distance - 1];
		spriteRenderer.sortingOrder = MaxDistance - Distance;
	}


	private Vector3 GetRandomOffset() => new Vector3(Extensions.RoundToGrid(Random.Range(0, 0.24f)), 0);
}
