using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
	public TMP_Text Text;
	public Animator ScoreScreenAnimator;
	public static int Score = 0;

	void OnEnable()
	{
		Events.OnPlayerDied += OnGameEnded;
		Events.OnGameStarted += OnGameStarted;
	}

	void OnDisable()
	{
		Events.OnPlayerDied -= OnGameEnded;
		Events.OnGameStarted -= OnGameStarted;
	}

	private void OnGameStarted() => ScoreScreenAnimator.SetBool("IsVisible", false);
	private void OnGameEnded() => ScoreScreenAnimator.SetBool("IsVisible", true);

	void Update()
	{
		Text.text = Score.ToString();
	}
}
