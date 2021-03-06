﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TouchInput : MonoBehaviour {

	// bool SupportsMultiTouch = Input.multiTouchEnabled;
	public Image testImge;
	public GameObject thing;

	Vector2 mXAxis = new Vector2(1, 0);
	Vector2 mYAxis = new Vector2(0, 1);

	float minSwipeDist = 50f;
	float minVelocity = 10.0f;
	float minAngle = 30f;

	private Vector2 startPos;
	private float swipeStartTime;
	TouchState state;

	enum TouchState { still, sLeft, sRight, sUp, sDown};



	void Update()
	{
		int nbTouches = Input.touchCount;

		if(nbTouches > 0)
		{
			// testImge.color = Color.blue;
			for(int i = 0; i< nbTouches; i++)
			{
				Touch touch = Input.GetTouch(i);

				if(touch.phase == TouchPhase.Began)
				{
					startPos = touch.position;
					swipeStartTime = Time.time;
				}

				if(touch.phase == TouchPhase.Ended)
				{
					float deltaTime = Time.time - swipeStartTime;
					Vector2 endPos = new Vector2(touch.position.x, touch.position.y);


					Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
					RaycastHit hit;

					/*if (Physics.Raycast(ray, out hit,200))
					{
						//testImge.color = Color.red;
						if (hit.transform.gameObject.name.Equals("Cube"))
						{
							testImge.color = Color.red;
						}
					}*/



					Vector2 swipeVector = endPos - startPos;
					float velocity = (swipeVector.magnitude/deltaTime);

					if(velocity > minVelocity && (swipeVector.magnitude > minSwipeDist))
					{
						//ladies and gentlement, we have a swipe

						swipeVector.Normalize();

						float angleOfSwipe = Vector2.Dot(swipeVector, mXAxis);
						angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;

						if(angleOfSwipe < minAngle)
						{
							//right
							state = TouchState.sRight;
							testImge.color = Color.black;
						}
						else if((180f - angleOfSwipe) < minAngle)
						{
							//left
							state = TouchState.sLeft;
							testImge.color = Color.green;
						}
						else
						{
							angleOfSwipe = Vector2.Dot(swipeVector, mYAxis);
							angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;
							if(angleOfSwipe < minAngle)
							{
								//top
								state = TouchState.sUp;
								testImge.color = Color.gray;
							}
							else if((180f - angleOfSwipe) < minAngle)
							{
								//bottom
								state = TouchState.sDown;
								testImge.color = Color.magenta;
							}
							else
							{
								//errror
							}
						}

					}
				}
			}
		}
		else
		{
			//testImge.color = Color.red;
		}
	}


}
