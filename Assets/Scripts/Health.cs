using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Health : MonoBehaviour
{
	private Animator animator;
	private int health = 4;

	void OnEnable()
	{
		animator = GetComponent<Animator>();
		SetHealth();
	}

	public void TakeDamage()
	{
		health = Mathf.Clamp(health - 1, 0, 4);
		SetHealth();
	}

	private void SetHealth()
	{
		animator.SetInteger("Health", health);
	}
}
