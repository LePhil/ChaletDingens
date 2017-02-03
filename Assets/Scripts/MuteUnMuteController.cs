using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Utility;
using HTC.UnityPlugin.Vive;
using System.Collections.Generic;
using UnityEngine;

public class MuteUnMuteController : MonoBehaviour
, IColliderEventClickHandler
, IColliderEventPressEnterHandler
, IColliderEventPressExitHandler
{
	private AudioSource _audioSource;

	public GameObject alpSound;
	public GameObject spaceSound;

	public ControllerManagerSample controllerManager;
	[SerializeField]
	private ControllerButton m_activeButton = ControllerButton.Trigger;

	public Transform buttonObject;
	public Vector3 buttonDownDisplacement;

	private Vector3 buttonOriginPosition;
	private bool radioIsMuted = false;

	private HashSet<ColliderButtonEventData> pressingEvents = new HashSet<ColliderButtonEventData>();

	public ControllerButton activeButton
	{
		get
		{
			return m_activeButton;
		}
		set
		{
			m_activeButton = value;
			// set all child MaterialChanger heighlightButton to value;
			var changers = ListPool<MaterialChanger>.Get();
			GetComponentsInChildren(changers);
			for (int i = changers.Count - 1; i >= 0; --i) { changers[i].heighlightButton = value; }
			ListPool<MaterialChanger>.Release(changers);
		}
	}

	private void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		if (_audioSource == null) {
			Debug.Log ("No AudioSource component found!");
		} else {
			SetRadioMute(radioIsMuted);
		}
	}

	#if UNITY_EDITOR

	protected virtual void OnValidate()
	{
		activeButton = m_activeButton;
	}

	#endif

	public void SetRadioMute(bool value)
	{
		radioIsMuted = value;
		_audioSource.mute = value;

		controllerManager.UpdateActivity();

		if (value) {
			alpSound.GetComponent<AudioSource> ().volume = 0.33f;
			spaceSound.GetComponent<AudioSource> ().volume = 0.33f;
		} else {
			alpSound.GetComponent<AudioSource> ().volume = 0f;
			spaceSound.GetComponent<AudioSource> ().volume = 0f;
		}
	}

	public void OnColliderEventClick(ColliderButtonEventData eventData)
	{
		if (pressingEvents.Contains(eventData) && pressingEvents.Count == 1)
		{
			SetRadioMute(!radioIsMuted);
		}
	}

	public void OnColliderEventPressEnter(ColliderButtonEventData eventData){}
	public void OnColliderEventPressExit(ColliderButtonEventData eventData){}
}