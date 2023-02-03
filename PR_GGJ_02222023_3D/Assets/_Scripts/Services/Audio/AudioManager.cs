using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager {

	public AudioSource Play(AudioClip audio, Vector3 position) {
		GameObject audioSourceObject = new GameObject("Audio Source");
		audioSourceObject.transform.position = position;

		AudioSource audioSource = audioSourceObject.AddComponent<AudioSource>();
		audioSource.clip = audio;
		audioSource.Play();

		GameObject.Destroy(audioSourceObject, audio.length + 1);

		return audioSource;
	}

}
