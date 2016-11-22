using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;
	public AudioSource sfxSource;
	public AudioSource [] musicLayers;


	// Use this for initialization
	void Awake () {
		//if instance is null
		if (instance == null)
		{
			//set instance to this
			instance = this;
		}else if (instance != this)//instance is not null and is not this
		{
			//destroy this gameobject
			Destroy(this.gameObject);
		}
		//keeps soundManager between levels, allows for continuos sounds&music
		DontDestroyOnLoad(this);
	}

	//play single audioclip through sfx source
	void PlaySingleSfx(AudioClip clip)
	{
		sfxSource.PlayOneShot(clip);
	}

	//plays random sound effect through sfx source
	void PlayRandomSfx(AudioClip[] clips, float minVol = 0.95f, float maxVol = 1.05f, float minPitch = 0.95f, float maxPitch = 1.05f)
	{
		//picks random index
		int i = Random.Range(0, clips.Length);
		//sets randVol between minVol and maxVol
		float randVol = Random.Range(minVol, maxVol);
		//sets randPitch between minpitch and maxpitch
		float randPitch = Random.Range(minPitch, maxPitch);
		//applies randVol
		sfxSource.volume = randVol;
		//applies randPitch
		sfxSource.pitch = randPitch;
		//plays randomized sfx
		sfxSource.PlayOneShot(clips[i]);
	}

	//fade in music layers
	void FadeInLayers(int layerIndex) //if input param is int
	{
		//musicLayers[layerIndex]
	}
	void FadeInLayers(int[] layers) //if input param is int array
	{
		foreach (int i in layers)
		{

		}
	}
	//fade out music layers
	void FadeOutLayers(int layerIndex) //if input param is int
	{

	}
	void FadeOutLayers(int[] layers) //if input param is int array
	{

	}
}
