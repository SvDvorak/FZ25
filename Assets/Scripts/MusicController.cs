using UnityEngine;

public class MusicController : MonoBehaviour
{
	public AudioSource Music;

	public void StartMusic()
	{
		Music.Play();
	}

	void Update()
    {
	    if(Input.GetKeyDown(KeyCode.M))
	    {
			if(Music.isPlaying)
				Music.Stop();
			else
				Music.Play();
	    }
    }
}
