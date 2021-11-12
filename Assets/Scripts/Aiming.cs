using UnityEngine;

public class Aiming : MonoBehaviour
{
	public float MoveSpeed;
	public Transform PistolReticle;
	public Transform ShotgunReticle;
	public Transform RifleReticle;

    void Start()
    {
        
    }

    void Update()
    {
	    var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

	    var newPosition = transform.localPosition + move * Time.deltaTime * MoveSpeed;
		transform.localPosition = new Vector3(
			Mathf.Clamp(newPosition.x, -0.005f, 0.245f),
			Mathf.Clamp(newPosition.y, 0.05f, 0.245f));
	}
}
