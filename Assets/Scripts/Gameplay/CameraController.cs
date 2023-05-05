// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JengaGame.Gameplay
{
	public class CameraController : MonoBehaviour
	{
		public Transform cameraTransform;
		public Transform[] focusPointCenters;

		public float verticalMoveSpeed;
		public float horizontalRotateSpeed;
		public float zoomSpeed;
		public float focusSwitchDuration;
		
		private bool mousePressed;

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				mousePressed = true;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}

			if (Input.GetMouseButtonUp(0))
			{
				mousePressed = false;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}

			if (mousePressed)
			{
				float mouseXDelta = Input.GetAxis("Mouse X");
				float mouseYDelta = Input.GetAxis("Mouse Y");

				
				transform.Rotate(0f, mouseXDelta * horizontalRotateSpeed, 0f);
				cameraTransform.position += Vector3.up * mouseYDelta * verticalMoveSpeed * Time.deltaTime;


			}
			
			
			float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
			cameraTransform.position += cameraTransform.forward * mouseScroll * zoomSpeed;


			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				StartCoroutine(MoveToFocus(focusPointCenters[0]));
			}
			
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				StartCoroutine(MoveToFocus(focusPointCenters[1]));
			}
			
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				StartCoroutine(MoveToFocus(focusPointCenters[2]));
			}
		}

		private IEnumerator MoveToFocus(Transform focusPoint)
		{
			Vector3 startPos = transform.position;
			Vector3 endPos = focusPoint.position;
			
			float timer = 0f;
			while (timer < focusSwitchDuration)
			{
				timer += Time.deltaTime;
				float lerp = Mathf.Clamp(timer / focusSwitchDuration, 0f, 1f);

				transform.position = Vector3.Lerp(startPos, endPos, lerp);
				yield return null;
			}
		}
	}

}