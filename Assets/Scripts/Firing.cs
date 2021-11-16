using UnityEngine;

public abstract class Firing : MonoBehaviour
{
	public HitMarkerPool Pool;

	public abstract void Fire();

	protected void TryHitAt(Vector2 position)
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

}