using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Utility;
using System.Collections;
using UnityEngine;

public class RadioSwitch : MonoBehaviour
, IColliderEventHoverEnterHandler
{
	public Transform switchObject;
	public bool radioEnabled = false;

	public GameObject alpSound;
	public GameObject spaceSound;
	public GameObject radio;

	private bool m_radioEnabled;
	private Vector3 previousGravity;

	public void SetRadioEnabled(bool value, bool forceSet = false)
	{
		if (ChangeProp.Set(ref m_radioEnabled, value) || forceSet)
		{
			// change the apperence the switch
			switchObject.localEulerAngles = new Vector3(0f, 0f, value ? 15f : -15f);

			radioEnabled = m_radioEnabled;

			radio.GetComponent<AudioSource> ().mute = !radioEnabled;
			if (!radioEnabled) {
				alpSound.GetComponent<AudioSource> ().volume = 0.33f;
				spaceSound.GetComponent<AudioSource> ().volume = 0.33f;
			} else {
				alpSound.GetComponent<AudioSource> ().volume = 0f;
				spaceSound.GetComponent<AudioSource> ().volume = 0f;
			}
		}
	}

	private void Start()
	{
		SetRadioEnabled(radioEnabled, false);
	}

	public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
	{
		SetRadioEnabled(!m_radioEnabled);
	}
}