using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Health : MonoBehaviour
{
	private Animator animator;
	private int health = 4;

	void OnEnable()
	{
		animator = GetComponent<Animator>();
		UpdateHealth();

		Events.OnGameStarted += AddFullHealth;
	}

	void OnDisable() => Events.OnGameStarted -= AddFullHealth;

	public void TakeDamage()
	{
		health = Mathf.Clamp(health - 1, 0, 4);
		UpdateHealth();
	}

	private void UpdateHealth()
	{
		animator.SetInteger("Health", health);

		if(health == 0)
			Events.PlayerDied();
	}

	public void AddFullHealth()
	{
		health = 4;
		UpdateHealth();
	}
}
