using UnityEngine;

public class SimulatedInput : MonoBehaviour
{
	public GameObject Up;
	public GameObject Down;
	public GameObject Left;
	public GameObject Right;
	public Animator XButton;
	public Animator YButton;

    void Update()
    {
	    var horizontal = Mathf.RoundToInt(Input.GetAxis("Horizontal"));
	    var vertical = Mathf.RoundToInt(Input.GetAxis("Vertical"));
		Up.SetActive(vertical > 0);
		Down.SetActive(vertical < 0);
		Left.SetActive(horizontal < 0);
		Right.SetActive(horizontal > 0);
        XButton.SetBool("Pressed", Input.GetKey(KeyCode.Space));
        YButton.SetBool("Pressed", Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
    }
}
