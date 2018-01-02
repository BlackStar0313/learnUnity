using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour {

	public AudioSource efxSource; 
	public AudioSource musicSource;
	public static SoundsManager instance = null;
	public float lowPitchRange = 0.95f ;
	public float highPitchRange = 1.05f;

	// Use this for initialization
	void Awake()
	{
		if (instance == null) 
			instance = this ;
		else if (instance != this) 
			Destroy(gameObject);
		
		DontDestroyOnLoad(gameObject);
	}

	public void PlaySingle(AudioClip clip) {
		efxSource.clip = clip ; 
		efxSource.Play();
	}

	public void RandomiSfx(params AudioClip[] clips) {
		int randomIndex = Random.Range(0 , clips.Length);

		float randomPich = Random.Range(lowPitchRange , highPitchRange);
		efxSource.pitch = randomPich;
		efxSource.clip = clips[randomIndex];
		efxSource.Play();
	}
}
