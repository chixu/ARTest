using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRotate : MonoBehaviour {

	float x;

	float y;

	float xSpeed = 20.0f;

	float ySpeed = 10.0f;

	float pinchSpeed;

	float distance = 10f;

	float minimumDistance = 5f;

	float maximumDistance = 100f;

	Touch touch;

	float lastDist = 0f;

	float curDist = 0f;

	Camera gameCamera;

	public float maximumXAngle = 0;
	public float maximumYAngle = 0;
	public bool isLocal = true;

	private float accumulateXAngle = 0;
	private float accumulateYAngle = 0;

	private Quaternion originalRotation;
	private Vector3 originalScale;
	private Vector3 originalPosition;

	void Awake(){
	
		originalRotation = this.transform.localRotation;
		originalScale = this.transform.localScale;
		originalPosition = this.transform.localPosition;

	}

	void OnEnable(){

		this.transform.localRotation = originalRotation;
		this.transform.localPosition = originalPosition;
		this.transform.localScale = originalScale;

	}

	void Update () 
	{

		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved) 

		{

			//One finger touch does orbit

			touch = Input.GetTouch(0);

			x = -touch.deltaPosition.x * xSpeed * Time.deltaTime;

			y = touch.deltaPosition.y * ySpeed * Time.deltaTime;


			if (maximumXAngle > 0) {
				accumulateXAngle += x;
				if (accumulateXAngle <= -maximumXAngle) {
					accumulateXAngle = -maximumXAngle;
					x = 0;
				} else if (accumulateXAngle >= maximumXAngle) {
					accumulateXAngle = maximumXAngle;
					x = 0;
				}
			}

			if (maximumYAngle > 0) {
				accumulateYAngle += y;
				if (accumulateYAngle <= -maximumYAngle) {
					accumulateYAngle = -maximumYAngle;
					y = 0;
				} else if (accumulateYAngle >= maximumYAngle) {
					accumulateYAngle = maximumYAngle;
					y = 0;
				}
			}

			Vector3 rotateAngle = new Vector3(y,x,0);


			if (isLocal) {
				transform.Rotate (rotateAngle, Space.Self);
			} else {
				transform.Rotate (rotateAngle, Space.World);
			}

		}

		/*if (Input.touchCount > 1 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)) 

		{

			//Two finger touch does pinch to zoom

			var touch1 = Input.GetTouch(0);t

			var touch2 = Input.GetTouch(1);

			curDist = Vector2.Distance(touch1.position, touch2.position);

			if(curDist > lastDist)

			{

				distance += Vector2.Distance(touch1.deltaPosition, touch2.deltaPosition)*pinchSpeed/10;

			}else{

				distance -= Vector2.Distance(touch1.deltaPosition, touch2.deltaPosition)*pinchSpeed/10;

			}



			lastDist = curDist;

		}*/


		if(distance <= minimumDistance)

		{

			//minimum camera distance

			distance = minimumDistance;

		}



		if(distance >= maximumDistance)

		{

			//maximum camera distance

			distance = maximumDistance;

		}




		//Sets rotation

		//var rotation = Quaternion.Euler(y, -x, 0);
		//transform.localRotation = rotation;
		//transform.RotateAround(transform.up,x);
		//transform.RotateAround(,x);

		//Sets zoom

		//var position = rotation * new Vector3(0.0f, 0.0f, -distance);



		//Applies rotation and position



		//transform.position = position;

	}
		
}
