public class PistolFiring : Firing
{
	public override void Fire()
	{
		TryHitAt(transform.parent.position);
	}
}