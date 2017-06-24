using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour {

	#region PRIVATE_MEMBERS
	public BoardController BoardMessageController;



	#endregion //PRIVATE_MEMBERS

	// Use this for initialization
	void Start () {
		if (BoardMessageController == null) {
			Debug.LogError("No BoardController Is Given");
			Destroy (this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (new Vector3 (0, Time.deltaTime*50, 0), Space.Self);
	}

	public void OnUserClicked(){

		//Destroy (this.gameObject);

		BoardMessageController.ShowBoardTarget ();

	}
}
