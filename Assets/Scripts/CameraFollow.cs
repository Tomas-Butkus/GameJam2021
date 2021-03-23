using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    public float SmoothSpeed = 10f;
    public Vector3 OffSet;

	void FixedUpdate()
	{
		Vector3 desiredPosition = Target.position + OffSet;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed * Time.deltaTime);

		transform.position = smoothedPosition;
		transform.LookAt(Target);
	}
}
