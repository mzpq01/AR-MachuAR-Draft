using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpVisibility : MonoBehaviour {
	public NCARTrackableEventHandler target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (target.IsBeingTracked)
			this.GetComponent<MeshRenderer> ().enabled = false;
		else
			this.GetComponent<MeshRenderer> ().enabled = true;
		
	}
}
