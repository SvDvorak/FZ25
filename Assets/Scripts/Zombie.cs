using UnityEngine;

public class Zombie : MonoBehaviour
{
	public int Health;
	public GameObject[] sprites;
	public int Distance { get; set; } = 5;
	public bool IsDead { get; set; }
	public float StepElapsed => Time.time - stepTime;

	public Animator AttackAnimator;

	private float stepTime;
	private GameObject activeSprite;
	private Vector3 offset;
	private int fullHealth;

	void OnEnable()
	{
		foreach(var sprite in sprites)
			sprite.SetActive(false);

		ActiveSpriteAtCurrentDistance();

		stepTime = Time.time;

		offset = GetRandomOffset();
		transform.localPosition += offset;
		transform.localScale = new Vector3(Extensions.RandomBool() ? 1 : -1, 1);
	}

	void Start()
	{
		fullHealth = Health;
	}

	public void StepCloser(Transform parent, int newDistance)
	{
		Distance = newDistance;
		transform.SetParent(parent, false);
		transform.localPosition = offset;
		ActiveSpriteAtCurrentDistance();
		stepTime = Time.time;
	}

	private void ActiveSpriteAtCurrentDistance()
	{
		if(activeSprite != null)
			activeSprite.SetActive(false);
		activeSprite = sprites[Distance - 1];
		activeSprite.SetActive(true);
	}

	private Vector3 GetRandomOffset() => new Vector3(Extensions.RoundToLocalGrid(Random.Range(0, 0.24f)), 0);

	public void Attack(Health health)
	{
		health.TakeDamage();
		stepTime = Time.time;
		AttackAnimator.SetTrigger("Chomp");
	}

	void LateUpdate() => AttackAnimator.ResetTrigger("Chomp");

	public void TakeDamage()
	{
		Health = Mathf.Clamp(Health - 1, 0, fullHealth);
		IsDead = Health == 0;
	}
}