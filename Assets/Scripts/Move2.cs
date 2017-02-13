using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems; 

[RequireComponent(typeof(Collider))]
public class Move2 : MonoBehaviour, IGvrGazeResponder {
	private Vector3 startingPosition;
	private bool gazedAt;

	public GameObject player;
	public GameObject camera;
	public GameObject teleportIndicator; 
	public GameObject reticle;

	void Start() {
		startingPosition = transform.localPosition;
		SetGazedAt(false);
	}

	void LateUpdate() {
		GvrViewer.Instance.UpdateState();
		if (GvrViewer.Instance.BackButtonPressed) {
			Application.Quit();
		}
	}


	public void Reset() {
		transform.localPosition = startingPosition;
	}



	public void SetGazedAt(bool gazedAt) {
		RaycastHit hit;
		if (Physics.Raycast (camera.transform.position, -Vector3.up, out hit, 100.0f)) {
			Instantiate( teleportIndicator, hit.point, Quaternion.identity );
		}
	}


	public void TeleportTo(BaseEventData data ) {
		PointerEventData pointerData = data as PointerEventData;
		Vector3 worldPos = pointerData.pointerCurrentRaycast.worldPosition;
		Vector3 playerPos = new Vector3(worldPos.x, player.transform.position.y, worldPos.z);
		player.transform.position = playerPos;
	}



	#region IGvrGazeResponder implementation

	/// Called when the user is looking on a GameObject with this script,
	/// as long as it is set to an appropriate layer (see GvrGaze).
	public void OnGazeEnter() {
		gazedAt = true;
	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeExit() {
		gazedAt = false;
	}

	/// Called when the viewer's trigger is used, between OnGazeEnter and OnPointerExit.
	public void OnGazeTrigger() {
	}

	#endregion
}
