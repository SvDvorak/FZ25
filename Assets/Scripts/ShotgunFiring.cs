using UnityEngine;

public class ShotgunFiring : Firing
{
	public HitMarkerPool Pool;

	public override void Fire()
	{
		TryHitAt(transform.parent);
	}

	private void TryHitAt(Transform transform)
	{
		var aimPos = Extensions.RoundToGlobalGrid(transform.position);
		var boxPos = aimPos + Extensions.GlobalPixelSize / 4;
		var overlapBox = Physics2D.OverlapBox(boxPos, Extensions.GlobalPixelSize / 4, 0); // Make box half size of pixel to not overlap on edges
		if(overlapBox != null)
		{
			Pool.ShowMarkerAt(aimPos);
			//Debug.Log(overlapBox.name);
		}

		// DEBUG!
		//var pixelCorner = Extensions.RoundToGlobalGrid(transform.position);
		//var boxCorner = pos;
		//Extensions.DrawBox(pixelCorner, Extensions.GlobalPixelSize, Color.yellow);
		//Extensions.DrawBox(boxCorner, Extensions.GlobalPixelSize / 2, Color.red);

	}
}