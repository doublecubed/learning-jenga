// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
		private bool cameraMoving;

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				mousePressed = true;
				//Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}

			if (Input.GetMouseButtonUp(0))
			{
				mousePressed = false;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}

			if (mousePressed && !cameraMoving)
			{
				float mouseXDelta = Input.GetAxis("Mouse X");
				float mouseYDelta = Input.GetAxis("Mouse Y");

				
				transform.Rotate(0f, mouseXDelta * horizontalRotateSpeed, 0f);
				cameraTransform.position += Vector3.up * mouseYDelta * verticalMoveSpeed * Time.deltaTime;


			}
			
			
			float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
			cameraTransform.position += cameraTransform.forward * mouseScroll * zoomSpeed;


			if (Input.GetKeyDown(KeyCode.Alpha6) && !cameraMoving)
			{
				MoveToSixthGrade();
			}
			
			if (Input.GetKeyDown(KeyCode.Alpha7) && !cameraMoving)
			{
				MoveToSeventhGrade();
			}
			
			if (Input.GetKeyDown(KeyCode.Alpha8) && !cameraMoving)
			{
				MoveToEighthGrade();
			}
		}

		public void MoveToSixthGrade()
		{
			StartCoroutine(MoveToFocus(focusPointCenters[0]));
		}

		public void MoveToSeventhGrade()
		{
			StartCoroutine(MoveToFocus(focusPointCenters[1]));
		}

		public void MoveToEighthGrade()
		{
			StartCoroutine(MoveToFocus(focusPointCenters[2]));
		}
		
		private bool IsPointerOverUI()
		{
			// Check if the mouse pointer is over any UI element
			if (EventSystem.current != null)
			{
				PointerEventData eventData = new PointerEventData(EventSystem.current);
				eventData.position = Input.mousePosition;

				List<RaycastResult> results = new List<RaycastResult>();
				EventSystem.current.RaycastAll(eventData, results);

				return results.Count > 0;
			}

			return false;
		}
		
		private IEnumerator MoveToFocus(Transform focusPoint)
		{
			cameraMoving = true;
			
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

			cameraMoving = false;
		}
	}

}