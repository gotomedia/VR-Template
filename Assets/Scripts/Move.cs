using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems; 

[RequireComponent(typeof(Collider))]
public class Move : MonoBehaviour, IGvrGazeResponder {
	private Vector3 startingPosition;
	private bool gazedAt;

	public GameObject player; 
	public GameObject teleportIndicator; 
	public GameObject reticle;

	void Start() {
		startingPosition = transform.localPosition;
	}

	void LateUpdate() {
		GvrViewer.Instance.UpdateState();
		if (GvrViewer.Instance.BackButtonPressed) {
			Application.Quit();
		}
		if (gazedAt){
			Vector3 p = reticle.transform.position;
			Vector3 teleportPos = new Vector3(p.x, 0, p.z);
			Vector3 hiddenPos = new Vector3 (p.x, -100, p.z);
			teleportIndicator.transform.position = gazedAt ? teleportPos : hiddenPos;
		}
	}
		

	public void Reset() {
		transform.localPosition = startingPosition;
	}



	public void TeleportInd(BaseEventData data) {
		PointerEventData pointerData = data as PointerEventData;
		Vector3 worldPos = pointerData.pointerCurrentRaycast.worldPosition;
		Vector3 teleportPos = new Vector3(worldPos.x, 0, worldPos.z);
		Vector3 hiddenPos = new Vector3 (worldPos.x, -100, worldPos.z);
		teleportIndicator.transform.position = gazedAt ? teleportPos : hiddenPos;

	}
		

	public void TeleportTo(BaseEventData data ) {
		PointerEventData pointerData = data as PointerEventData;
		Vector3 worldPos = pointerData.pointerCurrentRaycast.worldPosition;
		Vector3 playerPos = new Vector3(worldPos.x, player.transform.position.y, worldPos.z);
		player.transform.position = playerPos;
	}

	public void MoveTo(BaseEventData data ) {
		PointerEventData pointerData = data as PointerEventData;
		Vector3 worldPos = pointerData.pointerCurrentRaycast.worldPosition;
		Vector3 playerPos = new Vector3(worldPos.x, player.transform.position.y, worldPos.z);
		player.transform.position = playerPos;
	}

}
