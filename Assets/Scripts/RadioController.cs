using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour {

	private AudioClip clip;

	void Start () {
		string bbc = "http://a.files.bbci.co.uk/media/live/manifesto/audio/simulcast/hls/uk/sbr_med/llnw/bbc_radio_one.m3u8";
		string srf = "http://stream.srg-ssr.ch/drs3/aacp_96.m3u";
		string ogg = "http://stream.radioreklama.bg/city_low.ogg";

		WWW liveStream = new WWW (ogg);  // start a download of the given URL
		clip = liveStream.GetAudioClip(false, true); // 2D, streaming

		AudioSource source = GetComponent<AudioSource> ();
		source.clip = clip;
		source.Play ();
		Debug.Log(clip);
	}
	
	// Update is called once per frame
	void Update () {
		//audio.Play();
	}
}
