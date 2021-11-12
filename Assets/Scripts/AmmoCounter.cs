using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
	public int MaxRounds;
	public bool Cocked;
	public Transform RoundsRoot;
	public GameObject RoundTemplate;

	private int roundWidth;
	private int loadedRounds;
	private readonly List<GameObject> roundInstances = new List<GameObject>();

	public bool IsFull => loadedRounds == MaxRounds;
	public bool HasRoundsLoaded => loadedRounds > 0;

	public void Start()
	{
		var spriteRenderer = RoundTemplate.GetComponentInChildren<SpriteRenderer>();
		roundWidth = (int)spriteRenderer.sprite.rect.width;
	}

	public void AddSingle()
	{
		var roundInstance = Instantiate(RoundTemplate, RoundsRoot);
		roundInstance.transform.localPosition = new Vector3(loadedRounds * (roundWidth + 1), 0) * Game.PixelSize;
		roundInstances.Add(roundInstance);
		loadedRounds = Mathf.Clamp(loadedRounds + 1, 0, MaxRounds);
	}

	public void AddFull()
	{
		for(int i = 0; i < MaxRounds - loadedRounds; i++)
			AddSingle();
	}

	public void RemoveSingle()
	{
		roundInstances.RemoveAt(roundInstances.Count - 1);
	}
}