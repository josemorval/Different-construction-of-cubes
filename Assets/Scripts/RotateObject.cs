using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {

	public float rotateVel;

	void Update () {
		transform.Rotate(new Vector3(0f,rotateVel*Time.deltaTime,0f));
	}
}
