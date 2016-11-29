using UnityEngine;
using System.Collections;
//using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

	public GameObject GameManagerMusic; //Create Game Object reference - Conrad Robertson

	// Use this for initialization
	void Start () {

		//GameManagerMusic = GameObject.Find ("GameManager"); //Define what object GameManagerMusic is - Conrad Robertson

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Conrad Robertson
	public static void PlayMusic (GameObject GameManagerMusic){

		//GetComponent<AudioSource> ().mute = false; //Simple test code to quickly see if function is working as hoped (Game starts with mute = true, testing if it will turn false on play) - Conrad Robertson

		GameManagerMusic.GetComponent<AudioSource> ().mute = false;

	}
}
