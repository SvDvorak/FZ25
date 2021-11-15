using System.Collections.Generic;
using UnityEngine;

public class ShotgunFiring : Firing
{
	public int PelletCount;
	public HitMarkerPool Pool;
	public PolygonCollider2D HitArea;
	//public List<Vector2Int> HitPositions;
	private Bounds hitAreaBounds;

	void OnEnable()
	{
		hitAreaBounds = HitArea.bounds;
	}

	public override void Fire()
	{
		var centerPos = transform.parent.position;
		var pelletsShot = 0;
		for(int i = 0; i < 1000; i++)
		{
			if(pelletsShot == PelletCount)
				break;

			var point = centerPos + RandomPointInBounds(hitAreaBounds);
			if (!HitArea.OverlapPoint(point))
				continue;

			TryHitAt(point);
			pelletsShot += 1;
		}
	}

	private void TryHitAt(Vector2 position)
	{
		var hitPos = Extensions.RoundToGlobalGrid(position);
		var boxPos = hitPos + Extensions.GlobalPixelSize / 4;
		// Make box half size of pixel to not overlap on edges
		var overlapBox = Physics2D.OverlapBox(boxPos, Extensions.GlobalPixelSize / 4, 0, LayerMask.GetMask("Zombie"));
		if(overlapBox != null)
		{
			var zombie = overlapBox.transform.parent.GetComponent<Zombie>();
			zombie.TakeDamage();
			Pool.ShowMarkerAt(hitPos);
			//Debug.Log(overlapBox.name);
		}

		// DEBUG!
		var pixelCorner = Extensions.RoundToGlobalGrid(position);
		var boxCorner = hitPos;
		Extensions.DrawBox(pixelCorner, Extensions.GlobalPixelSize, Color.yellow);
		Extensions.DrawBox(boxPos, Extensions.GlobalPixelSize / 2, Color.red);
	}

	private static Vector3 RandomPointInBounds(Bounds bounds)
	{
		return new Vector3(
			Random.Range(bounds.min.x, bounds.max.x),
			Random.Range(bounds.min.y, bounds.max.y),
			Random.Range(bounds.min.z, bounds.max.z)
		);
	}
}