using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

	public enum RotationAxes { camera = 0, player = 1 }
	public RotationAxes axes = RotationAxes.camera;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -20F;
	public float maximumY = 20F;
	public float maxXseated = 60F;
	public float minXseated = -60F;
	float rotationX = 0F;
	float rotationY = 0F;
	Quaternion originalRotation;
	private GameObject camera;

	void Update ()
	{
		if (camera.GetComponent<Standup> ().seatedState ()) {
			if (axes == RotationAxes.camera) {
				// Read the mouse input axis
				rotationX += Input.GetAxis ("Mouse X") * sensitivityX;
				rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
				rotationX = ClampAngle (rotationX, minXseated, maxXseated);
				rotationY = ClampAngle (rotationY, minimumY, maximumY);
				Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
				Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);
				transform.localRotation = originalRotation * xQuaternion * yQuaternion;
			} else if (axes == RotationAxes.player) {
			} else {}
		} else {
			if (axes == RotationAxes.camera) {
				rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
				rotationY = ClampAngle (rotationY, minimumY, maximumY);
				Quaternion yQuaternion = Quaternion.AngleAxis (-rotationY, Vector3.right);
				transform.localRotation = originalRotation * yQuaternion;
			} else if (axes == RotationAxes.player) {
				rotationX += Input.GetAxis ("Mouse X") * sensitivityX;
				rotationX = ClampAngle (rotationX, minimumX, maximumX);
				Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
				transform.localRotation = originalRotation * xQuaternion;
			} else {}
		}


	}
	void Start ()
	{
		// Make the rigid body not change rotation
		/*if (rigidbody)
			rigidbody.freezeRotation = true;*/
		originalRotation = transform.localRotation;
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}
}
