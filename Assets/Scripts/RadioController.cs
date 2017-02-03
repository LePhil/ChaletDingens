using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour {


	private AudioSource _audioSource;

	private string _audioURL = "http://www.music.helsinki.fi/tmt/opetus/uusmedia/esim/a2002011001-e02.wav";
	//private string _audioURL = "file:///Users/yourNameHere/Dropbox/filename.wav";

	//
	public void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		if (_audioSource == null)
		{
			Debug.Log("No AudioSource component found!");
		}
		else
		{
			if (_audioSource.clip != null)
			{
				_audioSource.Play();
			}

			StartCoroutine(LoadAudioClip(_audioURL));
		}
	}

	//
	private IEnumerator LoadAudioClip(string audioURL)
	{
		Debug.Log("Loading clip from WWW");

		var audioLoader = new WWW(audioURL);
		var clip = audioLoader.audioClip;

		yield return new WaitUntil(() => clip.loadState.Equals(AudioDataLoadState.Loaded));

		if (clip.loadState.Equals(AudioDataLoadState.Loaded))
		{
			_audioSource.clip = audioLoader.GetAudioClip(false, false);

			Debug.Log("Playing clip...");
			_audioSource.Play();
		}
	}
}
