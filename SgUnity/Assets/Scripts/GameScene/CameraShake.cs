using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public Transform CamTransform;

	// How long the object should shake for.
	public float ShakeTime;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float ShakeAmount;
	public float DecreaseNumber;

	Vector3 OriginalPos;

	void Awake()
	{
		if (CamTransform == null)
		{
			CamTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		OriginalPos = CamTransform.localPosition;
	}

	void Update()
	{
		if (ShakeTime > 0)
		{
			CamTransform.localPosition = OriginalPos + Random.insideUnitSphere * ShakeAmount;

			ShakeTime -= Time.deltaTime * DecreaseNumber;
		}
		else
		{
			ShakeTime = 0f;
			CamTransform.localPosition = OriginalPos;
		}
	}
}
