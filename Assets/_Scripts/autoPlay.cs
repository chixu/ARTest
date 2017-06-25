using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoPlay : MonoBehaviour {
	private PlayVideo mPlayVideo = null;
	private MeshRenderer meshRenderer;

	// Use this for initialization
	void Start () {
		meshRenderer = GetComponent<MeshRenderer> ();
	}

	void update(){
		Debug.Log (meshRenderer.enabled.ToString());
	}
}
