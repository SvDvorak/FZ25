using UnityEngine;

public class Aiming : MonoBehaviour
{
	public float MoveSpeed;
	public Animator Animator;
	public Transform PistolReticle;
	public Transform ShotgunReticle;
	public Transform RifleReticle;
	//public Vector2 Global;
	//public Vector2 GlobalRound;

    void Start()
    {
    }

    public void UpdateInput()
    {
	    var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

	    var newPosition = transform.localPosition + move * Time.deltaTime * MoveSpeed;
		transform.localPosition = new Vector3(
			Mathf.Clamp(newPosition.x, -0.005f, 0.245f),
			Mathf.Clamp(newPosition.y, 0.05f, 0.245f));
		//Global = transform.position;
		//GlobalRound = Extensions.RoundToGlobalGrid(transform.position);
		//Extensions.DrawBox(GlobalRound, Extensions.GlobalPixelSize, Color.red, 0);
	}

    public void SetVisible(bool isVisible)
    {
		Animator.SetBool("Visible", isVisible);
    }
}
