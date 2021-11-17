using UnityEngine;

public class DebugTools : MonoBehaviour
{
	public GameObject TimeChangeActiveSprite;

    void Update()
    {
        if(Application.isEditor)
        {
	        var speeding = Input.GetKey(KeyCode.LeftControl);
	        Time.timeScale = speeding ? 5f : 1;
	        TimeChangeActiveSprite.SetActive(Application.isEditor && speeding);
        }
    }
}
