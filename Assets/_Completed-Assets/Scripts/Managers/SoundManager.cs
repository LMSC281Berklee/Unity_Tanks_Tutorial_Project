using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

	public AudioSource m_MovementAudio;    
	public AudioClip m_EngineIdling;       
	public AudioClip m_EngineDriving; 
	private Rigidbody m_Rigidbody;
	public float m_PitchRange = 0.2f; 
	private float m_MovementInputValue;
	private float m_TurnInputValue;
	private float m_OriginalPitch;  
	GameObject GameManagerMusic;
	public GameObject TankShooting;
	public bool Fired;


	// Use this for initialization
	void Start () {
		m_Rigidbody = GetComponent<Rigidbody>();
		m_OriginalPitch = m_MovementAudio.pitch;
		GameManagerMusic = GameObject.Find ("GameManager");
		TankShooting = GameObject.Find ("CompleteTank");
		//TankShooting.GetComponent<TankShooting> ().m_Fired = true;

		//CR Run the play music function 
		PlayMusic (GameManagerMusic);

	}
	
	// Update is called once per frame
	void Update () {

	}

	// Bens function for tank sound.
	public void TankMovementSound ()
	{
		// If there is no input (the tank is stationary)...
		if (Mathf.Abs (m_MovementInputValue) < 0.1f && Mathf.Abs (m_TurnInputValue) < 0.1f)
		{
			// ... and if the audio source is currently playing the driving clip...
			if (m_MovementAudio.clip == m_EngineDriving)
			{
				// ... change the clip to idling and play it.
				m_MovementAudio.clip = m_EngineIdling;
				m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play ();
			}
		}
		else
		{
			// Otherwise if the tank is moving and if the idling clip is currently playing...
			if (m_MovementAudio.clip == m_EngineIdling)
			{
				// ... change the clip to driving and play.
				m_MovementAudio.clip = m_EngineDriving;
				m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play();
			}
		}
	}
	public static void PlayMusic (GameObject GameManagerMusic)
	{
		GameManagerMusic.GetComponent<AudioSource> ().mute = true;

	}

}
