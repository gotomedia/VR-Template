using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems; 

[RequireComponent(typeof(Collider))]
public class Move3 : MonoBehaviour {
	private Vector3 startingPosition;
	private bool gazedAt;
	private bool predict;
	private Vector3 indicatorPos;
	private Vector3 playerPos;
	private bool sprinting;

	public Material inactiveMaterial;
	public Material gazedAtMaterial;
	public GameObject player;
	public GameObject camera;
	public GameObject reticle;
	public bool sprint;
	public float sprintSpeed = 20.0f;

	void Start() {
		startingPosition = transform.localPosition;
		SetGazedAt(false);
	}

	void Update() {
		GvrViewer.Instance.UpdateState();
		if (GvrViewer.Instance.BackButtonPressed) {
			Application.Quit();
		}

		if (predict == true && sprinting == false) {
			if (inactiveMaterial != null && gazedAtMaterial != null) {
				transform.parent.GetComponent<Renderer>().material = gazedAtMaterial;
				return;
			}
			Ray ray = new Ray (camera.transform.position, camera.transform.forward);
			RaycastHit hit;
			if (GetComponent<Collider>().Raycast (ray, out hit, 100.0f)) {
				
			} else {
			
			}
		} 

		if (predict == false && sprinting == false) {
			if (inactiveMaterial != null && gazedAtMaterial != null) {
				transform.parent.GetComponent<Renderer>().material = inactiveMaterial;
				return;
			}
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


	public void TeleportTo() {

		playerPos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
		if (sprint != true) {
			player.transform.position = playerPos;
		} else {
			sprinting = true;
		}
	}



}
