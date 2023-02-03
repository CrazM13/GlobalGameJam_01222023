using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDialog : MonoBehaviour {

	[SerializeField] private AIEnemy targetEnemy;
	[SerializeField] private float idleLineDelay = 10f;

	[Header("Audio")]
	[SerializeField] private AudioClip[] idleLines;
	[SerializeField] private AudioClip[] sightLines;
	[SerializeField] private AudioClip[] violentLines;
	private bool isViolent = false;

	private float timeUntilIdleLine = 0;
	private bool isIdle;

	private int lastIdleIndex = -1;
	private int lastSightIndex = -1;
	private int lastViolentIndex = -1;

	private AudioSource currentAudioSource;

	// Start is called before the first frame update
	void Start() {
		targetEnemy.OnStateChange.AddListener(OnStateChange);
	}

	// Update is called once per frame
	void Update() {
		if (!isViolent && ServiceLocator.CatchingManager.IsLastLife) {
			isViolent = true;
			Play(GetRandom(violentLines, lastViolentIndex));
		}

		if (isIdle) {
			timeUntilIdleLine -= Time.deltaTime;
			if (timeUntilIdleLine <= 0) {
				Play(GetRandom(idleLines, lastIdleIndex));
				timeUntilIdleLine = idleLineDelay * Random.Range(1, 1.4f);
			}
		}
	}

	private void OnStateChange(AIStates newState) {
		if (newState == AIStates.CHASE) {
			isIdle = false;
			Play(GetRandom(sightLines, lastSightIndex));
		} else {
			isIdle = true;
		}
	}

	private AudioClip GetRandom(AudioClip[] lines, int lastIndex) {
		int newIndex = Random.Range(0, lines.Length);

		return lines[(newIndex + (lastIndex + 1)) % lines.Length];
	}

	private void Play(AudioClip audio) {
		if (currentAudioSource) currentAudioSource.Stop();
		currentAudioSource = ServiceLocator.AudioManager.Play(audio, transform.position);
	}

}
