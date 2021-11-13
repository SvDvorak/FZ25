using System.Collections.Generic;
using UnityEngine;

public class HitMarkerPool : MonoBehaviour
{
	public GameObject MarkerTemplate;

	private readonly List<HitMarker> inUse = new List<HitMarker>();
	private readonly Stack<HitMarker> available = new Stack<HitMarker>();

	public void Update()
	{
		for(var i = 0; i < inUse.Count;)
		{
			var used = inUse[i];
			var finished = used.UpdateLifetime();
			if(finished)
			{
				inUse.RemoveAt(i);
				available.Push(used);
			}
			else
			{
				i += 1;
			}
		}
	}

    public void ShowMarkerAt(Vector2 position)
    {
		HitMarker marker;
		if(available.Count > 0)
		{
			marker = available.Pop();
		}
		else
		{
			var markerInstance = Instantiate(MarkerTemplate);
			markerInstance.transform.SetParent(transform);
			markerInstance.transform.localScale = Vector3.one;
			marker = markerInstance.GetComponent<HitMarker>();
		}

		Place(marker, position);
    }

    private void Place(HitMarker marker, Vector2 position)
    {
		inUse.Add(marker);
		marker.transform.position = position;
		marker.Activate();
    }
}
