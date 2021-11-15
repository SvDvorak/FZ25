using System.Collections.Generic;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
	public int MaxRounds;
	public Transform RoundsRoot;
	public GameObject RoundTemplate;

	public int LoadedRounds => roundInstances.Count;

	private int roundWidth;
	private readonly List<SpriteRenderer> roundInstances = new List<SpriteRenderer>();

	public bool IsFull => LoadedRounds == MaxRounds;
	public bool HasRoundsLoaded => LoadedRounds > 0;

	public void OnEnable()
	{
		var spriteRenderer = RoundTemplate.GetComponentInChildren<SpriteRenderer>();
		roundWidth = (int)spriteRenderer.sprite.rect.width;
	}

	public void AddSingle()
	{
		if(IsFull)
			return;

		var roundInstance = Instantiate(RoundTemplate, RoundsRoot);
		roundInstance.transform.localPosition = new Vector3(LoadedRounds * (roundWidth + 1), 0) * Game.PixelSize;
		roundInstances.Add(roundInstance.GetComponent<SpriteRenderer>());
	}

	public void AddFull()
	{
		var remaining = MaxRounds - LoadedRounds;
		for(int i = 0; i < remaining; i++)
			AddSingle();
	}

	public void RemoveSingle()
	{
		if(roundInstances.Count > 0)
		{
			var instance = roundInstances[roundInstances.Count - 1];
			Destroy(instance);
			roundInstances.RemoveAt(roundInstances.Count - 1);
		}
	}

	public bool IsEmpty()
	{
		return LoadedRounds == 0;
	}

	public void SetAmmoColor(bool colorLast)
	{
		for(var i = 0; i < LoadedRounds; i++)
		{
			var instance = roundInstances[i];
			if(colorLast && i == LoadedRounds - 1)
				instance.color = new Color(1, 0.5f, 0.5f);
			else
				instance.color = Color.white;
		}
	}
}