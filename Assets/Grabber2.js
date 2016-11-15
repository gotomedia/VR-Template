#pragma strict

 var hitObject : GameObject;
 
 function Update()
 {
 
 if(Input.touchCount > 0)  
 {
 
 var hit : RaycastHit;
 
 // Cast a ray
 
 if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), hit))
 {
 hitObject = hit.collider.gameObject;
 hitObject.transform.parent = gameObject.transform;
 }
 }
 }
 
if (Input.touchCount > 0) {
		var touch: Touch = Input.GetTouch(0);
		// Handle finger movements based on touch phase.
		switch (touch.phase) {
			// Record initial touch position.
			case TouchPhase.Ended:
			hitObject.transform.parent = null;
			hitObject = null;
			break;
	}

 
 }