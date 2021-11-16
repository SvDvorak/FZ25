using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
	public float StepTime;
	public float InitialSpawnTime;
	public float EndSpawnTime;
	public float DifficultyChangeTime;
	public float CurrentSpawnTime;
	public AnimationCurve DifficultyCurve;
	public GameObject ZombieTemplate;
	public Transform[] DistanceLines;
	public Health Health;

	public readonly List<Zombie> Zombies = new List<Zombie>();
	private float lastSpawnTime;

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

	    CurrentSpawnTime = Mathf.Lerp(InitialSpawnTime, EndSpawnTime,
		    DifficultyCurve.Evaluate(Mathf.Clamp01(GameState.ElapsedGameTime / DifficultyChangeTime)));

	    if(GameState.ElapsedGameTime - lastSpawnTime > CurrentSpawnTime)
	    {
			Spawn();
			lastSpawnTime = GameState.ElapsedGameTime;
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
		lastSpawnTime = GameState.ElapsedGameTime;
    }
}