using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
	public float StepTime;
	public float DifficultyChangeTime;
	//public float CosDifficultyWaveVariation;
	//public float CosDifficultyWaveLengthInSeconds;
	public float CurveSpawnTime;
	public float CurrentSpawnTime;
	public float TimeElapsed;
	public AnimationCurve DifficultyCurve;
	public GameObject ZombieTemplate;
	public Transform[] DistanceLines;
	public Health Health;

	public readonly List<Zombie> Zombies = new List<Zombie>();
	private float lastSpawnTime;
	private bool hasStartedWithOne;

	void OnEnable() => Events.OnGameStarted += Reset;
	void OnDisable() => Events.OnGameStarted -= Reset;

    void Update()
    {
	    if(Input.GetKeyDown(KeyCode.LeftAlt))
		    Spawn();

	    if(!GameState.Playing)
		    return;

	    for(var i = 0; i < Zombies.Count;)
	    {
		    var zombie = Zombies[i];

		    if(zombie.IsDead)
		    {
			    ScoreKeeper.Score += 1;
			    Zombies.RemoveAt(i);
				Destroy(zombie.gameObject);
			    continue;
		    }

		    if(zombie.StepElapsed > StepTime)
		    {
			    var newDistance = zombie.Distance - 1;
			    if(newDistance == 0)
				    zombie.Attack(Health);
			    else
				    zombie.StepCloser(DistanceLines[newDistance - 1], newDistance);
		    }

		    i++;
	    }

	    TimeElapsed = GameState.ElapsedGameTime;
	    CurveSpawnTime = DifficultyCurve.Evaluate(Mathf.Clamp01(GameState.ElapsedGameTime / DifficultyChangeTime));
	    //CurrentSpawnTime = CurveSpawnTime + Mathf.Cos(GameState.ElapsedGameTime / (2 * Mathf.PI) * CosDifficultyWaveLengthInSeconds) * CosDifficultyWaveVariation;
	    CurrentSpawnTime = CurveSpawnTime;
	    //CurrentSpawnTime = Mathf.Max(0.2f, CurrentSpawnTime);

	    if (GameState.ElapsedGameTime - lastSpawnTime > CurrentSpawnTime)
	    {
			Spawn();
			lastSpawnTime = GameState.ElapsedGameTime;
			//Debug.Log(CurrentSpawnTime);
	    }
    }

    private void Spawn()
    {
	    var zombie = Instantiate(ZombieTemplate, DistanceLines[DistanceLines.Length - 1]);
		Zombies.Add(zombie.GetComponent<Zombie>());
    }

    public void Reset()
    {
	    foreach(var zombie in Zombies)
		    Destroy(zombie.gameObject);
		Zombies.Clear();
		lastSpawnTime = GameState.ElapsedGameTime - DifficultyCurve.Evaluate(0) * 0.75f;
    }
}