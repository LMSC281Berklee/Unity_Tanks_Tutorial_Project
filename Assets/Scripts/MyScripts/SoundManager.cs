using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;
	public AudioSource sfxSource;
	public AudioSource [] musicLayers;
	float fadeOutRate = 0.4f;
	float fadeInRate = 0.2f;
	float crossFadeRate = 0.3f;
	Coroutine[] fadeInRoutines;
	Coroutine[] fadeOutRoutines;
    //Variables for sfx randomization
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.07f;

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

    //Alex Harder
    //Based on code from https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/audio-and-sound-manager
    //Lead by wjensen
    //Fall 2016
    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        sfxSource.clip = clip;

        //Play the clip.
        sfxSource.Play();
    }


    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        sfxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        sfxSource.clip = clips[randomIndex];

        //Play the clip.
        sfxSource.Play();
    }





//wjensen
//starts FadeIn coroutine and stores coroutine in parallel array fadeOutRoutines
public void FadeInLayers(int layerIndex) //if input param is int
	{
		//checks whether or not fadeInRoutine is already running
		if (fadeInRoutines[layerIndex] == null)
		{
			//starts fadeIn coroutine and stores coroutine in parallel array fadeInRoutines
			fadeInRoutines[layerIndex] = StartCoroutine(FadeIn(layerIndex,fadeInRate));
		}
	}
	//wjensen
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
				fadeInRoutines[i] = StartCoroutine(FadeIn(i,fadeInRate));
			}
		}
	}
	//wjensen
	//starts FadeOut coroutine and stores coroutine in parallel array fadeOutRoutines
	public void FadeOutLayers(int layerIndex) //if input param is int
	{
		//checks whether or not fadeOutRoutine is already running
		if (fadeOutRoutines[layerIndex] == null)
		{
			fadeOutRoutines[layerIndex] = StartCoroutine(FadeOut(layerIndex, fadeOutRate));
		}
	}
	//wjensen
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
				fadeOutRoutines[i] = StartCoroutine(FadeOut(i,fadeOutRate));
			}
		}
	}
	//wjensen
	//crossfades inputed layers at crossFadeRate
	public void CrossFadeLayers(int layerOut, int layerIn)
	{
		//runs only if layerIn is not currently fading in, prevents stacking of fades
		if (fadeOutRoutines[layerIn] == null)
		{
			fadeInRoutines[layerIn] = StartCoroutine(FadeIn(layerIn, crossFadeRate));
		}
		//runs only if layerOut is not currently fading out, prevents stacking of fades
		if (fadeOutRoutines[layerOut] == null)
		{
			fadeOutRoutines[layerOut] = StartCoroutine(FadeOut(layerOut, crossFadeRate));
        }
	}
	//wjensen
	//fade in layer
	IEnumerator FadeIn(int layerIndex, float fadeRate) //if input param is int
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
		//increments layer volume until layer volume is at full volume
		while (layer.volume < 1)
		{
			float layerVolume = layer.volume;
			//if predicted layer volume is greater than 1 set to 1
			if (layer.volume + fadeRate * Time.deltaTime >= 1)
			{
				layer.volume = 1;
			}
			else //increase layer.volume
			{
				layer.volume += fadeRate * Time.deltaTime;
			}
			yield return null;
		}
		//sets parallel fadeInRoutine coroutine to null to indicate that this coroutine is finished
		fadeInRoutines[layerIndex] = null;
		yield break;
	}
	//wjensen
	//fade out layer
	IEnumerator FadeOut(int layerIndex, float fadeRate) //if input param is int
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
		while (layer.volume > 0)
		{
			float layerVolume = layer.volume;
			//if predicted layer volume is less than 0 set to 0
			if (layer.volume - fadeRate * Time.deltaTime <= 0)
			{
				layer.volume = 0;
			}
			else //decrease layer.volume
			{
				layer.volume -= fadeRate * Time.deltaTime;
			}
			yield return null;
		}
		//sets parallel fadeOutRoutines to null to indicate that this coroutine is finished
		fadeOutRoutines[layerIndex] = null;
		yield break;
	}
}
