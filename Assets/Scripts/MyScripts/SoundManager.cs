using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;
	public AudioSource sfxSource;
	public AudioSource [] musicLayers;
	float fadeOutRate = 0.4f;
	float fadeInRate = 0.2f;
	Coroutine[] fadeInRoutines;
	Coroutine[] fadeOutRoutines;

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
		
		//initializes fadeInRoutines
		fadeInRoutines = new Coroutine[musicLayers.Length];
		//initializes fadeOutRoutines
		fadeOutRoutines = new Coroutine[musicLayers.Length];
	}

	//play single audioclip through sfx source
	void PlaySingleSfx(AudioClip clip)
	{
		sfxSource.PlayOneShot(clip);
	}

	//plays random sound effect through sfx source
	public void PlayRandomSfx(AudioClip[] clips, float minVol = 0.95f, float maxVol = 1.05f, float minPitch = 0.95f, float maxPitch = 1.05f)
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

	//starts FadeIn coroutine and stores coroutine in parallel array fadeOutRoutines
	public void FadeInLayers(int layerIndex) //if input param is int
	{
		//checks whether or not fadeInRoutine is already running
		if (fadeInRoutines[layerIndex] == null)
		{
			//starts fadeIn coroutine and stores coroutine in parallel array fadeInRoutines
			fadeInRoutines[layerIndex] = StartCoroutine(FadeIn(layerIndex));
		}
	}
	//starts fadeIn coroutine for each layer in int[] and stores coroutines in parallel array fadeInRoutines
	public void FadeInLayers(int[] layers) //if input param is int array
	{
		//runs for each int in layers array
		foreach (int i in layers)
		{
			//checks whether or not fadeInRoutine is already running
			if (fadeInRoutines[i] == null)
			{
				//starts fadeIn coroutine and stores coroutine in parallel array fadeInRoutines
				fadeInRoutines[i] = StartCoroutine(FadeIn(i));
			}
		}
	}

	//starts FadeOut coroutine and stores coroutine in parallel array fadeOutRoutines
	public void FadeOutLayers(int layerIndex) //if input param is int
	{
		//checks whether or not fadeOutRoutine is already running
		if (fadeOutRoutines[layerIndex] == null)
		{
			fadeOutRoutines[layerIndex] = StartCoroutine(FadeOut(layerIndex));
		}
	}
	//starts FadeOut coroutine foreach layer in int[] and stores coroutines in parallel array fadeOutRoutines
	public void FadeOutLayers(int[] layers) //if input param is int array
	{
		//runs for each int in layers array
		foreach (int i in layers)
		{
			//checks whether or not fadeOutRoutine is already running
			if (fadeOutRoutines[i] == null)
			{
				//starts fadeOut coroutine and stores coroutine in parallel array fadeOutRoutines
				fadeOutRoutines[i] = StartCoroutine(FadeOut(i));
			}
		}
	}

	//fade in layer
	IEnumerator FadeIn(int layerIndex) //if input param is int
	{
		//initializes layer
		AudioSource layer = musicLayers[layerIndex];
		//executes if there is no fadeIn coroutine running for this layer
		if (fadeOutRoutines[layerIndex] != null)
		{
			//stop parallel fadeOut coroutine
			StopCoroutine(fadeOutRoutines[layerIndex]);
			//sets parallel fadeOutRoutines to null indicating coroutine has stopped
			fadeOutRoutines[layerIndex] = null;
		}
		//execute while layer volume is less than 1
		while (layer.volume < 1)
		{
			float layerVolume = layer.volume;
			//if predicted layer volume is greater than 1 set to 1
			if (layer.volume + fadeInRate * Time.deltaTime >= 1)
			{
				layer.volume = 1;
				//yield break;
			}
			else //increase layer.volume
			{
				layer.volume += fadeInRate * Time.deltaTime;
			}
			yield return null;
		}
		//sets parallel fadeInRoutine coroutine to null to indicate that this coroutine is finished
		fadeInRoutines[layerIndex] = null;
		yield break;
	}

	//fade out layer
	IEnumerator FadeOut(int layerIndex) //if input param is int
	{
		//initializes layer
		AudioSource layer = musicLayers[layerIndex];
		//executes if no fadeOut coroutine is currently running for this layer
		if (fadeInRoutines[layerIndex] != null)
		{
			//stops parallel fadeIn coroutine
			StopCoroutine(fadeInRoutines[layerIndex]);
			//sets parallel fadeInRoutines to null indicating routine is stopped
			fadeInRoutines[layerIndex] = null;
		}
		//executes while layer volume is greater than 0
		while (musicLayers[layerIndex].volume > 0)
		{
			float layerVolume = layer.volume;
			//if predicted layer volume is less than 0 set to 0
			if (layer.volume - fadeOutRate * Time.deltaTime <= 0)
			{
				layer.volume = 0;
				//yield break;
			}
			else //decrease layer.volume
			{
				layer.volume -= fadeOutRate * Time.deltaTime;
			}
			yield return null;
		}
		//sets parallel fadeOutRoutines to null to indicate that this coroutine is finished
		fadeOutRoutines[layerIndex] = null;
		yield break;
	}
}
