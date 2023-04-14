using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside2 : MonoBehaviour {

	public Transform MinimapCam;
	 float MinimapSize;
	Vector3 TempV3;

    private void Start()
    {
		TempV3 = transform.parent.transform.position;
		TempV3.y = transform.position.y;

	}
    void Update () {
		
		transform.position = TempV3;
		MinimapSize = MinimapCam.GetComponent<Camera>().orthographicSize - 5;
	}

	void LateUpdate () {
		transform.position = new Vector3 (
			Mathf.Clamp(transform.position.x, MinimapCam.position.x-MinimapSize, MinimapSize+MinimapCam.position.x),
			transform.position.y,
			Mathf.Clamp(transform.position.z, MinimapCam.position.z-MinimapSize, MinimapSize+MinimapCam.position.z)
		);
	}
}
