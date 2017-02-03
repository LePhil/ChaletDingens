using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Utility;
using System.Collections;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
    , IColliderEventHoverEnterHandler
{
    public Transform switchObject;
    public bool gravityEnabled = false;
    public Vector3 impalse = Vector3.up;

	public GameObject alpStuff;
	public GameObject spaceStuff;
	public Light envLight;
	private float originalIntensity;

	public GameObject alpSound;
	public GameObject spaceSound;

    private bool m_gravityEnabled;
    private Vector3 previousGravity;

    public void SetGravityEnabled(bool value, bool forceSet = false)
    {
        if (ChangeProp.Set(ref m_gravityEnabled, value) || forceSet)
        {
            // change the apperence the switch
            switchObject.localEulerAngles = new Vector3(0f, 0f, value ? 15f : -15f);

            StopAllCoroutines();

            // Change the global gravity in the scene
            if (value)
            {
                Physics.gravity = previousGravity;
				envLight.intensity = originalIntensity;
            }
            else
            {
                previousGravity = Physics.gravity;
				envLight.intensity = 0.2f;

                StartCoroutine(DisableGravity());
            }
        }

        gravityEnabled = m_gravityEnabled;

		alpStuff.SetActive (gravityEnabled);
		spaceStuff.SetActive (!gravityEnabled);

		alpSound.GetComponent<AudioSource> ().mute = !gravityEnabled;
		spaceSound.GetComponent<AudioSource> ().mute = gravityEnabled;
    }

    private void Start()
    {
		originalIntensity = envLight.intensity;

        previousGravity = Physics.gravity;
        SetGravityEnabled(gravityEnabled, true);
    }

    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
    {
        SetGravityEnabled(!m_gravityEnabled);
    }

    private IEnumerator DisableGravity()
    {
        Physics.gravity = impalse;
        yield return new WaitForSeconds(0.3f);
        Physics.gravity = Vector3.zero;
    }
}