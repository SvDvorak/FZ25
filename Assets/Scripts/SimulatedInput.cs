using UnityEngine;

public class SimulatedInput : MonoBehaviour
{
	public Animator Dpad;
	public Animator XButton;
	public Animator YButton;

    void Update()
    {
        Dpad.SetInteger("Horizontal", Mathf.RoundToInt(Input.GetAxis("Horizontal")));
        Dpad.SetInteger("Vertical", Mathf.RoundToInt(Input.GetAxis("Vertical")));
        XButton.SetBool("Pressed", Input.GetKey(KeyCode.Space));
        YButton.SetBool("Pressed", Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
    }
}
