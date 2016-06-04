using UnityEngine;
using System.Collections;

/// <summary>
/// A simple script to shake the camera when the player hit an obstacle
/// </summary>
public class CameraShake : MonoBehaviour{

	private static Vector3 originPosition;
	private static Quaternion originRotation;

	private static float shakeDecay = 0.002f;
	private static float shakeIntensity;

	public static IEnumerator Shake(Transform t){
		originPosition = t.position;
		originRotation = t.rotation;
		shakeIntensity = 0.3f;
		while (shakeIntensity > 0) {
			t.position = originPosition + Random.insideUnitSphere * shakeIntensity;
			t.rotation = new Quaternion (
				originRotation.x + Utils.RandomRange (-shakeIntensity, shakeIntensity) * .2f,
				originRotation.y + Utils.RandomRange (-shakeIntensity, shakeIntensity) * .2f,
				originRotation.z + Utils.RandomRange (-shakeIntensity, shakeIntensity) * .2f,
				originRotation.w + Utils.RandomRange (-shakeIntensity, shakeIntensity) * .2f);
			shakeIntensity -= shakeDecay;
			yield return false;
		}
	}

	public static IEnumerator Shake(Transform t, float i){
		originPosition = t.position;
		originRotation = t.rotation;
		shakeIntensity = i;
		while (shakeIntensity > 0) {
			t.position = originPosition + Random.insideUnitSphere * shakeIntensity;
			t.rotation = new Quaternion (
				originRotation.x + Utils.RandomRange (-shakeIntensity, shakeIntensity) * .2f,
				originRotation.y + Utils.RandomRange (-shakeIntensity, shakeIntensity) * .2f,
				originRotation.z + Utils.RandomRange (-shakeIntensity, shakeIntensity) * .2f,
				originRotation.w + Utils.RandomRange (-shakeIntensity, shakeIntensity) * .2f);
			shakeIntensity -= shakeDecay;
			yield return false;
		}
	}
}