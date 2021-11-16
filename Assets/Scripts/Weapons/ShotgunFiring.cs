using UnityEngine;

public class ShotgunFiring : Firing
{
	public int PelletCount;
	public PolygonCollider2D HitArea;
	private Bounds hitAreaBounds;

	void Start()
	{
		hitAreaBounds = HitArea.bounds;
		hitAreaBounds.center = Vector3.zero;
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

	private static Vector3 RandomPointInBounds(Bounds bounds)
	{
		return new Vector3(
			Random.Range(bounds.min.x, bounds.max.x),
			Random.Range(bounds.min.y, bounds.max.y),
			Random.Range(bounds.min.z, bounds.max.z)
		);
	}
}