using UnityEngine;

public class DebugTools : MonoBehaviour
{
	public GameObject TimeChangeActiveSprite;

    void Update()
    {
	    var speeding = Input.GetKey(KeyCode.LeftControl);
	    Time.timeScale = speeding ? 4 : 1;
        TimeChangeActiveSprite.SetActive(Application.isEditor && speeding);
    }
}
