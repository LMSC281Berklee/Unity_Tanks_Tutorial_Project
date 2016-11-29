using UnityEngine;
using System.Collections;
//using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

	public GameObject GameManagerAudio;

	// Use this for initialization
	void Start () {

		//GameManagerAudio = GameObject.Find ("GameManager");
		//GetComponent<AudioSource> ().mute = true; //Initialize the game by having the music muted - Conrad Robertson



	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Conrad Robertson
	public static void PlayMusic (GameObject GameManagerAudio){

		GameManagerAudio.GetComponent<AudioSource> ().mute = true; //Using the mute true/false to quickly see if function is working as hoped - Conrad Robertson

	}
}
