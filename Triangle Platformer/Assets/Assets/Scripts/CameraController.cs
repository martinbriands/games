using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform player1;	
	public Transform player2;	

	Vector3 middlePoint;
	float distanceFromMiddlePoint;
	float distanceBetweenPlayers;
	float cameraDistance;
	float aspectRatio;
	float fov;
	float tanFov;


	void Start () {
		aspectRatio = Screen.width / Screen.height;
		tanFov = Mathf.Tan (Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);
	}

	void FixedUpdate () {

		if (player1 && player2) {
			Vector3 newCameraPos = Camera.main.transform.position;
			newCameraPos.x = middlePoint.x;
			newCameraPos.y = middlePoint.y;
			Camera.main.transform.position = newCameraPos;

			Vector3 vectorBetweenPlayers = player2.position - player1.position;
			middlePoint = player1.position + 0.5f * vectorBetweenPlayers;

			distanceBetweenPlayers = vectorBetweenPlayers.magnitude;
			cameraDistance = (distanceBetweenPlayers / 2.0f / aspectRatio) / tanFov;
			Vector3 dir = (Camera.main.transform.position - middlePoint).normalized;
			Camera.main.transform.position = middlePoint + dir * (cameraDistance);
			Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, -10);
			Camera.main.orthographicSize = Mathf.Max(distanceBetweenPlayers / 2 + 5, 10);
		} else {
			if (player1) {
				Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, new Vector3 (player1.transform.position.x, player1.transform.position.y, -10f), 0.1f);
			} else if (player2) {
				Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, new Vector3 (player2.transform.position.x, player2.transform.position.y, -10f), 0.1f);
			}
			if (Camera.main.orthographicSize > 5.5f) {
				Camera.main.orthographicSize -= 0.1f;
			}
				
			//if (GameObject.Find (winner).GetComponent<PlayerMovement> ().charging) {
			//	if (Camera.main.orthographicSize > 3f) {
			//		Camera.main.orthographicSize -= 0.1f;
			//	}
			//} else if (Camera.main.orthographicSize < 7.5f) {
			//	Camera.main.orthographicSize += 0.2f;
			//}
		}
	}
		
}

