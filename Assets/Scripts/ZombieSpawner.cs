using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
	public float StepTime;
	public GameObject ZombieTemplate;
	public Transform[] DistanceLines;
	public Health Health;
	private List<Zombie> zombies = new List<Zombie>();

    void Update()
    {
	    if(Input.GetKeyDown(KeyCode.LeftAlt))
		    Spawn();

	    foreach(var zombie in zombies)
	    {
		    if(zombie.StepElapsed > StepTime)
		    {
			    var newDistance = zombie.Distance - 1;
				if (newDistance == 0)
					zombie.Attack(Health);
				else
					zombie.StepCloser(DistanceLines[newDistance - 1], newDistance);
		    }
	    }
    }

    private void Spawn()
    {
	    var zombie = Instantiate(ZombieTemplate, DistanceLines[DistanceLines.Length - 1]);
		zombies.Add(zombie.GetComponent<Zombie>());
    }

}