using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffect : MonoBehaviour
{
	public float MaxRandomPitchDiff = 0;
	public float Delay = 0;
	private AudioSource audioSource;
	private float initialPitch;

	void OnEnable()
	{
		audioSource = GetComponent<AudioSource>();
		initialPitch = audioSource.pitch;
	}

	public void Play()
	{
		audioSource.pitch = initialPitch;
		if(Mathf.Abs(MaxRandomPitchDiff) > 0.0001f)
			audioSource.pitch += Random.value * MaxRandomPitchDiff;
		if(Delay > 0.0001f)
			audioSource.PlayDelayed(Delay);
		else
			audioSource.Play();
	}
}
