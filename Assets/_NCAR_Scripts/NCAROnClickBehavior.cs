using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NCAROnClickBehavior : MonoBehaviour
{
	public GameObject model;
	public GameObject upModel;
	private Vector3 upModelPosInit;

	bool isOnScaling = false;
	bool isFlatted = false;
	// Use this for initialization
	void Start ()
	{
		upModelPosInit = upModel.transform.localPosition;
			//upModel.transform.InverseTransformPoint (upModel.transform.position);
		print (upModelPosInit);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isOnScaling)
			DoScale ();
	}

	void OnMouseDown ()
	{
		Debug.Log ("This Object is Pressed!");
		this.isOnScaling = true;
	}

	void DoScale ()
	{
		
		//
		Vector3 t = model.transform.localScale;
		float factor = 2.5f * Time.deltaTime;
		Vector3 moveDir = new Vector3 (0, 1f, 0);
		if (isFlatted) {
			upModel.transform.localPosition = MoveObjectPosition (upModel, -1 * moveDir, 350f);
			t.y = t.y + factor;
			if (t.y > 1) {
				t.y = 1;
				model.transform.localScale = t;

				upModel.transform.localPosition = upModelPosInit;

				isOnScaling = false;
				isFlatted = false;

				return;
			}
		} else {
			upModel.transform.localPosition = MoveObjectPosition (upModel, 1 * moveDir, 350f);
			t.y = t.y - factor;
			if (t.y < 0) {
				
				isOnScaling = false;
				isFlatted = true;
				return;
			}
		}
		model.transform.localScale = t;
	}

	void ScaleObject (GameObject model, float factor)
	{
		Transform tf = model.transform;
		Vector3 scale = tf.localScale;
		float dtFactor = factor * Time.deltaTime; //speed
	}

	Vector3 MoveObjectPosition (GameObject model, Vector3 dir, float factor){
		Vector3 pos = model.transform.localPosition;
		pos = pos + (dir * factor * Time.deltaTime);
		return pos;
	}
		
}
