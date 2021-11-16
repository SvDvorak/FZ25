using System.Collections.Generic;
using UnityEngine;

public class ZombieSoundManager : MonoBehaviour
{
	public ZombieSpawner ZombieSpawnerInstance;
	public List<AudioClip> HitSounds;
	public List<AudioClip> AttackSounds;
	public List<AudioClip> MoanSounds;
	public List<float> DistanceVolumes;
	public AnimationCurve MoanDensityDelay;

	public SoundEffect HitSoundSource;
	public List<SoundEffect> MoanSoundSources;
	public List<SoundEffect> AttackSoundSources;

	public static ZombieSoundManager Instance;
	private float nextZombieSoundTime;

	void OnEnable()
	{
		Instance = this;
		Events.OnGameStarted += StopAllMoans;
	}

	void OnDisable() => Events.OnGameStarted -= StopAllMoans;

	void Update()
	{
		if(nextZombieSoundTime < Time.time)
		{
			PlayMoan();
		}
	}

	private void PlayMoan()
	{
		var moanDensityDelay = MoanDensityDelay.Evaluate(ZombieSpawnerInstance.Zombies.Count);
		nextZombieSoundTime = Time.time + 1.5f + Random.Range(0.2f, moanDensityDelay);
		
		var source = GetFreeSource(MoanSoundSources);
		if(source == null || ZombieSpawnerInstance.Zombies.Count == 0)
			return;

	    source.Clip = MoanSounds.GetRandomElement();
	    var randomDistance = ZombieSpawnerInstance.Zombies.GetRandomElement().Distance;
	    source.Volume = DistanceVolumes[randomDistance - 1];
		source.Play();
	}

	private void StopAllMoans()
	{
		foreach(var moan in MoanSoundSources)
		{
			moan.Stop();
		}
	}

	private static SoundEffect GetFreeSource(List<SoundEffect> sources)
	{
		foreach(var source in sources)
		{
			if(!source.IsPlaying)
				return source;
		}

		return null;
	}

	public static void PlayHit(Zombie zombie)
    {
	    var hitSource = Instance.HitSoundSource;
	    if(hitSource.IsPlaying)
		    return;

	    hitSource.Clip = Instance.HitSounds.GetRandomElement();
	    hitSource.Volume = 0.35f + Instance.DistanceVolumes[zombie.Distance - 1];
		hitSource.Play();
    }

	public static void PlayAttack()
	{
		var source = GetFreeSource(Instance.AttackSoundSources);
	    if(source == null)
		    return;

	    source.Clip = Instance.AttackSounds.GetRandomElement();
		source.Play();
	}
}
