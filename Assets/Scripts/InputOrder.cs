using UnityEngine;

public class InputOrder : MonoBehaviour
{
	public AmmoCounter AmmoCounter;
	public InteractionArrow InteractionArrow;
	public ShotgunReload ShotgunReload;
	public Animator WeaponParent;

    void Start()
    {
    }

    void Update()
    {
		ShotgunReload.UpdateInput(AmmoCounter, InteractionArrow);

        if(Input.GetKeyDown(KeyCode.Space))
            WeaponParent.SetBool("Visible", !WeaponParent.GetBool("Visible"));
    }
}