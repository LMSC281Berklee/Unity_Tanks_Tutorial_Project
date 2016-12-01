using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class MusicSourceCreator : EditorWindow {
	AudioSource template;
	
	//adds class to unity editor windows
	[MenuItem("Window/MusicSourceCreator")]
	public static void GetWindow()
	{
		EditorWindow.GetWindow(typeof(MusicSourceCreator));
	}

	//runs clear and fill function on button press
	void OnGUI()
	{
		if (GUI.Button(new Rect(4, 4, position.width - 8, 24), "Reset Music AudioSources"))
		{
			ClearAndFillAudioSources();
		}
	}

	//removes all audiosources attached to gameObjects with the clip array function
	//creates new audiosources from template based on clips in clip array function
	void ClearAndFillAudioSources()
	{
		//find templete
		GameObject temp = (GameObject)Resources.Load("AudioTemplates/AudioSourceTemplate(Music)");
		//set template's audiosource
		template = temp.GetComponent<AudioSource>();
		
		//find clipArrays
		ClipArray[] clipArrays = FindObjectsOfType<ClipArray>();

		//itterates through each clipArray found
		foreach (ClipArray clipArray in clipArrays)
		{
			//get gameobject from clip array
			GameObject musicGO = clipArray.gameObject;
			//get audio sources on gameobject
			AudioSource [] audioSources = musicGO.GetComponents<AudioSource>();
			//destroy each audiosource in audioSources
			foreach (AudioSource audioSource in audioSources)
			{
				DestroyImmediate(audioSource);
			}
			//set clipCount
			int clipCount = clipArray.layerClips.Length;
			//replaces layers with new array at same length as layerClips
			clipArray.layers = new AudioSource[clipCount];
			for (int i = 0; i < clipCount; i++)
			{
				//creates audioSource
				clipArray.layers[i] = musicGO.AddComponent<AudioSource>();
				//adds audio clip
				clipArray.layers[i].clip = clipArray.layerClips[i];
				//apply template data to new AudioSource
				clipArray.layers[i].volume = template.volume;
				clipArray.layers[i].loop = template.loop;
				clipArray.layers[i].outputAudioMixerGroup = template.outputAudioMixerGroup;
			}
		}
	}
}
