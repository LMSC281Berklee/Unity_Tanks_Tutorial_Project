﻿using UnityEngine;
using System.Collections;

public class LayerTester : MonoBehaviour {
	int[] allLayers = new int[] { 0, 1, 2, 3 };
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			SoundManager.instance.FadeInLayers(0);
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			SoundManager.instance.FadeOutLayers(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			SoundManager.instance.FadeInLayers(1);
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			SoundManager.instance.FadeOutLayers(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			SoundManager.instance.FadeInLayers(2);
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			SoundManager.instance.FadeOutLayers(2);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			SoundManager.instance.FadeInLayers(3);
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			SoundManager.instance.FadeOutLayers(3);
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			SoundManager.instance.FadeInLayers(allLayers);
		}
		if (Input.GetKeyDown(KeyCode.Z))
		{
			SoundManager.instance.FadeOutLayers(allLayers);
		}
	}
}
