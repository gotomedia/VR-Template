using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems; 

[RequireComponent(typeof(Collider))]
public class Move2 : MonoBehaviour {
	private Vector3 startingPosition;
	private bool gazedAt;
	private bool predict;
	private Vector3 indicatorPos;
	private Vector3 playerPos;
	private bool sprinting;

	public GameObject player;
	public GameObject camera;
	public GameObject teleportIndicator; 
	public GameObject reticle;
	public bool sprint;
	public float sprintSpeed = 20.0f;

	void Start() {
		startingPosition = transform.localPosition;
		SetGazedAt(false);
		indicatorPos = teleportIndicator.transform.position;
	}

	void Update() {

		GvrViewer.Instance.UpdateState();
		if (GvrViewer.Instance.BackButtonPressed) {
			Application.Quit();
		}

		if (predict == true && sprinting == false) {
			Ray ray = new Ray (camera.transform.position, camera.transform.forward);
			RaycastHit hit;
			if (GetComponent<Collider>().Raycast (ray, out hit, 100.0f)) {
				/// Instantiate (teleportIndicator, hit.point, Quaternion.identity);
				teleportIndicator.transform.position = new Vector3 (hit.point.x, indicatorPos.y, hit.point.z);
			} else {
				teleportIndicator.transform.position = new Vector3 (0, -1000, 0);
			}
		} 
		if (predict == false && sprinting == false) {
			teleportIndicator.transform.position = new Vector3 (0, -1000, 0);
		}

		if (sprinting == true && Vector3.Distance(player.transform.position,playerPos) > 0) {
			player.transform.position = Vector3.MoveTowards(player.transform.position, playerPos, sprintSpeed*Time.deltaTime); 
		}
		if (sprinting == true && Vector3.Distance(player.transform.position,playerPos) <= 0) {
			sprinting = false;
		}
	}


	public void Reset() {
		transform.localPosition = startingPosition;
	}



	public void SetGazedAt(bool gazedAt) {
		predict = gazedAt;
	}


	public void TeleportTo(BaseEventData data ) {
		PointerEventData pointerData = data as PointerEventData;
		Vector3 worldPos = pointerData.pointerCurrentRaycast.worldPosition;
		playerPos = new Vector3(worldPos.x, player.transform.position.y, worldPos.z);
		if (sprint != true) {
			player.transform.position = playerPos;
		} else {
			sprinting = true;
		}
	}



}
